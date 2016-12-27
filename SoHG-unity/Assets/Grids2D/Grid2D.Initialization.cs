using Sohg.CrossCutting.Contracts;
using Sohg.Grids2D.Contracts;
using Sohg.GameAgg.Contracts;
using System;
using System.Linq;
using UnityEngine;

namespace Grids2D
{
    public partial class Grid2D : IGrid
    {
        public void AddOnCellClick(Action<ICell> onCellClick)
        {
            OnCellClick += cellIndex => onCellClick(cells[cellIndex]);
        }

        public void AddOnTerritoryClick(Action<ITerritory> onTerritoryClick)
        {
            OnTerritoryClick +=
                territoryIndex => onTerritoryClick(territories[territoryIndex]);
        }

        public void InitializeBoard(ISohgFactory sohgFactory, IGameDefinition gameDefinition)
        {
            this.sohgFactory = sohgFactory;

            SetGridProperties(gameDefinition);

            Enumerable.Range(0, cells.Count).ToList()
                .ForEach(cellIndex => InitializeCell(cellIndex));
            
            Redraw();
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
            var cell = cells[cellIndex];
            var cellWorldPosition = CellGetPosition(cellIndex);
            cell.Initialize(cellIndex, cellWorldPosition);
            SetCellTerritory(cell);
        }

        private void SetGridProperties(IGameDefinition gameDefinition)
        {
            gridTopology = GRID_TOPOLOGY.Hexagonal;
            SetGridSelectionToNone();
            
            numTerritories = 1;
            columnCount = gameDefinition.BoardColumns;
            rowCount = gameDefinition.BoardRows;
            showTerritories = true;
            colorizeTerritories = true;
            allowTerritoriesInsideTerritories = true;
            territoryHighlightColor = new Color(1, 1, 1, 0.3f);
            cellBorderColor = new Color(0, 0, 0, 0.1f);
            territoryFrontiersColor = new Color(0, 0, 0, 0.3f);

            SetTexture(gameDefinition.BoardBackground);
            SetMask(gameDefinition.BoardMask);

            territories[0].fillColor = new Color(1, 1, 1, 0);
        }
    }
}
