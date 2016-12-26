using UnityEngine;

namespace Sohg.GameAgg
{
    public partial class GameDefinition
    {
        [Header("Attack")]
        [SerializeField]
        private float attackDamageTieRateThreshold;
        [SerializeField]
        private int fightDuration;
        [SerializeField]
        private float friendshipRangeBottomThresholdForAttack;
        [SerializeField]
        private float powerBalanceThresholdForAttack;

        public float AttackDamageTieRateThreshold { get { return attackDamageTieRateThreshold; } }
        public int FightDuration { get { return fightDuration; } }
        public float FriendshipRangeBottomThresholdForAttack { get { return friendshipRangeBottomThresholdForAttack; } }
        public float PowerBalanceThresholdForAttack { get { return powerBalanceThresholdForAttack; } }
    }
}
