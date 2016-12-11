using Sohg.SocietyAgg.Contracts;
using UnityEngine;

namespace Sohg.SocietyAgg
{
    public class SocietyState : ISocietyState
    {
        private ISociety society;
        private float aggressivityRate;

        public float FriendshipRange { get; private set; }
        public long PopulationAmount { get; private set; }
        public float TechnologyLevelRate { get; private set; }
        
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
            get { return 5; } // TODO calculate ExpansionCapacity
        }

        public SocietyState(ISociety society)
        {
            this.society = society;
            aggressivityRate = society.Species.InitialAggressivityRate;
            TechnologyLevelRate = society.Species.InitialTechnologyLevelRate;
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
}
