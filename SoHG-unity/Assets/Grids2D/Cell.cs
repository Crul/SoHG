using Sohg.Grids2D.Contracts;
using UnityEngine;

namespace Grids2D
{
    public enum CellType
    {
        Undefined,
        Sea,
        TerritoryUnassigned,
        TerritoryAssigned
    }

    public partial class Cell : ICell
    {
        public int CellIndex { get; private set; }
        
        public bool IsTerritoryUnassigned
        {
            get { return cellType == CellType.TerritoryUnassigned; }
        }

        private CellType cellType;

        public void Initialize(int cellIndex)
        {
            CellIndex = cellIndex;
            cellType = (visible ? CellType.Sea : CellType.TerritoryUnassigned);
            visible = true;
        }

        public void SetTerritoryAssigned()
        {
            cellType = CellType.TerritoryAssigned;
        }
        
    }
}
