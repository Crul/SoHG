namespace Sohg.SocietyAgg.Contracts
{
    public interface ISocietyState
    {
        long CivilizationLevel { get; }
        int ExpansionCapacity { get; }
        int MaximumAttacks { get; }
        long Population { get; }
        float PopulationDensity { get; }
        float Power { get; }
        long Production { get; }
        long Resources { get; }
        float SplitingProbability { get; }
        float TechnologyLevelRate { get; }

        float FaithShrinkingRateBonus { get; set; }
        int PowerBonus { get; set; }

        int GetFaithEmitted();
        void Evolve();
        void Kill(long deads);
        void OnExpanded();
        void OnSkillAdded(ISkill skill);
        void OnSplit(ISociety splitSociety, long totalPopulation, long totalResources);
        void SetInitialPopulation();
    }
}
