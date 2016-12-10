using Sohg.GameAgg.Contracts;
using System.Linq;
using UnityEngine;

namespace Sohg.GameAgg.Stages
{
    [CreateAssetMenu(fileName = "EvolutionStage", menuName = "SoHG/Stages/Evolution")]
    public class EvolutionStage : GameStage<IEvolvableGame>
    {
        private int time;
        private int currentSocietyIndex;

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
            game.RedrawIfChanged();

            if (!game.IsPaused() && (time % game.SohgFactory.Config.EvolutionActionsTimeInterval == 0))
            {
                var societies = game.Species.SelectMany(species => species.Societies).ToList();
                if (currentSocietyIndex >= societies.Count)
                {
                    currentSocietyIndex = 0;
                }

                var society = societies[currentSocietyIndex];
                society.Species.Evolve(game);
                society.Evolve(game);

                if (society.Species == game.PlayerSpecies)
                {
                    game.EmitFaith(society);
                }

                currentSocietyIndex++;

                CheckWinOrLoose();
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
                // TODO: this should be enough (is not): var hasPlayerWon = (game.Species.Count == 1);
                var hasPlayerWon = (game.Species.Count(species =>  species.Societies.Sum(society => society.Territory.CellCount) > 0) == 1);
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
