using Sohg.Grids2D.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Grids2D
{
    public partial class Grid2D : IGrid
    {
        #region circles
        private int[][] evenColumnCircle = new int[7][]
        {   /////////// row  col
            new int[2] { -1,  0 }, // top
            new int[2] {  0, +1 }, // right
            new int[2] { +1, +1 }, // bottom right
            new int[2] { +1,  0 }, // bottom
            new int[2] { +1, -1 }, // bottom left
            new int[2] {  0, -1 }, // left
            new int[2] { -1,  0 }  // top
        };

        private int[][] oddColumnCircle = new int[7][]
        {   /////////// row  col
            new int[2] { -1,  0 }, // top
            new int[2] { -1, +1 }, // top right
            new int[2] {  0, +1 }, // right
            new int[2] { +1,  0 }, // bottom
            new int[2] {  0, -1 }, // left
            new int[2] { -1, -1 }, // top left
            new int[2] { -1,  0 }  // top
        };
        #endregion

        public ICell GetInvadableCell(ICell from, ITerritory territory)
        {
            return CellGetNeighbours(from.CellIndex)
                .Where(neighbour => neighbour.CanBeInvaded && neighbour.TerritoryIndex == territory.TerritoryIndex)
                .FirstOrDefault();
        }

        public bool InvadeTerritory(ICell from, ICell target)
        {
            if (target.IsSocietyTerritory && !CanCellBeInvaded(target))
            {
                // TODO Ring territories are not invaded
                return false;
            }

            if (target.TerritoryIndex > -1)
            {
                var territoryToBeInvaded = territories[target.TerritoryIndex];
                territoryToBeInvaded.cells.Remove((Cell)target);
            }

            var territoryInvader = territories[from.TerritoryIndex];
            SetCellTerritory(target, territoryInvader);

            UpdateFrontiersAfterTerritoryChange(target, from);

            FixNonInvadableTerritories();
            
            territoriesHaveChanged = true;

            return true;
        }

        private bool CanCellBeInvaded(ICell cell)
        {
            if (cell.IsSea)
            {
                return false;
            }

            if (cell.IsNonSocietyTerritory)
            {
                return true;
            }

            // cell cannot be invaded if same-territory-neighbour-cells are NOT contigous
            // because it could cause disconnected territories

            var candidateCell = (Cell)cell;
            var territory = territories[candidateCell.territoryIndex];
            var neighbours = CellGetNeighbours(candidateCell);

            var otherTerritoryNeighbours = neighbours.Where(neighbour => !neighbour.IsSameTerritory(candidateCell));
            var otherTerritoryNeighboursCount = otherTerritoryNeighbours.Count();
            if (otherTerritoryNeighboursCount < 2 || otherTerritoryNeighboursCount > 4)
                return true;

            var circleRowColumn = (candidateCell.column % 2 == 0 ? evenColumnCircle : oddColumnCircle);
            var neighboursInCircularOrder = new Cell[7];
            for (var circleIndex = 0; circleIndex < circleRowColumn.Length; circleIndex++)
            {
                var rowColumn = circleRowColumn[circleIndex];
                AddNeighbour(neighboursInCircularOrder, neighbours, candidateCell, circleIndex, rowColumn[0], rowColumn[1]);
            }

            var valueChanges = 0;
            var currentCell = neighboursInCircularOrder[0];
            var lastValue = (currentCell != null && currentCell.IsSameTerritory(candidateCell));
            for (var i = 1; i < neighboursInCircularOrder.Length; i++)
            {
                currentCell = neighboursInCircularOrder[i];
                var currentValue = (currentCell != null && currentCell.IsSameTerritory(candidateCell));
                if (currentValue != lastValue)
                {
                    valueChanges++;
                }

                if (valueChanges > 2)
                {
                    return false;
                }

                lastValue = currentValue;
            }

            return true;
        }

        private void UpdateFrontiersAfterTerritoryChange(ICell target, ICell from = null)
        {
            var neighbourCells = CellGetNeighbours(target.CellIndex);
            if (from != null)
            {
                neighbourCells = neighbourCells.Concat(CellGetNeighbours(from.CellIndex)).ToList();
            }
            else
            {
                neighbourCells.Add((Cell)target);
            }

            var affectedCells = neighbourCells.Distinct();

            var affectedTerritories = affectedCells
                .Where(cell => cell.IsSocietyTerritory)
                .Select(cell => territories[cell.TerritoryIndex])
                .ToList();

            var affectedCellIndices = affectedCells.Select(cell => cell.CellIndex).ToList();

            affectedTerritories.ForEach(territory =>
                territory.UpdateFrontiers(affectedTerritories, affectedCellIndices, this));

            affectedCells.ToList().ForEach(cell => cell.CanBeInvaded = CanCellBeInvaded(cell));
        }

        private bool IsCellOfTerritoryInvadable(Cell cell, ITerritory territory)
        {
            return cell.CanBeInvaded 
                && !cell.IsInvolvedInAttack
                && cell.territoryIndex == territory.TerritoryIndex;
        }

        private void AddNeighbour(Cell[] cellList,
            List<Cell> neighbours,
            Cell referenceCell,
            int index,
            int rowDelta,
            int columnDelta)
        {
            var neighbour = neighbours
                .FirstOrDefault(cell => cell.column == referenceCell.column + columnDelta
                    && cell.row == referenceCell.row + rowDelta);

            cellList[index] = neighbour;
        }
    }
}
