using Sohg.SocietyAgg.Contracts;
using Sohg.SpeciesAgg.Contracts;
using UnityEngine;

namespace Sohg.SocietyAgg
{
    public partial class Society
    {
        public struct SocietyState : ISocietyState
        {
            private float aggressivityRate;

            public float FriendshipRange { get; private set; }
            public long PopulationAmount { get; private set; }
            public float TechnologyLevelRate { get; private set; }

            public float Power
            {
                get { return System.Math.Max(1, PopulationAmount * aggressivityRate * TechnologyLevelRate); }
            }

            public int MaximumAttacks
            {
                get { return System.Math.Max(1, System.Convert.ToInt32(Power / 200)); } // TODO calculate MaximumAttacks
            }

            public SocietyState(ISpecies species)
            {
                aggressivityRate = species.InitialAggressivityRate;
                TechnologyLevelRate = species.InitialTechnologyLevelRate;
                PopulationAmount = 0;
                FriendshipRange = 0.5f;
            }

            public void SetInitialPopulation(long populationAmount)
            {
                PopulationAmount = populationAmount;
            }

            public void Kill(float deathRate)
            {
                PopulationAmount = System.Convert.ToInt64(PopulationAmount * (1 - deathRate));
            }

            public int GetFaithEmitted()
            {
                // TODO Society.State.GetFaithEmitted() configuration
                var isFaithEmitted = Random.Range(0f, 1f) > 0.1;
                var faithEmitted = (isFaithEmitted ? Random.Range(2, 10) : 0);

                return faithEmitted;
            }

            public void OnSkillAdded(ISkill skill)
            {
                TechnologyLevelRate += skill.TechnologyRateBonus;
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
