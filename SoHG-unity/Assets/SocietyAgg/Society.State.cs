using Sohg.SocietyAgg.Contracts;

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
                get { return populationAmount * aggressivityRate * technologyLevelRate; }
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
        }

        public ISocietyState State { get; private set; }
        
        public void Initialize()
        {
            var initialPopulation = Territory.CellCount * config.InitialSocietyPopulationByCell;
            State.SetInitialPopulation(initialPopulation);
        }
    }
}
