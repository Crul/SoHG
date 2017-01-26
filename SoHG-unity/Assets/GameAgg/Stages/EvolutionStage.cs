using Sohg.GameAgg.Contracts;
using System.Linq;
using UnityEngine;
using Sohg.Grids2D.Contracts;
using Sohg.SocietyAgg.Contracts;

namespace Sohg.GameAgg.Stages
{
    [CreateAssetMenu(fileName = "EvolutionStage", menuName = "SoHG/Stages/Evolution")]
    public class EvolutionStage : GameStage<IEvolvableGame>
    {
        private int time;

        private int timeForOneYearStep = 2000000;
        private int timeDecelerationAmortiguation = 1000;

        public override void OnTerritoryClick(ITerritory territory)
        {
            if (territory.Society == null)
            {
                game.CloseSocietyInfo();
            }
            else
            {
                game.OpenSocietyInfo(territory.Society);
            }
        }

        public override void Start()
        {
            time = 0;

            game.GameInfoPanel.EnableTechnologyTree();

            game.OpenInstructions(
                    "The Stories begin" + System.Environment.NewLine +
                    "The human species has born and they are not alone." + System.Environment.NewLine +
                    System.Environment.NewLine +
                    "Good luck!");

            game.Log("Hominid evolution has started!");
        }

        public override void FixedUpdate()
        {
            time++;

            if (!game.IsPaused() && (time % game.GameDefinition.EvolutionActionsTimeInterval == 0))
            {
                game.Societies.ForEach(society => RunFeatures(society));

                game.Year += (timeForOneYearStep) / (game.Year + timeDecelerationAmortiguation);

                CheckWinOrLoose();
            }
        }

        private void RunFeatures(ISociety society)
        {
            var features = game.GameDefinition.Features.ToList();
            for (var featureIndex = 0; featureIndex < features.Count; featureIndex++)
            {
                if (society.IsDead)
                {
                    game.Kill(society);
                    return;
                }
                else
                {
                    features[featureIndex].Run(game, society);
                }
            }
        }

        private void CheckWinOrLoose()
        {
            if (game.IsPlayerDead)
            {
                Finish(false);
            }
        }

        private void Finish(bool hasPlayerWon)
        {
            game.EndGame.Show(hasPlayerWon);
            game.Log("End game!");
        }
    }
}
