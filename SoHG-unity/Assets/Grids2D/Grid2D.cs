using Sohg.CrossCutting.Contracts;
using Sohg.Grids2D.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Grids2D
{
    public partial class Grid2D : IGrid
    {
        private ISohgFactory sohgFactory;
        private bool territoriesHaveChanged = false;
        private List<ITerritory> seaTerritories;
        private List<ITerritory> nonSocietyTerritories;

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

        public ICell GetRandomCell(Func<ICell, bool> cellFilter)
        {
            var filteredCells = cells.Where(cell => cellFilter(cell));
            var randomIndex = UnityEngine.Random.Range(0, filteredCells.Count() - 1);

            return filteredCells.ElementAt(randomIndex);
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

        private void SetCellTerritory(ICell cell, ITerritory territory)
        {
            CellSetTerritory(((Cell)cell).CellIndex, ((Territory)territory).TerritoryIndex);
            ((Territory)territory).cells.Add((Cell)cell);
            if (territory.Society != null)
            {
                cell.SetSocietyAssigned();
            }
        }

        private void SetMask(string maskPath)
        {
            gridMask = Resources.Load<Texture2D>(maskPath);
        }

        private void SetTexture(string texturePath)
        {
            var textureResource = Resources.Load<Texture2D>(texturePath);
            var material = GetComponent<Renderer>().materials[0];
            material.mainTexture = textureResource;
            material.color = new Color(1, 1, 1);
        }
    }
}
