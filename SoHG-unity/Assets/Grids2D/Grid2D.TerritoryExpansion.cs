using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Grids2D
{
    public partial class Grid2D
    {
        public void ExpandSocietiesTerritories()
        {
            var unassignedSocietyCells = cells.Where(cell => cell.IsSocietyUnassigned).ToList();

            var societyTerritories = cells.Where(cell => cell.IsSocietyAssigned)
                .Select(cell => GetTerritory(cell)).Distinct().ToList();

            while (unassignedSocietyCells.Count > 0)
            {
                var expadedCellCount = societyTerritories
                    .Sum(territory => ExpandTerritory(territory, unassignedSocietyCells));

                CheckSocietiesTerritoriesExpansion(expadedCellCount, unassignedSocietyCells);
            }

            territories.ForEach(territory => territory.InitializeFrontier(this));

            Redraw();

            territories.ForEach(territory =>
                TerritoryToggleRegionSurface(territory.TerritoryIndex, true, Color.white, false, canvasTexture));

            cells.Where(cell => cell.territoryIndex > -1).ToList()
                .ForEach(cell => cell.CanBeInvaded = CanCellBeInvaded(cell));

            FixNonInvadableTerritories();
            
            societyTerritories.SelectMany(territory => territory.cells)
                .ToList().ForEach(cell => cell.SetSocietyAssigned());
        }

        private void CheckSocietiesTerritoriesExpansion(int expadedCellCount, List<Cell> unassignedSocietyCells)
        {
            var noCellsExpandedAndUnassignedCellsPending = (expadedCellCount == 0 && unassignedSocietyCells.Count > 0);
            if (noCellsExpandedAndUnassignedCellsPending)
            {
                Debug.LogWarning("ExpandSocietyTerritories not ended!"); // TODO ExpandSocietyTerritories not ended!
                unassignedSocietyCells.Clear();
            }
        }

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

        private void FixNonInvadableTerritories()
        {
            // TODO: resolve ring corner cases (still problem with multiple rings)
            territories
                .Where(territory => (territory.cells.Count > 0
                    && territory.cells.Count(cell => cell.CanBeInvaded) == 0))
                .SelectMany(territory => territory.cells)
                .ToList()
                .ForEach(cell => cell.CanBeInvaded = true);
        }
    }
}
