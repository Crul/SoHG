﻿using Sohg.Grids2D.Contracts;
using Sohg.SocietyAgg.Contracts;
using UnityEngine;
using System.Linq;

namespace Grids2D
{
    public partial class Territory : ITerritory
    {
        public int TerritoryIndex { get; private set; }
        public ISociety Society { get; private set; }

        public int CellCount { get { return cells.Count; } }

        public Territory(int territoryIndex)
            : this(territoryIndex.ToString())
        {
            TerritoryIndex = territoryIndex;
            fillColor = new Color(1, 1, 1, 0f);
        }

        public Vector2 GetCenter()
        {
            return new Vector2
            (
                cells.Sum(cell => cell.WorldPosition.x) / cells.Count,
                cells.Sum(cell => cell.WorldPosition.y) / cells.Count
            );
        }

        public float GetFertility()
        {
            return cells.Sum(cell => cell.FertilityRatio);
        }

        public void SetSociety(ISociety society)
        {
            Society = society;
            name = society.Name;

            var societyColor = society.Color;
            societyColor.a = 0.3f; // TODO configure territory.fillColor alpha
            fillColor = societyColor;

            cells.ForEach(cell => cell.SetSocietyAssigned());
        }
    }
}
