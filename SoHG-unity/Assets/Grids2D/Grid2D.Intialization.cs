using Sohg.CrossCutting.Factories.Contracts;
using Sohg.Grids2D.Contracts;
using System.Linq;
using UnityEngine;

namespace Grids2D
{
    public partial class Grid2D : IGrid
    {
        public int TerritoryCount
        {
            get { return territories.Count; }
        }

        public void InitializeBoard(ISohgFactory sohgFactory)
        {
            gridTopology = GRID_TOPOLOGY.Hexagonal;

            // TODO configure in MapSettings:
            numTerritories = 1;
            columnCount = 36;
            rowCount = 24;
            showTerritories = true;
            colorizeTerritories = true;
            allowTerritoriesInsideTerritories = true;
            territoryHighlightColor = new Color(1, 1, 1, 0.3f);

            territories.Clear();
            SetMask("Textures/worldMask");
            SetTexture("Textures/worldMap");

            GetCellIndexRange().ForEach(cellIndex => InitializeCell(cellIndex));

            CreateConnectedTerritories(sohgFactory);
            RedrawTerritories();
        }

        private void CreateConnectedTerritories(ISohgFactory sohgFactory)
        {
            var unassignedTerritoryCells = cells.Where(c => c.IsTerritoryUnassigned).ToList();
            while (unassignedTerritoryCells.Count > 0)
            {
                var territory = (Territory)sohgFactory.CreateTerritory();
                ExpandFullTerritory(territory, unassignedTerritoryCells);
                territory.cells.ToList().ForEach(cell => cell.SetTerritoryAssigned());
            }
        }

        private void InitializeCell(int cellIndex)
        {
            cells[cellIndex].Initialize(cellIndex);
            CellToggleRegionSurface(cellIndex, true, canvasTexture);
        }
    }
}
