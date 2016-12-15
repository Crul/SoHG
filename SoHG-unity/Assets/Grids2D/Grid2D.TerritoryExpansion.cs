using Sohg.Grids2D.Contracts;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Grids2D
{
    public partial class Grid2D
    {
        public void ContractSingleCell(ITerritory territory)
        {
            var abandonedCell = territory.FrontierCellIndices
                .Select(cellIndex => cells[cellIndex])
                .Where(cell => cell.CanBeInvaded && !cell.IsInvolvedInAttack)
                .OrderBy(cellIndex => Random.Range(0f, 1f))
                .FirstOrDefault();

            if (abandonedCell == null)
            {
                return;
            }

            var nonSocietyNeighbour = CellGetNeighbours(abandonedCell)
                .FirstOrDefault(neighbour => neighbour.IsNonSocietyTerritory);

            var nonSocietyTerritory = (nonSocietyNeighbour == null
                ? nonSocietyTerritories.First()
                : territories[nonSocietyNeighbour.TerritoryIndex]);
            
            SetCellTerritory(abandonedCell, nonSocietyTerritory);

            UpdateFrontiersAfterTerritoryChange(abandonedCell);

            FixNonInvadableTerritories();

            if (nonSocietyNeighbour == null)
            {
                FixDisconnectedTerritory(nonSocietyTerritory);
            }
            else
            {
                var nonSocietyNeighbourTerritories = CellGetNeighbours(abandonedCell)
                    .Where(neighbourCell => neighbourCell.IsNonSocietyTerritory
                        && neighbourCell.TerritoryIndex != nonSocietyNeighbour.TerritoryIndex)
                    .Select(neighbourCell => territories[neighbourCell.TerritoryIndex])
                    .Distinct();

                nonSocietyNeighbourTerritories
                    .SelectMany(neighbourTerritory => neighbourTerritory.cells)
                    .ToList()
                    .ForEach(cell => SetCellTerritory(cell, nonSocietyTerritory));
            }

            territoriesHaveChanged = true;
        }

        public void ExpandSocietiesTerritories(int initialSocietyPopulationLimit)
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
                    .Where(territory => initialSocietyPopulationLimit > territory.CellCount * territory.Society.Species.InitialPopulationDensity)
                    .Sum(territory => ExpandTerritory(territory, unassignedSocietyCells, (initialSocietyPopulationLimit / territory.Society.Species.InitialPopulationDensity)));
            }
            while (expadedCellCount > 0);
            
            nonSocietyTerritories
                .ForEach(nonSocietyTerritory => FixDisconnectedTerritory(nonSocietyTerritory));

            societyTerritories.ForEach(territory => territory.InitializeFrontier(this));

            Redraw();

            cells.ToList().ForEach(cell => cell.CanBeInvaded = CanCellBeInvaded(cell));
            
            FixNonInvadableTerritories();
        }

        public bool ExpandSingleCell(ITerritory territory)
        {
            var fromCellIndexsList = territory.FrontierCellIndices
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
                        nonSocietyTerritories
                            .ForEach(nonSocietyTerritory => FixDisconnectedTerritory(nonSocietyTerritory));

                        return true;
                    }
                }

                fromCellIndexListIndex++;
            }
            while (fromCellIndexListIndex < fromCellIndexsList.Count);

            return false;
        }
        
        private int ExpandTerritory(Territory territory, List<Cell> unassignedCells, int territorySizeLimit)
        {
            var cellsToBeExpanded = territory.cells
                .SelectMany(cell => CellGetNeighbours(cell.CellIndex))
                .Distinct()
                .Where(cell => unassignedCells.Contains(cell))
                .ToList();

            var availableCellsToExpand = (territorySizeLimit - territory.cells.Count);
            if (cellsToBeExpanded.Count > availableCellsToExpand)
            {
                cellsToBeExpanded = cellsToBeExpanded.Take(availableCellsToExpand).ToList();
            }

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
