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
        private ISociety society;
        private float settleProbability = 0.1f;

        public void Initialize(IRunningGame game, ISociety society, ICell boatCreationCell)
        {
            this.game = game;
            this.society = society;
            cell = boatCreationCell;

            GetComponent<Image>().color = society.Color;
            MoveBoat();
        }

        public void FixedUpdate()
        {
            if (game == null || game.IsPaused())
            {
                return;
            }

            if (!TryToSettle())
            {
                MoveBoat();
            }

            // TODO Add Boat return to land (add resources)
            // TODO Add Boat attack
        }

        public bool TryToSettle()
        {
            if (Random.Range(0f, 1f) > settleProbability)
            {
                return false;
            }

            var hasBeenExpanded = game.Grid.SettleFromSea(society, cell);
            if (hasBeenExpanded)
            {
                society.State.Boats.Remove(this);
                ReturnToPool();
            }

            return hasBeenExpanded;
        }

        private void MoveBoat()
        {
            cell = game.Grid.GetSeaNextTo(cell);
            transform.position = cell.WorldPosition;
        }
    }
}
