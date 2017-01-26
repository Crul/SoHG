using Sohg.GameAgg.Contracts;
using Sohg.SocietyAgg.Contracts;
using System.Linq;
using UnityEngine;

namespace Sohg.GameAgg.Features
{
    [CreateAssetMenu(fileName = "ExpansionFeature", menuName = "SoHG/Features/Expansion")]
    public class Expansion : GameFeature
    {
        private int expansionMarginForOtherFeatures = 0;

        public override void Run(IEvolvableGame game, ISociety society)
        {
            var expansion = society.State.ExpansionCapacity;
            var margin = Random.Range(0, expansionMarginForOtherFeatures + 1);
            if (expansion > margin)
            {
                var hasExpanded = Enumerable.Range(0, expansion - margin)
                    .ToList()
                    .Any(i => ExpandSociety(game, society));

                if (hasExpanded)
                {
                    game.Log(society.Name + " has expanded");
                }
            }

            if (expansion < 0)
            {
                Enumerable.Range(0, System.Math.Abs(expansion))
                    .ToList()
                    .ForEach(i => game.Shrink(society));
            }
        }

        private bool ExpandSociety(IEvolvableGame game, ISociety society)
        {
            var hasBeenExpanded = game.Grid.ExpandSingleCell(society.Territory);
            if (hasBeenExpanded)
            {
                society.State.OnExpanded();
            }

            return hasBeenExpanded;
        }
    }
}
