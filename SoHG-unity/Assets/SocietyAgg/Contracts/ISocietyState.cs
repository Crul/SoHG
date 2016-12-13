namespace Sohg.SocietyAgg.Contracts
{
    public interface ISocietyState
    {
        int ExpansionCapacity { get; }
        int MaximumAttacks { get; }
        long PopulationAmount { get; }
        float PopulationDensity { get; }
        float Power { get; }
        float TechnologyLevelRate { get; }

        float FaithShrinkingRateBonus { get; set; }
        int PowerBonus { get; set; }

        int GetFaithEmitted();
        void Evolve();
        void Expanded();
        void Kill(float damageRate);
        void OnSkillAdded(ISkill skill);
        void SetInitialPopulation();
    }
}
