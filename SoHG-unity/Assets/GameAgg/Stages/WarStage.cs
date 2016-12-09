using Sohg.GameAgg.Contracts;
using System.Linq;
using UnityEngine;

namespace Sohg.GameAgg.Stages
{
    [CreateAssetMenu(fileName = "WarStage", menuName = "SoHG/Stages/War")]
    public class WarStage : GameStage<IWarPlayable>
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
            game.Log("Hominid war has started!");
        }

        public override void FixedUpdate()
        {
            time++;
            game.RedrawIfChanged();

            if (!game.IsPaused() && (time % game.SohgFactory.Config.WarActionsTimeInterval == 0))
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
               EndWar(false);
            }
            else
            {
                var hasPlayerWon = (game.Species.Count == 1);
                if (hasPlayerWon)
                {
                    EndWar(true);
                }
            }
        }

        private void EndWar(bool hasPlayerWon)
        {
            game.EndWar(hasPlayerWon);
            game.NextStage();
        }
    }
}
