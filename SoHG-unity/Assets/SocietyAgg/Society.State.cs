using System;
using Sohg.SocietyAgg.Contracts;

namespace Sohg.SocietyAgg
{
    public partial class Society
    {
        public struct SocietyState : ISocietyState
        {
            private float aggressivityRate;
            private float technologyLevelRate;

            public float FriendshipRange { get; private set; }
            public long PopulationAmount { get; private set; }

            public float Power
            {
                get { return Math.Max(1, PopulationAmount * aggressivityRate * technologyLevelRate); }
            }

            public int MaximumAttacks
            {
                get { return Math.Max(1, Convert.ToInt32(Power / 10000)); } // TODO calculate MaximumAttacks
            }

            public SocietyState(ISocietyDefinition definition)
            {
                aggressivityRate = definition.InitialAggressivityRate;
                technologyLevelRate = definition.InitialTechnologyLevelRate;
                PopulationAmount = 0;
                FriendshipRange = 0.5f;
            }

            public void SetInitialPopulation(long populationAmount)
            {
                PopulationAmount = populationAmount;
            }

            public void Kill(float deathRate)
            {
                PopulationAmount = Convert.ToInt64(PopulationAmount * (1 - deathRate));
            }
        }

        public ISocietyState State { get; private set; }
        
        public void Initialize()
        {
            var initialPopulation = Territory.CellCount * config.InitialSocietyPopulationByCell;
            State.SetInitialPopulation(initialPopulation);
        }
    }
}
