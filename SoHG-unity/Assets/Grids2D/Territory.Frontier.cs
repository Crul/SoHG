using System.Collections.Generic;
using System.Linq;

namespace Grids2D
{
    public partial class Territory
    {
        public IDictionary<int, List<int>> FrontierCellsByTerritoryIndex { get; private set; }

        public void InitializeFrontier(Grid2D grid)
        {
            FrontierCellsByTerritoryIndex = grid.territories
                .Where(neighbour => neighbour.TerritoryIndex != TerritoryIndex)
                .ToDictionary
                (
                    neighbour => neighbour.TerritoryIndex,
                    neighbour => neighbour.cells
                        .SelectMany(cell => grid.CellGetNeighbours(cell))
                        .Where(cell => cell.TerritoryIndex == TerritoryIndex)
                        .Distinct()
                        .Select(cell => cell.CellIndex)
                        .ToList()
                );
        }

        public void UpdateFrontiers(List<Territory> affectedTerritories, List<int> affectedCellIndices, Grid2D grid)
        {
            affectedTerritories
                .ForEach(affectedTerritory => RemoveFrontierCells(affectedTerritory, affectedCellIndices));

            affectedCellIndices
                .Where(cellIndex => grid.GetCell(cellIndex).TerritoryIndex == TerritoryIndex)
                .ToList()
                .ForEach(territoryAffectedCellIndex => AddFrontierCell(territoryAffectedCellIndex, grid));
        }


        private void AddFrontierCell(int territoryAffectedCellIndex, Grid2D grid)
        {
            grid.CellGetNeighbours(territoryAffectedCellIndex)
                .Select(affectedCell => affectedCell.TerritoryIndex)
                .Where(territoryIndex => territoryIndex != TerritoryIndex)
                .Distinct()
                .ToList()
                .ForEach(territoryIndex =>
                {
                    if (!FrontierCellsByTerritoryIndex.ContainsKey(territoryIndex))
                    {
                        FrontierCellsByTerritoryIndex.Add(territoryIndex, new List<int>());
                    }
                    FrontierCellsByTerritoryIndex[territoryIndex].Add(territoryAffectedCellIndex);
                });
        }

        private void RemoveFrontierCells(Territory affectedTerritory, List<int> affectedCellIndices)
        {
            var affectedTerritoryIndex = affectedTerritory.TerritoryIndex;
            if (FrontierCellsByTerritoryIndex.ContainsKey(affectedTerritoryIndex))
            {
                FrontierCellsByTerritoryIndex[affectedTerritoryIndex] = FrontierCellsByTerritoryIndex[affectedTerritoryIndex]
                    .Where(cellIndex => !affectedCellIndices.Contains(cellIndex))
                    .ToList();
            }
        }
    }
}
