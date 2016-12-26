using Sohg.CrossCutting.Contracts;
using Sohg.Grids2D.Contracts;
using System.Linq;
using UnityEngine;
using System;
using System.Collections.Generic;
using Sohg.GameAgg.Contracts;

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

            if (territories != null)
            {
                territories.Clear();
            }

            SetGridProperties(gameDefinition);

            Enumerable.Range(0, cells.Count).ToList()
                .ForEach(cellIndex => InitializeCell(cellIndex));

            seaTerritories = CreateTerritoriesByCellType(cell => cell.IsSea);
            nonSocietyTerritories = CreateTerritoriesByCellType(cell => cell.IsNonSocietyTerritory);
            
            Redraw();
        }
        
        private List<ITerritory> CreateTerritoriesByCellType(Func<ICell, bool> cellFilter)
        {
            var filteredCells = cells.Where(cell => cellFilter(cell)).ToList();
            var allCellsTerritory = sohgFactory.CreateTerritory("Land", filteredCells.ToArray());

            return FixDisconnectedTerritory((Territory)allCellsTerritory);
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
        }
    }
}
