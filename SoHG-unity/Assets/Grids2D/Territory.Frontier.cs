using System.Collections.Generic;
using System.Linq;

namespace Grids2D
{
    public partial class Territory
    {
        public List<int> FrontierCellIndices { get; private set; }
        public IDictionary<int, List<int>> FrontierCellIndicesByTerritoryIndex { get; private set; }

        public void InitializeFrontier(Grid2D grid)
        {
            FrontierCellIndices = cells
                .Select(cell => cell.CellIndex)
                .Where(cellIndex => grid.CellGetNeighbours(cellIndex)
                    .Any(neighbourCell => neighbourCell.TerritoryIndex != TerritoryIndex))
                .ToList();

            FrontierCellIndicesByTerritoryIndex = grid.territories
                .Where(neighbourTerritory => neighbourTerritory != this)
                .ToDictionary
                (
                    neighbourTerritory => neighbourTerritory.TerritoryIndex,
                    neighbourTerritory => FrontierCellIndices
                        .Where(cell => grid.CellGetNeighbours(cell)
                            .Any(neighbourCell => neighbourCell.TerritoryIndex == neighbourTerritory.TerritoryIndex))
                        .ToList()
                );
        }

        public void UpdateFrontiers(List<Territory> affectedTerritories, List<int> affectedCellIndices, Grid2D grid)
        {
            affectedTerritories
                .Where(affectedTerritory => FrontierCellIndicesByTerritoryIndex.ContainsKey(affectedTerritory.TerritoryIndex))
                .ToList()
                .ForEach(affectedTerritory => RemoveFrontierCells(affectedTerritory, affectedCellIndices));

            var myAffectedCellIndices = affectedCellIndices
                .Where(cellIndex => grid.GetCell(cellIndex).TerritoryIndex == TerritoryIndex);

            myAffectedCellIndices.ToList()
                .ForEach(cellIndex => AddFrontierCell(cellIndex, grid));

            var myAffectedCellFrontierIndices = myAffectedCellIndices
                .Where(cellIndex => grid.CellGetNeighbours(cellIndex)
                    .Any(neighbour => neighbour.TerritoryIndex != TerritoryIndex))
                .ToList();
            
            affectedCellIndices
                .Where(cellIndex => !myAffectedCellFrontierIndices.Contains(cellIndex))
                .ToList()
                .ForEach(cellIndex => FrontierCellIndices.Remove(cellIndex));

            myAffectedCellFrontierIndices
                .Where(cellIndex => !FrontierCellIndices.Contains(cellIndex))
                .ToList()
                .ForEach(cellIndex => FrontierCellIndices.Add(cellIndex));
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
                    if (!FrontierCellIndicesByTerritoryIndex.ContainsKey(territoryIndex))
                    {
                        FrontierCellIndicesByTerritoryIndex.Add(territoryIndex, new List<int>());
                    }
                    FrontierCellIndicesByTerritoryIndex[territoryIndex].Add(territoryAffectedCellIndex);
                });
        }

        private void RemoveFrontierCells(Territory affectedTerritory, List<int> affectedCellIndices)
        {
            var territoryIndex = affectedTerritory.TerritoryIndex;

            FrontierCellIndicesByTerritoryIndex.Values.ToList()
                .ForEach(territoryFrontierCellIndices => 
                {
                    territoryFrontierCellIndices
                        .Where(cellIndex => affectedCellIndices.Contains(cellIndex))
                        .ToList()
                        .ForEach(cellIndex => territoryFrontierCellIndices.Remove(cellIndex));
                });
        }
    }
}
