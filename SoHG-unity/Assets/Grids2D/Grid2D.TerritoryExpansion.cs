using Sohg.Grids2D.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Grids2D
{
    public partial class Grid2D
    {
        private void ExpandFullTerritory(Territory territory, List<Cell> unassignedCells)
        {
            var initialCell = unassignedCells.First();
            SetCellTerritory(initialCell, territory);
            unassignedCells.Remove(initialCell);

            var expandedCells = 0;
            do
            {
                expandedCells = ExpandTerritory(territory, unassignedCells);
            }
            while (expandedCells > 0);
        }

        private int ExpandTerritory(Territory territory, List<Cell> unassignedCells)
        {
            var cellsToBeExpanded = territory.cells
                .SelectMany(cell => CellGetNeighbours(cell.CellIndex))
                .Distinct()
                .Where(cell => unassignedCells.Contains(cell))
                .ToList();

            cellsToBeExpanded.ForEach(cell =>
            {
                SetCellTerritory(cell, territory);
                unassignedCells.Remove(cell);
            });

            return cellsToBeExpanded.Count;
        }
    }
}
