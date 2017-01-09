using Sohg.CrossCutting.Pooling;
using Sohg.GameAgg.Contracts;
using Sohg.Grids2D.Contracts;
using Sohg.SocietyAgg.Contracts;
using UnityEngine;
using UnityEngine.UI;

namespace Sohg.SocietyAgg.UI
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Image))]
    public class Boat : PooledObject, IBoat
    {
        private ICell cell;
        private IRunningGame game;
        private int navigationDuration;
        private ISociety society;

        public void Initialize(IRunningGame game, ISociety society, ICell boatCreationCell)
        {
            this.game = game;
            this.society = society;
            cell = boatCreationCell;
            navigationDuration = 0;

            GetComponent<Image>().color = society.Color;
            MoveBoat();
        }

        public void FixedUpdate()
        {
            MoveBoat();
            navigationDuration++;

            // TODO Add Boat return to land
            // TODO Add Boat attack
            // TODO Add Boat expansion
        }

        private void MoveBoat()
        {
            cell = game.Grid.GetSeaNextTo(cell);
            transform.position = cell.WorldPosition;
        }
    }
}
