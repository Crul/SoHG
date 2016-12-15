using Sohg.GameAgg.Contracts;
using Sohg.Grids2D.Contracts;
using System;
using System.Linq;

namespace Sohg.GameAgg
{
    public partial class GameEngine : ISelectStartPlayable
    {
        public void CreateSocieties(ICell humanInitialCell)
        {
            SohgFactory.CreateSociety(this, gameDefinition.PlayerSpecies, humanInitialCell);

            var nonPlayerSpeciesCount = gameDefinition.NonPlayerSpecies.Length;
            for (var i = 0; i < SohgFactory.Config.NonPlayerSocietyCount; i++)
            {
                var species = gameDefinition.NonPlayerSpecies[i % (nonPlayerSpeciesCount - 1)];
                SohgFactory.CreateSociety(this, species);
            }
            
            Grid.ExpandSocietiesTerritories(SohgFactory.Config.InitialSocietyPopulationLimit);

            Species.SelectMany(species => species.Societies).ToList()
                .ForEach(society => society.Initialize());
        }
    }
}
