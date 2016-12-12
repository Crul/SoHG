using Sohg.GameAgg.Contracts;
using Sohg.Grids2D.Contracts;

namespace Sohg.GameAgg
{
    public partial class GameEngine
    {
        private int currentStageIndex;
        private IGameStage currentStage;

        public void Start()
        {
            Grid.InitializeBoard(SohgFactory);
            currentStageIndex = -1;
            NextStage();
        }

        public void FixedUpdate()
        {
            if (currentStage != null)
            {
                currentStage.FixedUpdate();
                Grid.RedrawIfChanged();
            }
        }

        public void Update()
        {
            if (!IsPaused() && PlayerSpecies != null)
            {
                GameInfoPanel.GameStatusInfo.SetValues(this);
            }
        }

        private void OnGridCellClick(ICell cell)
        {
            if (currentStage != null)
            {
                currentStage.OnCellClick(cell);
            }
        }

        private void OnGridTerritoryClick(ITerritory territory)
        {
            if (currentStage != null)
            {
                currentStage.OnTerritoryClick(territory);

            }
        }
    }
}
