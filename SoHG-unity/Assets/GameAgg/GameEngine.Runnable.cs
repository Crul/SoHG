using Sohg.GameAgg.Contracts;
using Sohg.Grids2D.Contracts;

namespace Sohg.GameAgg
{
    public partial class GameEngine : IRunnableGame
    {
        private int currentStageIndex;
        private IGameStage currentStage;

        public void Start()
        {
            grid.InitializeBoard(SohgFactory);
            currentStageIndex = -1;
            NextStage();
        }

        public void FixedUpdate()
        {
            if (currentStage != null)
                currentStage.FixedUpdate();
        }
        
        private void OnGridCellClick(ICell cell)
        {
            if (currentStage != null)
                currentStage.OnCellClick(cell);
        }
    }
}
