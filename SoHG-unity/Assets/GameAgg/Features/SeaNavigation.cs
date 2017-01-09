using Sohg.GameAgg.Contracts;
using Sohg.SocietyAgg.Contracts;
using System.Linq;
using UnityEngine;
using Sohg.Grids2D.Contracts;

namespace Sohg.GameAgg.Features
{
    [CreateAssetMenu(fileName = "SeaNavigationFeature", menuName = "SoHG/Features/Sea Navigation")]
    public class SeaNavigation : GameFeature
    {
        private float boatCreationProbability = 1; // 0.1f;

        public override void Run(IEvolvableGame game, ISociety society)
        {
            if (society.State.SeaMovementCapacity == 0)
            {
                return;
            }

            var boatCapacity = society.State.BoatCapacity;
            if (society.State.BoatCount < boatCapacity && Random.Range(0f, 1f) < boatCreationProbability)
            {
                var boatCreationCell = society.Territories
                    .SelectMany(territory => game.Grid.GetCoast(territory))
                    .OrderBy(cell => Random.Range(0f, 1f))
                    .FirstOrDefault();

                if (boatCreationCell != null)
                {
                    CreateBoat(game, society, boatCreationCell);
                }
            }
        }

        private void CreateBoat(IEvolvableGame game, ISociety society, ICell boatCreationCell)
        {
            game.SohgFactory.CreateBoat(society, boatCreationCell);
            society.State.BoatCount++;
        }
    }
}
