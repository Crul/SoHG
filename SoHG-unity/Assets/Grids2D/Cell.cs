using Sohg.Grids2D.Contracts;
using UnityEngine;

namespace Grids2D
{
    public enum CellType
    {
        Undefined,
        Sea,
        NonSocietyTerritory,
        SocietyTerritory
    }

    public partial class Cell : ICell
    {
        private CellType cellType;

        public int CellIndex { get; private set; }
        public bool CanBeInvaded { get; set; }
        public int DistanceToCoast { get; set; }
        public bool IsInvolvedInAttack { get; set; }
        public Vector3 WorldPosition { get; private set; }

        public bool IsNonSocietyTerritory
        {
            get { return cellType == CellType.NonSocietyTerritory; }
        }

        public bool IsCoast
        {
            get { return IsSocietyTerritory && DistanceToCoast == 0; }
        }

        public bool IsSea
        {
            get { return cellType == CellType.Sea; }
        }

        public bool IsSocietyTerritory
        {
            get { return cellType == CellType.SocietyTerritory; }
        }

        public int TerritoryIndex { get { return territoryIndex; } }

        public void Initialize(int cellIndex, Vector3 worldPosition)
        {
            CellIndex = cellIndex;
            WorldPosition = worldPosition;
            cellType = (visible ? CellType.NonSocietyTerritory : CellType.Sea);
        }

        public bool IsSameTerritory(Cell cell)
        {
            return territoryIndex == cell.territoryIndex;
        }

        public void SetSocietyAssigned()
        {
            cellType = CellType.SocietyTerritory;
        }

        public void SetSocietyUnassigned()
        {
            cellType = CellType.NonSocietyTerritory;
        }
    }
}
