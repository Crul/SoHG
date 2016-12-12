using Sohg.Grids2D.Contracts;
using Sohg.GameAgg.Contracts;
using UnityEngine;

namespace Sohg.GameAgg.Stages
{
    [CreateAssetMenu(fileName = "SelectStartStage", menuName = "SoHG/Stages/Select Start")]
    public class SelectStartStage : GameStage<ISelectStartPlayable>
    {
        public override void Start()
        {
            game.ResetGame();

            game.OpenInstructions
            (
                "The human species are just going to start existing, and it's not the only hominid species in the world." + System.Environment.NewLine +
                System.Environment.NewLine +
                "First of all, select the start point for the humans."
            )
            .OnClose(() => game.Grid.SetGridSelectionToCell());

            game.Log("Select start point");
        }

        public override void OnCellClick(ICell cell)
        {
            if (!game.IsPaused())
            {
                SetStartPoints(game, cell);
            }
        }

        private void SetStartPoints(ISelectStartPlayable game, ICell cell)
        {
            if (cell.IsNonSocietyTerritory)
            {
                game.Grid.SetGridSelectionToNone();
                game.CreateSocieties(cell);
                game.NextStage();
            }
        }
    }
}
