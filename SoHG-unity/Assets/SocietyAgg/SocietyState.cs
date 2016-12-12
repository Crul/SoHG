using Sohg.CrossCutting.Contracts;
using Sohg.SocietyAgg.Contracts;
using System;
using System.Linq;

namespace Sohg.SocietyAgg
{
    public class SocietyState : ISocietyState
    {
        private ISociety society;
        private ISohgConfig config;
        private float aggressivityRate;
        private long resources;

        public float FriendshipRange { get; private set; }
        public long PopulationAmount { get; private set; }
        public float TechnologyLevelRate { get; private set; }
        
        private long consume
        {
            get { return Convert.ToInt64(PopulationAmount); }
        }

        private long production
        {
            get { return productionPerCell * territoryExtension; }
        }

        private long productionPerCell
        {
            get { return Convert.ToInt64(100 * Math.Pow(50000, 1.065 * TechnologyLevelRate)); }
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
                var expansionCapacity = Convert.ToInt32(Math.Floor(1.1f * PopulationDensity / productionPerCell));

                return Math.Max(0, expansionCapacity);
            }
        }

        public float PopulationDensity
        {
            get { return PopulationAmount / System.Math.Max(1, society.Territory.CellCount); }
        }

        public float Power
        {
            get
            {
                var powerBonus = society.Actions
                    .Where(action => typeof(IPowerBonus).IsAssignableFrom(action.GetType()))
                    .Sum(action => ((IPowerBonus)action).GetPowerBonus(society));

                return powerBonus + System.Math.Max(1, 100 * PopulationDensity * aggressivityRate * TechnologyLevelRate);
            }
        }


        public SocietyState(ISohgConfig config, ISociety society)
        {
            this.config = config;
            this.society = society;
            aggressivityRate = society.Species.InitialAggressivityRate;
            TechnologyLevelRate = society.Species.InitialTechnologyLevelRate;
            PopulationAmount = 0;
            FriendshipRange = 0.5f;
        }

        public void Evolve()
        {
            var resourcesGrowth = (production - consume);
            resources += resourcesGrowth;

            long populationGrowth;
            if (resources < 0)
            {
                populationGrowth = Convert.ToInt64(0.1 * resources);
                resources = 0;
            }
            else
            {
                populationGrowth = Convert.ToInt64(0.002 * resources);
            }

            PopulationAmount += populationGrowth;
            TechnologyLevelRate *= 1.001f;
        }

        public void Kill(float deathRate)
        {
            var deads = System.Convert.ToInt64(PopulationDensity * deathRate / (5 + deathRate));
            PopulationAmount -= deads;
        }

        public int GetFaithEmitted()
        {
            // TODO Society.State.GetFaithEmitted() configuration
            var isFaithEmitted = UnityEngine.Random.Range(0f, 1f) > 0.1;
            var faithEmitted = (isFaithEmitted ? UnityEngine.Random.Range(2, 10) : 0);

            return faithEmitted;
        }

        public void OnSkillAdded(ISkill skill)
        {
            TechnologyLevelRate += skill.TechnologyRateBonus;
        }

        public void SetInitialPopulation()
        {
            PopulationAmount = territoryExtension * config.InitialPopulationByCell;
        }

        public void Expanded()
        {
            resources -= Convert.ToInt64(PopulationDensity * 3);
            if (resources < 0)
            {
                resources = 0;
            }
        }
    }
}
