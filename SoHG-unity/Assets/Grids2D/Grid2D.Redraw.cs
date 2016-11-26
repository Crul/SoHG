using Sohg.Grids2D.Contracts;
using System.Linq;
using UnityEngine;

namespace Grids2D
{
    public partial class Grid2D : IGrid
    {
        private void RedrawTerritories()
        {
            Redraw();
            GetTerritoryIndexRange().ForEach(territoryIndex =>
            {
                TexturizeTerritory(territoryIndex);
                SetSocietyNeighbours(territoryIndex);
            });
        }

        private void SetSocietyNeighbours(int territoryIndex)
        {
            var territory = territories[territoryIndex];
            if (territory.Society == null)
            {
                return;
            }

            // TODO: TerritoryGetNeighbours(territory); not working?
            var neighbourSocieties = territory.cells
                .SelectMany(cell => CellGetNeighbours(cell))
                .Select(cell => cell.territoryIndex)
                .Where(neighbourIndex => neighbourIndex > -1)
                .Distinct()
                .Select(neighbourIndex => territories[neighbourIndex].Society)
                // TODO .Distinct() needed if Society had multiple territories
                .ToList();

            territory.Society.SetNeighbours(neighbourSocieties);
        }

        private void TexturizeTerritory(int territoryIndex)
        {
            var color = new Color(1, 1, 1);
            var society = territories[territoryIndex].Society;
            if (society != null)
            {
                color = society.Color;
            }
            color.a = 0.2f;

            TerritoryToggleRegionSurface(territoryIndex, true, color, false, null);
        }
    }
}
