using System.Collections;
using Sohg.GameAgg.Contracts;
using Sohg.SocietyAgg.Contracts;

namespace Sohg.GameAgg
{
    public partial class GameEngine : IRunningGame
    {
        public IGameDefinition Definition { get { return gameDefinition; } }

        public void ExecuteAction(IEnumerator actionExecution)
        {
            StartCoroutine(actionExecution);
        }

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

        public void OpenSocietyInfo(ISociety society)
        {
            societyInfo.Show(society);
        }
    }
}
