using Sohg.GameAgg.Contracts;
using Sohg.SocietyAgg.Contracts;
using System.Linq;
using UnityEngine;

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
            if (society.State.Boats.Count < boatCapacity && Random.Range(0f, 1f) < boatCreationProbability)
            {
                var boatCreationCell = game.Grid.GetCoast(society.Territory)
                    .OrderBy(cell => Random.Range(0f, 1f))
                    .FirstOrDefault();

                if (boatCreationCell != null)
                {
                    game.SohgFactory.CreateBoat(society, boatCreationCell);
                }
            }
        }
    }
}
