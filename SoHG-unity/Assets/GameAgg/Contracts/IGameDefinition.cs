using Sohg.TechnologyAgg.Contracts;
using Sohg.SocietyAgg.Contracts;
using Sohg.SpeciesAgg.Contracts;
using UnityEngine;

namespace Sohg.GameAgg.Contracts
{
    public interface IGameDefinition
    {
        IGameFeature[] Features { get; }
        IGameStage[] Stages { get; }
        ITechnologyCategory[] TechnologyCategories { get; }

        int BoardColumns { get; }
        int BoardRows { get; }
        Texture2D BoardBackground { get; }
        Texture2D BoardMask { get; }

        ISpecies PlayerSpecies { get; }
        ISpecies[] NonPlayerSpecies { get; }
        int NonPlayerSocietyCount { get; }
        int InitialSocietyPopulationLimit { get; }
        ISkill[] Skills { get; }
        ISocietyAction[] SocietyActions { get; }
        float FriendshipChangeRate { get; }
        float InitialFriendshipRange { get; }

        int EvolutionActionsTimeInterval { get; }

        float AttackDamageTieRateThreshold { get; }
        int FightDuration { get; }
        float FriendshipDecreaseOnLooseAttack { get; }
        float FriendshipDecreaseOnStartedAttack { get; }
        float FriendshipDecreaseOnTieAttack { get; }
        float FriendshipDecreaseOnWinAttack { get; }
        float FriendshipRangeBottomThresholdForAttack { get; }
        float PowerBalanceThresholdForAttack { get; }
        float SameSpeciesWillingToAttackFactor { get; }
    }
}
