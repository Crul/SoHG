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
                GetTerritoryIndexRange().ForEach(territoryIndex => TexturizeTerritory(territoryIndex));
            }
        }

        private void TexturizeTerritory(int territoryIndex)
        {
            var society = territories[territoryIndex].Society;
            Color color;
            if (society == null)
            {
                color = new Color(1, 1, 1, 0f);
            }else
            {
                color = society.Color;
                color.a = 0.2f;
            }

            territories[territoryIndex].fillColor = color;
        }
    }
}
