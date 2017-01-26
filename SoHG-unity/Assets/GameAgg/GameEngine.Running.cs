using Sohg.GameAgg.Contracts;
using Sohg.SocietyAgg.Contracts;
using System.Collections;
using UnityEngine;

namespace Sohg.GameAgg
{
    public partial class GameEngine : IRunningGame
    {
        public Coroutine ExecuteRoutine(IEnumerator actionExecution)
        {
            return StartCoroutine(actionExecution);
        }

        public bool IsPaused()
        {
            // TODO fix pause, now is not working with Fight animation (and execution)
            return GameInfoPanel.PausedPanel.IsVisible() 
                || GameInfoPanel.TechnologyPanel.IsVisible()
                || Instructions.IsOpened();
        }

        public void Log(string log, params object[] logParams)
        {
            var logInfo = string.Format("{0}: {1}", 
                GameInfoPanel.GameStatusInfo.DisplayingYear, 
                string.Format(log, logParams));

            GameInfoPanel.LogOutput(logInfo);
        }

        public void NextStage()
        {
            currentStageIndex++;
            if (currentStageIndex >= GameDefinition.Stages.Length)
            {
                throw new System.Exception("GameEngine.NextStage() - Not enough stages");
            }

            currentStage = GameDefinition.Stages[currentStageIndex];
            currentStage.SetGame(this);
            currentStage.Start();
        }

        public IInstructions OpenInstructions(string instructionsText)
        {
            Instructions.Show(instructionsText);

            return Instructions;
        }

        public void OpenSocietyInfo(ISociety society)
        {
            if (SocietyInfo.Society == society)
            {
                CloseSocietyInfo();
            }
            else
            {
                SocietyInfo.Show(society);
            }
        }

        public void CloseSocietyInfo()
        {
            SocietyInfo.Hide();
        }
    }
}
