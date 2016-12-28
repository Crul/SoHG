using Sohg.GameAgg.Contracts;
using Sohg.SocietyAgg.Contracts;
using UnityEngine;

namespace Sohg.GameAgg.Features
{
    [CreateAssetMenu(fileName = "FaithEmissionFeature", menuName = "SoHG/Features/Faith Emission")]
    public class FaithEmission : GameFeature
    {
        public override void Run(IEvolvableGame game, ISociety society)
        {
            if (society.Species == game.PlayerSpecies)
            {
                EmitFaith(game, society);
            }
        }

        public void EmitFaith(IEvolvableGame game, ISociety society)
        {
            society.Territories.ForEach(territory =>
            {
                society.State.GetFaithEmitted(territory).ForEach(faithAmount =>
                {
                    var faithCell = game.Grid
                        .GetRandomCell(cell => cell.TerritoryIndex == territory.TerritoryIndex);

                    game.SohgFactory.CreateFaith(society, faithCell, faithAmount);
                });
            });
        }
    }
}
