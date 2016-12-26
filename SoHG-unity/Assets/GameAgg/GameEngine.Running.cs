using Sohg.GameAgg.Contracts;
using Sohg.SocietyAgg.Contracts;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

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
                || instructions.IsOpened();
        }

        public void Log(string log, params object[] logParams)
        {
            GameInfoPanel.LogOutput(string.Format(log, logParams));
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
            instructions.Show(instructionsText);

            return instructions;
        }

        public void OpenSocietyInfo(ISociety society)
        {
            if (societyInfo.Society == society)
            {
                CloseSocietyInfo();
            }
            else
            {
                societyInfo.Show(society);
            }
        }

        public void CloseSocietyInfo()
        {
            societyInfo.Hide();
        }
    }
}
