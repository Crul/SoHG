using Sohg.Grids2D.Contracts;
using System.Collections.Generic;

namespace Sohg.SocietyAgg.Contracts
{
    public interface ISocietyState
    {
        float AggressivityRate { get; }
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
        
        float FaithShrinkingRateBonus { get; set; }
        int PowerBonus { get; set; }

        int BoatCapacity { get; }
        List<IBoat> Boats { get; }
        
        List<int> GetFaithEmitted(ITerritory territory);
        void Evolve();
        void InheritState(ISociety originSociety);
        void Kill(long deads);
        void OnExpanded();
        void OnSkillAdded(ISkill skill);
        void OnSplit(ISociety splitSociety, long totalPopulation, long totalResources);
        void SetInitialPopulation(float initialPopulationDensity);
    }
}
