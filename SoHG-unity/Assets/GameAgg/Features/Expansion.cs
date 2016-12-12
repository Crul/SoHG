using Sohg.GameAgg.Contracts;
using Sohg.Grids2D.Contracts;
using Sohg.SocietyAgg.Contracts;
using System.Linq;
using UnityEngine;

namespace Sohg.GameAgg.Features
{
    [CreateAssetMenu(fileName = "ExpansionFeature", menuName = "SoHG/Features/Expansion")]
    public class Expansion : GameFeature
    {
        public override void Run(IEvolvableGame game, ISociety society)
        {
            Enumerable.Range(0, society.State.ExpansionCapacity)
                .ToList()
                .ForEach(i => ExpandSociety(game.Grid, society));
        }

        public void ExpandSociety(IGrid grid, ISociety society)
        {
            var hasBeenExpanded = grid.ExpandSingleCell(society.Territory);
            if (hasBeenExpanded)
            {
                society.State.Expanded();
                Debug.Log(society.Name + " has expanded");
            }
        }
    }
}
