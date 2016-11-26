using Sohg.CrossCutting.Contracts;
using UnityEngine;

namespace Sohg.CrossCutting
{
    [CreateAssetMenu(fileName = "SohgConfig", menuName = "SoHG/Config")]
    public class SohgConfigScript : ScriptableBaseObject, ISohgConfig
    {
        [Header("Game")]
        [SerializeField]
        private int nonPlayerSocietyCount;
        [SerializeField]
        private long initialSocietyPopulationByCell;
        [Header("Attack")]
        [SerializeField]
        private float friendshipRangeBottomThresholdForAttack;
        [SerializeField]
        private float powerBalanceThresholdForAttack;

        public int NonPlayerSocietyCount { get { return nonPlayerSocietyCount; } }
        public long InitialSocietyPopulationByCell { get { return initialSocietyPopulationByCell; } }
        
        public float FriendshipRangeBottomThresholdForAttack { get { return friendshipRangeBottomThresholdForAttack; } }
        public float PowerBalanceThresholdForAttack { get { return powerBalanceThresholdForAttack; } }
    }
}
