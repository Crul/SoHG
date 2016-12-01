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
            game.OpenInstructions(
                "The human species are just going to start existing, and it's not the only hominid species in the world." + System.Environment.NewLine +
                System.Environment.NewLine +
                "First of all, select the start point for the humans.");
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
            if (cell.IsSocietyUnassigned)
            {
                game.CreateSocieties(cell);
                game.NextStage();
            }
        }
    }
}
