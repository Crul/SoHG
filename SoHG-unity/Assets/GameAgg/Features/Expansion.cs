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
            var expansion = Random.Range(0, society.State.ExpansionCapacity + 1);
            Enumerable.Range(0, expansion)
                .ToList()
                .ForEach(i => ExpandSociety(game, society));
        }

        public void ExpandSociety(IEvolvableGame game, ISociety society)
        {
            var hasBeenExpanded = game.Grid.ExpandSingleCell(society.Territory);
            if (hasBeenExpanded)
            {
                society.State.Expanded();
                game.Log(society.Name + " has expanded");
            }
        }
    }
}
