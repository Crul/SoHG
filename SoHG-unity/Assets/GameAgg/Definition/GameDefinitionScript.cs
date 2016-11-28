using Sohg.CrossCutting;
using Sohg.GameAgg.Contracts;
using Sohg.GameAgg.Stages;
using Sohg.SocietyAgg.Contracts;
using UnityEngine;
using Sohg.SocietyAgg;
using Sohg.SocietyAgg.Actions;

namespace Sohg.GameAgg.Definition
{
    [CreateAssetMenu(fileName = "GameDefinition", menuName = "SoHG/Game Definition")]
    public class GameDefinitionScript : ScriptableBaseObject, IGameDefinition
    {
        [SerializeField]
        private GameStageScript[] stages;
        
        [Header("Society")]
        [SerializeField]
        private SocietyDefinitionScript playerSociety;
        [SerializeField]
        private SocietyDefinitionScript nonPlayerSociety;
        [Space]
        [SerializeField]
        private SocietyAction[] societyActions;

        public IGameStage[] Stages { get { return stages; } }
        public ISocietyDefinition PlayerSociety { get { return playerSociety; } }
        public ISocietyDefinition NonPlayerSociety { get { return nonPlayerSociety; } }
        public ISocietyAction[] SocietyActions { get { return societyActions; } }
    }
}
