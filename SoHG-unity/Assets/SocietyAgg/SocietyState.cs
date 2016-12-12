using Sohg.CrossCutting.Contracts;
using Sohg.SocietyAgg.Contracts;
using System;

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
            get { return Convert.ToInt64(100 * Math.Pow(50000, 1.065 * TechnologyLevelRate) * territoryExtension); }
        }

        private int territoryExtension { get { return society.Territory.CellCount; } }

        public float PopulationDensity
        {
            get { return PopulationAmount / System.Math.Max(1, society.Territory.CellCount); }
        }

        public float Power
        {
            get { return System.Math.Max(1, PopulationAmount * aggressivityRate * TechnologyLevelRate); }
        }

        public int MaximumAttacks
        {
            get { return System.Math.Max(1, System.Convert.ToInt32(Power / 200)); } // TODO calculate MaximumAttacks
        }

        public int ExpansionCapacity
        {
            get { return Convert.ToInt32(Math.Log(1 + (0.1 * resources / PopulationDensity))); } // TODO calculate ExpansionCapacity
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
            resources += (production - consume);

            long populationGrowth;
            if (resources < 0)
            {
                populationGrowth = Convert.ToInt64(0.1 * resources);
                resources = 0;
            }
            else
            {
                populationGrowth = Convert.ToInt64(0.05 * resources);
            }

            PopulationAmount = PopulationAmount + populationGrowth;
        }

        public void Kill(float deathRate)
        {
            PopulationAmount = System.Convert.ToInt64(PopulationAmount * (1 - deathRate));
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
