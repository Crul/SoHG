using Sohg.SpeciesAgg.Contracts;

namespace Sohg.GameAgg
{
    public partial class GameEngine
    {
        public void EmitFaith(ISpecies species)
        {
            species.Societies.ForEach(society =>
            {
                var faithAmount = society.State.GetFaithEmitted();
                if (faithAmount > 0)
                {
                    var faithCell = grid
                        .GetRandomCell(cell => cell.TerritoryIndex == society.Territory.TerritoryIndex);

                    SohgFactory.CreateFaith(this, faithCell, faithAmount);
                }
            });
        }
    }
}
