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
        private int currentSocietyIndex;

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
            currentSocietyIndex = 0;

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
            var hasPlayerLosen = (game.PlayerSpecies.Societies.Count == 0);
            if (hasPlayerLosen)
            {
                Finish(false);
            }
            else
            {
                // TODO: this should be enough (but is not): var hasPlayerWon = (game.Species.Count == 1);
                var hasPlayerWon = (game.Species.Count(species => species.Societies.Sum(society => society.Territory.CellCount) > 0) == 1);
                if (hasPlayerWon)
                {
                    Finish(true);
                }
            }
        }

        private void Finish(bool hasPlayerWon)
        {
            game.FinishEvolution(hasPlayerWon);
            game.NextStage();
        }
    }
}
