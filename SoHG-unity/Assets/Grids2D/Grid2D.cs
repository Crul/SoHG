using Sohg.CrossCutting.Contracts;
using Sohg.Grids2D.Contracts;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Grids2D
{
    public partial class Grid2D : IGrid
    {
        private ISohgFactory sohgFactory;
        private bool territoriesHaveChanged = false;

        public int TerritoryCount { get { return territories.Count; } }

        public void AddTerritory(ITerritory territory, params ICell[] cells)
        {
            territories.Add((Territory)territory);
            _numTerritories++;
            cells.ToList().ForEach(cell => SetCellTerritory(cell, territory));
        }

        public ICell GetCell(int cellIndex)
        {
            return cells[cellIndex];
        }

        public List<ICell> GetCoast(ITerritory territory)
        {
            return ((Territory)territory).cells
                .Where(cell => cell.IsCoast)
                .Select(cell => (ICell)cell)
                .ToList();
        }

        public ICell GetRandomCell(System.Func<ICell, bool> cellFilter)
        {
            var filteredCells = cells.Where(cell => cellFilter(cell));
            var randomIndex = UnityEngine.Random.Range(0, filteredCells.Count() - 1);

            return filteredCells.ElementAt(randomIndex);
        }

        public ICell GetSeaNextTo(ICell cell)
        {
            return CellGetNeighbours((Cell)cell)
                .Where(neighbour => neighbour.IsSea)
                .OrderBy(neighbour => Random.Range(0f, 1f))
                .FirstOrDefault();
        }

        public ITerritory GetTerritory(ICell cell)
        {
            return territories[cell.TerritoryIndex];
        }

        public void RedrawIfChanged()
        {
            if (territoriesHaveChanged)
            {
                territoriesHaveChanged = false;
                Redraw();
            }
        }

        public void SetGridSelectionToNone()
        {
            highlightMode = HIGHLIGHT_MODE.None;
        }

        public void SetGridSelectionToCell()
        {
            highlightMode = HIGHLIGHT_MODE.Cells;
        }

        public void SetGridSelectionToTerritory()
        {
            highlightMode = HIGHLIGHT_MODE.Territories;
        }

        private void SetCellTerritory(ICell cell, ITerritory territory = null)
        {
            if (territory != null)
            {
                CellSetTerritory(cell.CellIndex, territory.TerritoryIndex);
                ((Territory)territory).cells.Add((Cell)cell);
            }
            else
            {
                CellSetTerritory(cell.CellIndex, -1);
            }

            if (!cell.IsSea)
            {
                if (territory != null)
                {
                    cell.SetSocietyAssigned();
                }
                else
                {
                    cell.SetSocietyUnassigned();
                }
            }
        }

        private void SetMask(Texture2D mask)
        {
            gridMask = mask;
        }

        private void SetTexture(Texture2D texture)
        {
            var material = GetComponent<Renderer>().materials[0];
            material.mainTexture = texture;
            material.color = new Color(1, 1, 1);
        }
    }
}
