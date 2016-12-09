namespace Sohg.SocietyAgg.Contracts
{
    public interface ISocietyState
    {
        int MaximumAttacks { get; }
        long PopulationAmount { get; }
        float TechnologyLevelRate { get; }
        float Power { get; }

        int GetFaithEmitted();
        void Kill(float damageRate);
        void OnSkillAdded(ISkill skill);
        void SetInitialPopulation(long initialPopulation);
    }
}
