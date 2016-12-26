using Sohg.TechnologyAgg.Contracts;
using Sohg.SocietyAgg.Contracts;
using Sohg.SpeciesAgg.Contracts;

namespace Sohg.GameAgg.Contracts
{
    public interface IGameDefinition
    {
        IGameFeature[] Features { get; }
        IGameStage[] Stages { get; }
        ITechnologyCategory[] TechnologyCategories { get; }

        ISpecies PlayerSpecies { get; }
        ISpecies[] NonPlayerSpecies { get; }
        int NonPlayerSocietyCount { get; }
        ISkill[] Skills { get; }
        ISocietyAction[] SocietyActions { get; }

        int InitialSocietyPopulationLimit { get; }

        int EvolutionActionsTimeInterval { get; }

        float AttackDamageTieRateThreshold { get; }
        int FightDuration { get; }
        float FriendshipRangeBottomThresholdForAttack { get; }
        float PowerBalanceThresholdForAttack { get; }
    }
}
