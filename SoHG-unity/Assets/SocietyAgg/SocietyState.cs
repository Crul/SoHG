﻿using Sohg.Grids2D.Contracts;
using Sohg.SocietyAgg.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sohg.SocietyAgg
{
    public class SocietyState : ISocietyState
    {
        private ISociety society;
        private float resourcesConservationRate = 0.2f;
        private int minCellToSplit = 30;
        private float destabilizationFactorPerCell = 0.0001f;

        public float AggressivityRate { get; private set; }
        public List<IBoat> Boats { get; private set; }
        public long CivilizationLevel { get; private set; }
        public long Population { get; private set; }
        public long Resources { get; private set; }
        public float SeaMovementCapacity { get; private set; }
        public float TechnologyLevelRate { get; private set; }

        public float FaithShrinkingRateBonus { get; set; }
        public int PowerBonus { get; set; }

        private long consume
        {
            get { return Population; }
        }

        public int BoatCapacity
        {
            get
            {
                return 1; // TODO BoatCapacity
            }
        }

        public int ExpansionCapacity
        {
            get
            {
                var expansionCapacity = 0;
                if (PopulationDensity > ProductionLimitPerCell)
                {
                    expansionCapacity = Convert.ToInt32(
                        Math.Ceiling(PopulationDensity / ProductionLimitPerCell));
                }
                else
                {
                    expansionCapacity = Math.Min(0, Convert.ToInt32(
                        1 - (Math.Floor(ProductionLimitPerCell / (1 + PopulationDensity)))));
                }
                
                return expansionCapacity;
            }
        }

        public int MaximumAttacks
        {
            get { return System.Math.Max(1, System.Convert.ToInt32(Power / 500)); } // TODO calculate MaximumAttacks
        }

        public float PopulationDensity
        {
            get
            {
                return (float)Population / (float)System.Math.Max(1, society.TerritoryExtension);
            }
        }

        public float Power
        {
            get
            {
                return PowerBonus + System.Math.Max(1, 100 * PopulationDensity * AggressivityRate * TechnologyLevelRate);
            }
        }

        public long Production
        {
            get
            {
                return Convert.ToInt64(society.Territory.GetFertility()
                    * ((99f * PopulationDensity) + ProductionLimitPerCell) / 100f);
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
                if (society.TerritoryExtension < minCellToSplit)
                {
                    return 0;
                }

                return society.TerritoryExtension * destabilizationFactorPerCell;
            }
        }
        
        public SocietyState(ISociety society)
        {
            this.society = society;
            AggressivityRate = society.Species.InitialAggressivityRate;
            TechnologyLevelRate = society.Species.InitialTechnologyLevelRate;
            CivilizationLevel = 0;
            Population = 0;
            Resources = 0;
            SeaMovementCapacity = 0;
            Boats = new List<IBoat>();
        }

        public void Evolve()
        {
            Resources = Convert.ToInt64(Resources * resourcesConservationRate);
            
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
                populationGrowth = Convert.ToInt64((1 + TechnologyLevelRate) * (1 + Resources));
            }

            Population += populationGrowth;
        }

        public void InheritState(ISociety originSociety)
        {
            AggressivityRate = originSociety.State.AggressivityRate;
            SeaMovementCapacity = originSociety.State.SeaMovementCapacity;
            TechnologyLevelRate = originSociety.State.TechnologyLevelRate;
        }

        public void Kill(long deads)
        {
            Population -= deads;
        }

        public List<int> GetFaithEmitted(ITerritory territory)
        {
            // TODO Society.State.GetFaithEmitted() configuration
            return Enumerable.Range(0, territory.CellCount)
                .Where(cell => UnityEngine.Random.Range(0f, 1f) > 0.995f)
                .Select(cell => UnityEngine.Random.Range(3, 9))
                .ToList();
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
            SeaMovementCapacity += skill.SeaMovementCapacityBonus;

            FaithShrinkingRateBonus += skill.FaithShrinkingRateBonus;
        }

        public void OnSplit(ISociety splitSociety, long totalPopulation, long totalResources)
        {
            var totalTerritoryExtension = (society.TerritoryExtension + splitSociety.TerritoryExtension);
            var territoryProportion = ((float)society.TerritoryExtension / totalTerritoryExtension);

            Population = Convert.ToInt32(territoryProportion * totalPopulation);
            Resources = Convert.ToInt32(territoryProportion * totalResources);
            CivilizationLevel = Math.Max(CivilizationLevel, splitSociety.State.CivilizationLevel);
            TechnologyLevelRate = Math.Max(TechnologyLevelRate, splitSociety.State.TechnologyLevelRate);
            FaithShrinkingRateBonus = Math.Max(FaithShrinkingRateBonus, splitSociety.State.FaithShrinkingRateBonus);
        }

        public void SetInitialPopulation(float initialPopulationDensity)
        {
            Population = Convert.ToInt64(society.TerritoryExtension * initialPopulationDensity);
        }
    }
}
