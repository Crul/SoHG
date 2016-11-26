using System;
using Sohg.SocietyAgg.Contracts;
using Sohg.CrossCutting.Contracts;

namespace Sohg.SocietyAgg
{
    public partial class Society
    {
        public struct SocietyState : ISocietyState
        {
            private float aggressivityRate;
            private long populationAmount;
            private float technologyLevelRate;

            public float FriendshipRange { get; private set; }

            public float Power
            {
                get { return Math.Max(1, populationAmount * aggressivityRate * technologyLevelRate); }
            }

            public int MaximumAttacks
            {
                get { return Math.Max(1, Convert.ToInt32(Power / 10000)); } // TODO calculate MaximumAttacks
            }

            public SocietyState(ISocietyDefinition definition)
            {
                aggressivityRate = definition.InitialAggressivityRate;
                technologyLevelRate = definition.InitialTechnologyLevelRate;
                populationAmount = 0;
                FriendshipRange = 0.5f;
            }

            public void SetInitialPopulation(long populationAmount)
            {
                this.populationAmount = populationAmount;
            }

            public void Kill(float damageRate)
            {
                // TODO Kill configuration to SohgConfig
                float deathRate;
                if (damageRate < 1)
                { // we win
                    deathRate = ((10000 - damageRate) / 10000);
                }
                else
                { // we loose
                    deathRate = 1 / ((9 + damageRate) / 10);
                }

                populationAmount = Convert.ToInt64(populationAmount * deathRate);
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
