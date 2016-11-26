using Sohg.GameAgg.Contracts;

namespace Sohg.GameAgg
{
    public partial class GameEngine : IRunningGame
    {
        public bool IsPaused()
        {
            return false;
        }

        public void NextStage()
        {
            currentStageIndex++;
            if (currentStageIndex >= gameDefinition.Stages.Length)
            {
                throw new System.Exception("GameEngine.NextStage() - Not enough stages");
            }

            currentStage = gameDefinition.Stages[currentStageIndex];
            currentStage.SetGame(this);
            currentStage.Start();
        }
    }
}
