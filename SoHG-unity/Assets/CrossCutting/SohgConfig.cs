using Sohg.CrossCutting.Contracts;
using UnityEngine;

namespace Sohg.CrossCutting
{
    [CreateAssetMenu(fileName = "SohgConfig", menuName = "SoHG/Config")]
    public class SohgConfig : ScriptableBaseObject, ISohgConfig
    {
        [Header("Game")]
        [SerializeField]
        private int nonPlayerSocietyCount;
        [SerializeField]
        private long initialPopulationByCell;
        [SerializeField]
        private int initialSocietyPopulationLimit;

        [Header("Evolution")]
        [SerializeField]
        private int evolutionActionsTimeInterval;

        [Header("Attack")]
        [SerializeField]
        private float attackDamageTieRateThreshold;
        [SerializeField]
        private int fightDuration;
        [SerializeField]
        private float friendshipRangeBottomThresholdForAttack;
        [SerializeField]
        private float powerBalanceThresholdForAttack;

        public int NonPlayerSocietyCount { get { return nonPlayerSocietyCount; } }
        public long InitialPopulationByCell { get { return initialPopulationByCell; } }
        public int InitialSocietyPopulationLimit { get { return initialSocietyPopulationLimit; } }

        public int EvolutionActionsTimeInterval { get { return evolutionActionsTimeInterval; } }

        public float AttackDamageTieRateThreshold { get { return attackDamageTieRateThreshold; } }
        public int FightDuration { get { return fightDuration; } }
        public float FriendshipRangeBottomThresholdForAttack { get { return friendshipRangeBottomThresholdForAttack; } }
        public float PowerBalanceThresholdForAttack { get { return powerBalanceThresholdForAttack; } }
    }
}
