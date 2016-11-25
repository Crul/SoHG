using Sohg.Grids2D.Contracts;
using Sohg.GameAgg.Contracts;
using UnityEngine;

namespace Sohg.GameAgg.Stages
{
    [CreateAssetMenu(fileName = "SelectStartStage", menuName = "SoHG/Stages/Select Start")]
    public class SelectStartStage : GameStage<ISelectStartPlayable>
    {
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
            }
        }
    }
}
