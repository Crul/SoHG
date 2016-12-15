using UnityEngine;

namespace Sohg.Grids2D.Contracts
{
    public interface ICell
    {
        int CellIndex { get; }
        bool IsInvolvedInAttack { get; set; }
        int TerritoryIndex { get; }
        Vector3 WorldPosition { get; }

        bool IsSea { get; }
        bool IsNonSocietyTerritory { get; }
        bool IsSocietyTerritory { get; }

        void Initialize(int cellIndex, Vector3 worldPosition);
        void SetSocietyAssigned();
        void SetSocietyUnassigned();
    }
}
