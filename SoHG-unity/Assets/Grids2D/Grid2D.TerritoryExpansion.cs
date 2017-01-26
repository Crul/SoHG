using Sohg.Grids2D.Contracts;
using Sohg.SocietyAgg.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Grids2D
{
    public partial class Grid2D
    {
        public void ContractSingleCell(ITerritory territory)
        {
            if (territory.CellCount == 1)
            {
                return;
            }

            var abandonedCell = territory.FrontierCellIndices
                .Select(cellIndex => cells[cellIndex])
                .Where(cell => cell.CanBeInvaded && !cell.IsInvolvedInAttack)
                .OrderBy(cell => cell.FertilityRatio)
                .FirstOrDefault();

            if (abandonedCell == null)
            {
                return;
            }
            
            SetCellTerritory(abandonedCell);
            UpdateFrontiersAfterTerritoryChange(abandonedCell);
            FixNonInvadableTerritories();
            
            territoriesHaveChanged = true;
        }

        public void ExpandSocietiesTerritories(int initialSocietyPopulationLimit)
        {
            var unassignedSocietyCells = cells
                .Where(cell => cell.IsNonSocietyTerritory)
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
            
            Redraw();
            
            territories.ToList().ForEach(territory => territory.InitializeFrontier(this));

            cells.ToList().ForEach(cell => cell.CanBeInvaded = CanCellBeInvaded(cell));

            FixNonInvadableTerritories();
        }

        public bool ExpandSingleCell(ITerritory territory)
        {
            var fromCellIndexsList = territory.FrontierCellIndices
                .OrderByDescending(cellIndex => cells[cellIndex].FertilityRatio)
                .ToList();

            var fromCellIndexListIndex = 0;
            do
            {
                var fromCellIndex = fromCellIndexsList[fromCellIndexListIndex];
                var target = CellGetNeighbours(fromCellIndex)
                    .Where(cell => cell.IsNonSocietyTerritory)
                    .OrderBy(cell => cell.DistanceToCoast)
                    .ThenByDescending(cell => cell.FertilityRatio)
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

        public void RemoveSocietyTerritory(ITerritory territory)
        {
            if (territory.CellCount == 0)
            {
                return;
            }

            var territoryCells = ((Territory)territory).cells;
            territoryCells.ForEach(cell =>
            {
                SetCellTerritory(cell);
                UpdateFrontiersAfterTerritoryChange(cell);
            });
            FixNonInvadableTerritories();

            territoriesHaveChanged = true;
        }

        public bool SettleFromSea(ISociety society, ICell cell)
        {
            var targetLandCell = CellGetNeighbours(cell.CellIndex)
                .Where(neighbour => neighbour.IsNonSocietyTerritory
                    && !CellGetNeighbours(neighbour).Any(neighbourNeighbour => neighbourNeighbour.IsSocietyTerritory))
                .OrderByDescending(neighbour => neighbour.FertilityRatio)
                .FirstOrDefault();

            if (targetLandCell == null)
            {
                return false;
            }

            var newSociety = sohgFactory.CreateSociety(society, targetLandCell);
            newSociety.State.SetInitialPopulation(newSociety.State.PopulationDensity);
            newSociety.Territory.InitializeFrontier(this);

            territoriesHaveChanged = true;

            return true;
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
    }
}
