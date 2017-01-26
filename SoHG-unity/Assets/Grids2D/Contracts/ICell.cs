using UnityEngine;

namespace Sohg.Grids2D.Contracts
{
    public interface ICell
    {
        int CellIndex { get; }
        float FertilityRatio { get; }
        bool IsInvolvedInAttack { get; set; }
        int TerritoryIndex { get; }
        Vector3 WorldPosition { get; }

        bool IsCoast { get; }
        bool IsSea { get; }
        bool IsNonSocietyTerritory { get; }
        bool IsSocietyTerritory { get; }

        void Initialize(int cellIndex, Vector3 worldPosition, int worldRowSize);
        void SetDistanceToCoast(int distanceToCoast);
        void SetSocietyAssigned();
        void SetSocietyUnassigned();
    }
}
