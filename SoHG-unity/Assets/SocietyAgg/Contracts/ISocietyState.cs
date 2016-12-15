namespace Sohg.SocietyAgg.Contracts
{
    public interface ISocietyState
    {
        int ExpansionCapacity { get; }
        int MaximumAttacks { get; }
        long Population { get; }
        float PopulationDensity { get; }
        float Power { get; }
        long Production { get; }
        long Resources { get; }
        float TechnologyLevelRate { get; }

        float FaithShrinkingRateBonus { get; set; }
        int PowerBonus { get; set; }

        int GetFaithEmitted();
        void Evolve();
        void Expanded();
        void Kill(long deads);
        void OnSkillAdded(ISkill skill);
        void SetInitialPopulation();
    }
}
