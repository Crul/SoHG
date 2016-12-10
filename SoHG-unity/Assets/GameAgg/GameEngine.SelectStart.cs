using Sohg.GameAgg.Contracts;
using Sohg.Grids2D.Contracts;
using Sohg.SocietyAgg.Contracts;
using Sohg.SpeciesAgg.Contracts;
using System;
using System.Linq;

namespace Sohg.GameAgg
{
    public partial class GameEngine : ISelectStartPlayable
    {
        public void CreateSocieties(ICell humanInitialCell)
        {
            CreateSociety(gameDefinition.PlayerSpecies, humanInitialCell);

            var nonPlayerSpeciesCount = gameDefinition.NonPlayerSpecies.Length;
            for (var i = 0; i < SohgFactory.Config.NonPlayerSocietyCount; i++)
            {
                var species = gameDefinition.NonPlayerSpecies[i % (nonPlayerSpeciesCount - 1)];
                CreateSociety(species);
            }

            var initialTerritorySize = Convert.ToInt16(SohgFactory.Config.InitialSocietyPopulationLimit 
                / SohgFactory.Config.InitialSocietyPopulationByCell);

            grid.ExpandSocietiesTerritories(initialTerritorySize);

            Species.SelectMany(species => species.Societies).ToList()
                .ForEach(society => society.Initialize());
        }

        public void SetGridSelectionToCell()
        {
            grid.SetGridSelectionToCell();
        }

        public void SetGridSelectionToNone()
        {
            grid.SetGridSelectionToNone();
        }

        private void CreateSociety(ISpecies species, params ICell[] initialCells)
        {
            SohgFactory.CreateSociety(this, species, initialCells);
        }
    }
}
