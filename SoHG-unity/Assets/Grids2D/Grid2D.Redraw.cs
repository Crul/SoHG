using Sohg.Grids2D.Contracts;
using UnityEngine;

namespace Grids2D
{
    public partial class Grid2D : IGrid
    {
        public void RedrawIfChanged()
        {
            if (territoriesHaveChanged)
            {
                territoriesHaveChanged = false;
                Redraw();
                GetTerritoryIndexRange().ForEach(territoryIndex =>
                {
                    TerritoryToggleRegionSurface(territoryIndex, true, Color.white, false, canvasTexture);
                    TexturizeTerritory(territoryIndex);
                });
            }
        }

        private void TexturizeTerritory(int territoryIndex)
        {
            var color = new Color(1, 1, 1);
            var society = territories[territoryIndex].Society;
            if (society != null)
            {
                color = society.Color;
            }
            color.a = 0.1f;

            territories[territoryIndex].fillColor = color;
        }
    }
}
