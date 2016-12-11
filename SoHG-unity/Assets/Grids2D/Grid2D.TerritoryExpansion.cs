using Sohg.GameAgg.Contracts;
using Sohg.Grids2D.Contracts;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Grids2D
{
    public partial class Grid2D
    {
        public void ExpandSocietiesTerritories(int initialTerritorySizeLimit)
        {
            var unassignedSocietyCells = nonSocietyTerritories
                .SelectMany(territory => ((Territory)territory).cells)
                .ToList();

            var societyTerritories = cells.Where(cell => cell.IsSocietyTerritory)
                .Select(cell => (Territory)GetTerritory(cell)).Distinct().ToList();

            int expadedCellCount;
            do
            {
                expadedCellCount = societyTerritories
                    .Where(territory => territory.CellCount < initialTerritorySizeLimit)
                    .Sum(territory => ExpandTerritory(territory, unassignedSocietyCells));
            }
            while (expadedCellCount > 0);
            
            nonSocietyTerritories
                .ForEach(nonSocietyTerritory => FixDisconnectedTerritory(nonSocietyTerritory));

            societyTerritories.ForEach(territory => territory.InitializeFrontier(this));

            Redraw();

            cells.ToList().ForEach(cell => cell.CanBeInvaded = CanCellBeInvaded(cell));

            FixNonInvadableTerritories();
        }

        public bool ExpandSingleCell(ITerritory territoryToExpand)
        {
            var fromCellIndexsList = territoryToExpand.FrontierCellIndices
                .OrderBy(cellIndex => Random.Range(0f, 1f))
                .ToList();

            var fromCellIndexListIndex = 0;
            do
            {
                var fromCellIndex = fromCellIndexsList[fromCellIndexListIndex];
                var target = CellGetNeighbours(fromCellIndex)
                    .Where(cell => cell.IsNonSocietyTerritory)
                    .OrderBy(cells => Random.Range(0f, 1f))
                    .FirstOrDefault();

                if (target != null)
                {
                    var from = cells[fromCellIndex];
                    var hasBeenInvaded = InvadeTerritory(from, target);
                    if (hasBeenInvaded)
                    {
                        return true;
                    }
                }

                fromCellIndexListIndex++;
            }
            while (fromCellIndexListIndex < fromCellIndexsList.Count);

            return false;
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
        
        private List<ITerritory> FixDisconnectedTerritory(ITerritory territory)
        {
            var connectedTerritories = new List<ITerritory>() { territory };
            var disconnectedCells = ((Territory)territory).cells;
            while (disconnectedCells.Count > 0)
            {
                var currentTerritoryCells = disconnectedCells.Take(1).ToList();
                disconnectedCells.Remove(currentTerritoryCells.Single());

                var lastAddedCellsCount = 0;
                do
                {
                    var cellsToAdd = currentTerritoryCells.SelectMany(cell => CellGetNeighbours(cell))
                        .Where(neighbour => disconnectedCells.Contains(neighbour))
                        .Distinct()
                        .ToList();

                    cellsToAdd.ForEach(cell => disconnectedCells.Remove(cell));
                    currentTerritoryCells.AddRange(cellsToAdd);

                    lastAddedCellsCount = cellsToAdd.Count;
                }
                while (lastAddedCellsCount > 0);

                if (disconnectedCells.Count > 0)
                {
                    var newTerritory = sohgFactory.CreateTerritory(currentTerritoryCells.ToArray());
                    connectedTerritories.Add(newTerritory);
                }
            }

            return connectedTerritories;
        }
    }
}
