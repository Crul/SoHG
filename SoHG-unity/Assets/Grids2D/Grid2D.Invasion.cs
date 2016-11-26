using Sohg.Grids2D.Contracts;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

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

        public Dictionary<ICell, ICell> GetInvadableCells(ITerritory territory1, ITerritory territory2)
        {
            return ((Territory)territory1).cells
                .Where(cell => !cell.IsInvolvedInAttack)
                .Select(cell => new
                {
                    Cell = cell,
                    Neighbour = CellGetNeighbours(cell)
                        .FirstOrDefault(neighbour => IsCellOfTerritoryInvadable(neighbour, territory2))
                })
                .Where(pair => pair.Neighbour != null)
                .ToDictionary(pair => (ICell)pair.Cell, pair => (ICell)pair.Neighbour);
        }

        public void InvadeTerritory(ICell from, ICell target)
        {
            if (!CanCellBeInvaded(target))
            {
                return;
            }

            var territoryToBeInvaded = territories[target.TerritoryIndex];
            territoryToBeInvaded.cells.Remove((Cell)target);

            var territoryInvader = territories[from.TerritoryIndex];
            SetCellTerritory(target, territoryInvader);

            territoriesHaveChanged = true;

            Debug.Log(string.Format("{0} INVADED {1}", territoryInvader.Society.Name, territoryToBeInvaded.Society.Name));
        }

        private bool CanCellBeInvaded(ICell cell)
        {
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
