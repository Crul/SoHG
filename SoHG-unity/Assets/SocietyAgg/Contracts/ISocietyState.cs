using Sohg.Grids2D.Contracts;
using System.Collections.Generic;

namespace Sohg.SocietyAgg.Contracts
{
    public interface ISocietyState
    {
        int BoatCapacity { get; }
        long CivilizationLevel { get; }
        int ExpansionCapacity { get; }
        int MaximumAttacks { get; }
        long Population { get; }
        float PopulationDensity { get; }
        float Power { get; }
        long Production { get; }
        long Resources { get; }
        float SeaMovementCapacity { get; }
        float SplitingProbability { get; }
        float TechnologyLevelRate { get; }

        int BoatCount { get; set; }
        float FaithShrinkingRateBonus { get; set; }
        int PowerBonus { get; set; }

        List<int> GetFaithEmitted(ITerritory territory);
        void Evolve();
        void Kill(long deads);
        void OnExpanded();
        void OnSkillAdded(ISkill skill);
        void OnSplit(ISociety splitSociety, long totalPopulation, long totalResources);
        void SetInitialPopulation();
    }
}
