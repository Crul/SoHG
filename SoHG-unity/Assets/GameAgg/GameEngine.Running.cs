using System.Collections;
using Sohg.GameAgg.Contracts;
using Sohg.SocietyAgg.Contracts;
using System.Linq;
using Sohg.TechnologyAgg.Contracts;
using System.Collections.Generic;

namespace Sohg.GameAgg
{
    public partial class GameEngine : IRunningGame
    {
        public void ActivateActions()
        {
            gameDefinition.SocietyActions.ToList()
                .ForEach(action => action.CheckActivation(this));
        }
        
        public void ExecuteAction(IEnumerator actionExecution)
        {
            StartCoroutine(actionExecution);
        }

        public List<ITechnology> GetActiveTechnologies()
        {
            return gameDefinition.TechnologyCategories.SelectMany
                (
                    technologyCategory => technologyCategory.Technologies
                        .Where(technology => technology.IsActive)
                ).ToList();
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
            if (currentStageIndex >= gameDefinition.Stages.Length)
            {
                throw new System.Exception("GameEngine.NextStage() - Not enough stages");
            }

            currentStage = gameDefinition.Stages[currentStageIndex];
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
            societyInfo.Show(society);
        }
    }
}
