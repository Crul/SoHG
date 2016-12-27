using Sohg.SocietyAgg.Contracts;
using System;

namespace Sohg.SocietyAgg
{
    public class SocietyState : ISocietyState
    {
        private ISociety society;
        private float aggressivityRate;
        private float resourcesConservationRate = 0.2f;
        private int minCellToSplit = 30;
        private float destabilizationFactorPerCell = 0.0001f;

        public long CivilizationLevel { get; private set; }
        public long Population { get; private set; }
        public long Resources { get; private set; }
        public float TechnologyLevelRate { get; private set; }

        public float FaithShrinkingRateBonus { get; set; }
        public int PowerBonus { get; set; }

        private long consume
        {
            get { return Population; }
        }

        private int territoryExtension { get { return society.Territory.CellCount; } }

        public int MaximumAttacks
        {
            get { return System.Math.Max(1, System.Convert.ToInt32(Power / 500)); } // TODO calculate MaximumAttacks
        }

        public int ExpansionCapacity
        {
            get
            {
                var positiveExpansionFactor = 1.2f;
                var expansionCapacity = 0;
                if (positiveExpansionFactor * PopulationDensity > ProductionLimitPerCell)
                {
                    expansionCapacity = Convert.ToInt32(
                        Math.Ceiling(positiveExpansionFactor * PopulationDensity / ProductionLimitPerCell));
                }
                else
                {
                    expansionCapacity = Math.Min(0, Convert.ToInt32(
                        1 - (Math.Floor(1.4f * ProductionLimitPerCell / (1 + PopulationDensity)))));
                }
                
                return expansionCapacity;
            }
        }

        public float PopulationDensity
        {
            get
            {
                return (float)Population / (float)System.Math.Max(1, society.Territory.CellCount);
            }
        }

        public float Power
        {
            get
            {
                return PowerBonus + System.Math.Max(1, 100 * PopulationDensity * aggressivityRate * TechnologyLevelRate);
            }
        }

        public long Production
        {
            get
            {
                return Convert.ToInt64(territoryExtension * ((99f * PopulationDensity) + (1f * ProductionLimitPerCell)) / 100);
            }
        }

        public long ProductionLimitPerCell
        {
            get { return Convert.ToInt64(100 * Math.Pow(50000, 1.065 * TechnologyLevelRate)); }
        }

        public float SplitingProbability
        {
            get
            {   
                if (territoryExtension < minCellToSplit)
                {
                    return 0;
                }

                return territoryExtension * destabilizationFactorPerCell;
            }
        }
        
        public SocietyState(ISociety society)
        {
            this.society = society;
            aggressivityRate = society.Species.InitialAggressivityRate;
            TechnologyLevelRate = society.Species.InitialTechnologyLevelRate;
            CivilizationLevel = 0;
            Population = 0;
            Resources = 0;
        }

        public void Evolve()
        {
            Resources = Convert.ToInt64(Resources * resourcesConservationRate);

            var currentProduction = Production;
            var currentConsume = consume;

            var resourcesGrowth = (Production - consume);
            Resources += resourcesGrowth;

            long populationGrowth;
            if (Resources < 0)
            {
                populationGrowth = Convert.ToInt64(0.5 * Resources);
                Resources = 0;
            }
            else
            {
                populationGrowth = Convert.ToInt64((1 + TechnologyLevelRate) * Resources);
            }

            Population += populationGrowth;
        }

        public void Kill(long deads)
        {
            Population -= deads;
        }

        public int GetFaithEmitted()
        {
            // TODO Society.State.GetFaithEmitted() configuration
            var isFaithEmitted = UnityEngine.Random.Range(0f, 1f) > 0.8;
            var faithEmitted = (isFaithEmitted ? UnityEngine.Random.Range(2, 10) : 0);

            return faithEmitted;
        }

        public void OnExpanded()
        {
            Resources -= Convert.ToInt64(PopulationDensity / 2);
            if (Resources < 0)
            {
                Resources = 0;
            }
        }

        public void OnSkillAdded(ISkill skill)
        {
            var populationOverProduction = (Population / ProductionLimitPerCell);
            TechnologyLevelRate += skill.TechnologyRateBonus;
            Population = populationOverProduction * ProductionLimitPerCell;

            FaithShrinkingRateBonus += skill.FaithShrinkingRateBonus;
        }

        public void OnSplit(ISociety splitSociety, long totalPopulation, long totalResources)
        {
            var totalTerritoryExtension = (territoryExtension + splitSociety.Territory.CellCount);
            var territoryProportion = ((float)territoryExtension / totalTerritoryExtension);

            Population = Convert.ToInt32(territoryProportion * totalPopulation);
            Resources = Convert.ToInt32(territoryProportion * totalResources);
            CivilizationLevel = Math.Max(CivilizationLevel, splitSociety.State.CivilizationLevel);
            TechnologyLevelRate = Math.Max(TechnologyLevelRate, splitSociety.State.TechnologyLevelRate);
            FaithShrinkingRateBonus = Math.Max(FaithShrinkingRateBonus, splitSociety.State.FaithShrinkingRateBonus);
        }

        public void SetInitialPopulation()
        {
            Population = territoryExtension * society.Species.InitialPopulationDensity;
        }
    }
}
