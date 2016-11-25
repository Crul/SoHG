using Sohg.Grids2D.Contracts;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Grids2D
{
    public partial class Grid2D
    {
        private List<int> GetCellIndexRange()
        {
            return Enumerable.Range(0, cells.Count).ToList();
        }

        private Territory GetTerritory(Cell cell)
        {
            return territories[cell.territoryIndex];
        }

        private List<int> GetTerritoryIndexRange()
        {
            return Enumerable.Range(0, territories.Count).ToList();
        }

        private void SetCellTerritory(ICell cell, ITerritory territory)
        {
            CellSetTerritory(((Cell)cell).CellIndex, ((Territory)territory).TerritoryIndex);
            ((Territory)territory).cells.Add((Cell)cell);
        }

        private void SetMask(string maskPath)
        {
            gridMask = Resources.Load<Texture2D>(maskPath);
        }

        private void SetTexture(string texturePath)
        {
            canvasTexture = Resources.Load<Texture2D>(texturePath);
        }

        private void RedrawTerritories()
        {
            Redraw();
            GetTerritoryIndexRange().ForEach(territoryIndex => TexturizeTerritory(territoryIndex));
        }

        private void TexturizeTerritory(int territoryIndex)
        {
            var color = new Color(1, 1, 1, 0.3f);
            TerritoryToggleRegionSurface(territoryIndex, true, color, false, null);
        }
    }
}
