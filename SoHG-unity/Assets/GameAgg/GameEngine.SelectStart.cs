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

            var initialTerritorySize = Convert.ToInt16(SohgFactory.Config.InitialSocietyPopulationLimit 
                / SohgFactory.Config.InitialPopulationByCell);

            Grid.ExpandSocietiesTerritories(initialTerritorySize);

            Species.SelectMany(species => species.Societies).ToList()
                .ForEach(society => society.Initialize());
        }

        public void SetGridSelectionToCell()
        {
            Grid.SetGridSelectionToCell();
        }

        public void SetGridSelectionToNone()
        {
            Grid.SetGridSelectionToNone();
        }
    }
}
