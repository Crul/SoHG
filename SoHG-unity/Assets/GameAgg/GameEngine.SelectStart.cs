using Sohg.GameAgg.Contracts;
using Sohg.Grids2D.Contracts;
using System.Linq;

namespace Sohg.GameAgg
{
    public partial class GameEngine : ISelectStartPlayable
    {
        public void CreateSocieties(ICell humanInitialCell)
        {
            SohgFactory.CreateSociety(this, GameDefinition.PlayerSpecies, humanInitialCell);

            var nonPlayerSpeciesCount = GameDefinition.NonPlayerSpecies.Length;
            for (var i = 0; i < GameDefinition.NonPlayerSocietyCount; i++)
            {
                var species = GameDefinition.NonPlayerSpecies[i % (nonPlayerSpeciesCount - 1)];
                SohgFactory.CreateSociety(this, species);
            }

            Grid.ExpandSocietiesTerritories(GameDefinition.InitialSocietyPopulationLimit);

            Species.SelectMany(species => species.Societies).ToList()
                .ForEach(society => society.Initialize());
        }
    }
}
