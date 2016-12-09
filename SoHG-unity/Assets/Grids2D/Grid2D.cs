using Sohg.Grids2D.Contracts;
using System;
using System.Linq;

namespace Grids2D
{
    public partial class Grid2D : IGrid
    {
        private bool territoriesHaveChanged = false;

        public void AddTerritory(ITerritory territory, params ICell[] cells)
        {
            territories.Add((Territory)territory);
            _numTerritories++;
            cells.ToList().ForEach(cell => SetCellTerritory(cell, territory));
            TexturizeTerritory(territory.TerritoryIndex);
        }

        public ICell GetRandomCell(Func<ICell, bool> cellFilter)
        {
            var filteredCells = cells.Where(cell => cellFilter(cell));
            var randomIndex = UnityEngine.Random.Range(0, filteredCells.Count() - 1);

            return filteredCells.ElementAt(randomIndex);
        }

        public void SetGridSelectionToNone()
        {
            highlightMode = HIGHLIGHT_MODE.None;
        }

        public void SetGridSelectionToCell()
        {
            highlightMode = HIGHLIGHT_MODE.Cells;
        }
    }
}
