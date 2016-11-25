using Sohg.CrossCutting;
using Sohg.GameAgg.Contracts;
using Sohg.GameAgg.Stages;
using UnityEngine;

namespace Sohg.GameAgg.Definition
{
    [CreateAssetMenu(fileName = "GameDefinition", menuName = "SoHG/Game Definition")]
    public class GameDefinitionScript : ScriptableBaseObject, IGameDefinition
    {
        [SerializeField]
        private GameStageScript[] stages;

        public IGameStage[] Stages { get { return stages; } }
    }
}
