using UnityEngine;

namespace Sohg.Grids2D.Contracts
{
    public interface ICell
    {
        bool IsInvolvedInAttack { get; set; }
        bool IsSocietyAssigned { get; }
        bool IsSocietyUnassigned { get; }
        int TerritoryIndex { get; }
        Vector3 WorldPosition { get; }

        void Initialize(int cellIndex, Vector3 worldPosition);
        void SetSocietyAssigned();
    }
}
