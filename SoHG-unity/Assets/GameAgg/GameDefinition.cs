using Sohg.CrossCutting;
using Sohg.GameAgg.Contracts;
using Sohg.GameAgg.Stages;
using UnityEngine;
using Sohg.TechnologyAgg;
using Sohg.TechnologyAgg.Contracts;
using Sohg.GameAgg.Features;

namespace Sohg.GameAgg
{
    [CreateAssetMenu(fileName = "GameDefinition", menuName = "SoHG/Game Definition")]
    public partial class GameDefinition : ScriptableBaseObject, IGameDefinition
    {
        [Header("Game")]
        [SerializeField]
        private GameFeature[] features;
        [SerializeField]
        private GameStage[] stages;
        [SerializeField]
        private TechnologyCategory[] technologyCategories;
        
        [Header("Evolution")]
        [SerializeField]
        private int evolutionActionsTimeInterval;
        
        public IGameFeature[] Features { get { return features; } }
        public IGameStage[] Stages { get { return stages; } }
        public ITechnologyCategory[] TechnologyCategories { get { return technologyCategories; } }

        public int EvolutionActionsTimeInterval { get { return evolutionActionsTimeInterval; } }
    }
}
