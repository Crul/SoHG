using UnityEngine;

namespace Sohg.GameAgg
{
    public partial class GameDefinition
    {
        [Header("Attack")]
        [SerializeField]
        [Range(0, 1)]
        private float attackDamageTieRateThreshold;
        [SerializeField]
        private int fightDuration;
        [SerializeField]
        [Range(0, 1)]
        private float friendshipDecreaseOnLooseAttack;
        [SerializeField]
        [Range(0, 1)]
        private float friendshipDecreaseOnStartedAttack;
        [SerializeField]
        [Range(0, 1)]
        private float friendshipDecreaseOnTieAttack;
        [SerializeField]
        [Range(0, 1)]
        private float friendshipDecreaseOnWinAttack;
        [SerializeField]
        [Range(0, 1)]
        private float friendshipRangeBottomThresholdForAttack;
        [SerializeField]
        [Range(0, 1)]
        private float powerBalanceThresholdForAttack;
        [SerializeField]
        [Range(0, 1)]
        private float sameSpeciesWillingToAttackFactor;

        public float AttackDamageTieRateThreshold { get { return attackDamageTieRateThreshold; } }
        public int FightDuration { get { return fightDuration; } }
        public float FriendshipDecreaseOnLooseAttack { get { return friendshipDecreaseOnLooseAttack; } }
        public float FriendshipDecreaseOnStartedAttack { get { return friendshipDecreaseOnStartedAttack; } }
        public float FriendshipDecreaseOnTieAttack { get { return friendshipDecreaseOnTieAttack; } }
        public float FriendshipDecreaseOnWinAttack { get { return friendshipDecreaseOnWinAttack; } }
        public float FriendshipRangeBottomThresholdForAttack { get { return friendshipRangeBottomThresholdForAttack; } }
        public float PowerBalanceThresholdForAttack { get { return powerBalanceThresholdForAttack; } }
        public float SameSpeciesWillingToAttackFactor { get { return sameSpeciesWillingToAttackFactor; } }
    }
}
