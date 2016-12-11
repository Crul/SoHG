using Sohg.SocietyAgg.Contracts;
using System.Collections.Generic;
using UnityEngine;

namespace Sohg.Grids2D.Contracts
{
    public interface ITerritory
    {
        int CellCount { get; }
        List<int> FrontierCellIndices { get; }
        IDictionary<int, List<int>> FrontierCellIndicesByTerritoryIndex { get; }
        ISociety Society { get; }
        int TerritoryIndex { get; }

        Vector2 GetCenter();
        void SetSociety(ISociety society);
    }
}
