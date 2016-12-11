using Sohg.CrossCutting.Contracts;
using Sohg.Grids2D.Contracts;
using System.Linq;
using UnityEngine;
using System;
using System.Collections.Generic;

namespace Grids2D
{
    public partial class Grid2D : IGrid
    {
        public void AddOnCellClick(Action<ICell> onCellClick)
        {
            OnCellClick += cellIndex => onCellClick(cells[cellIndex]);
        }

        public void InitializeBoard(ISohgFactory sohgFactory)
        {
            this.sohgFactory = sohgFactory;

            territories.Clear();

            SetGridProperties();
            SetTexture("Textures/worldMap");
            SetMask("Textures/worldMask");

            Enumerable.Range(0, cells.Count).ToList()
                .ForEach(cellIndex => InitializeCell(cellIndex));

            seaTerritories = CreateTerritoriesByCellType(cell => cell.IsSea);
            nonSocietyTerritories = CreateTerritoriesByCellType(cell => cell.IsNonSocietyTerritory);
            
            Redraw();
        }

        private List<ITerritory> CreateTerritoriesByCellType(Func<ICell, bool> cellFilter)
        {
            var filteredCells = cells.Where(cell => cellFilter(cell)).ToList();
            var allCellsTerritory = sohgFactory.CreateTerritory(filteredCells.ToArray());

            return FixDisconnectedTerritory(allCellsTerritory);
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

        private void InitializeCell(int cellIndex)
        {
            var cellWorldPosition = CellGetPosition(cellIndex);
            cells[cellIndex].Initialize(cellIndex, cellWorldPosition);
        }

        private void SetGridProperties()
        {
            gridTopology = GRID_TOPOLOGY.Hexagonal;
            SetGridSelectionToNone();

            // TODO configure in MapSettings:
            numTerritories = 1;
            columnCount = 36;
            rowCount = 24;
            showTerritories = true;
            colorizeTerritories = true;
            allowTerritoriesInsideTerritories = true;
            territoryHighlightColor = new Color(1, 1, 1, 0.3f);
            cellBorderColor = new Color(0, 0, 0, 0.1f);
            territoryFrontiersColor = new Color(0, 0, 0, 0.3f);
        }
    }
}
