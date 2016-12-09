using System.Collections.Generic;
using Sohg.SocietyAgg.Contracts;
using UnityEngine;

namespace Sohg.Grids2D.Contracts
{
    public interface ITerritory
    {
        int TerritoryIndex { get; }
        ISociety Society { get; }
        int CellCount { get; }

        Vector2 GetCenter();
        void SetSociety(ISociety society);
    }
}
