using System.Collections;
using Sohg.GameAgg.Contracts;
using Sohg.GameAgg.UI;
using Sohg.SocietyAgg.Contracts;
using System.Linq;

namespace Sohg.GameAgg
{
    public partial class GameEngine : IRunningGame
    {
        public void ExecuteAction(IEnumerator actionExecution)
        {
            StartCoroutine(actionExecution);
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

        public void OnTechnologyActivated(ITechnology technology)
        {
            ConsumeFaith(technology.FaithCost);

            // TODO make society action technology requirement properly
            var activatedSocietyActions = GameDefinition.SocietyActions
                .Where(action => action.Requires(technology));

            activatedSocietyActions.ToList()
                .ForEach(action => societyInfo.AddAction(action));
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
