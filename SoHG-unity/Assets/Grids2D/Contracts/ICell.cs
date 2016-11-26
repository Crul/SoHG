using UnityEngine;

namespace Sohg.Grids2D.Contracts
{
    public interface ICell
    {
        bool IsSocietyAssigned { get; }
        bool IsSocietyUnassigned { get; }
        int TerritoryIndex { get; }

        void Initialize(int cellIndex, Vector3 worldPosition);
        void SetSocietyAssigned();
    }
}
