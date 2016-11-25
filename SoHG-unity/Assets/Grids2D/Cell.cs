using Sohg.Grids2D.Contracts;

namespace Grids2D
{
    public enum CellType
    {
        Undefined,
        Sea,
        TerritoryUnassigned,
        SocietyUnassigned,
        SocietyAssigned
    }

    public partial class Cell : ICell
    {
        public int CellIndex { get; private set; }

        public bool IsSocietyAssigned
        {
            get { return cellType == CellType.SocietyAssigned; }
        }

        public bool IsSocietyUnassigned
        {
            get { return cellType == CellType.SocietyUnassigned; }
        }

        public bool IsTerritoryUnassigned
        {
            get { return cellType == CellType.TerritoryUnassigned; }
        }

        private CellType cellType;

        public void Initialize(int cellIndex)
        {
            CellIndex = cellIndex;
            cellType = (visible ? CellType.TerritoryUnassigned : CellType.Sea);
            visible = true;
        }

        public void SetSocietyAssigned()
        {
            cellType = CellType.SocietyAssigned;
        }

        public void SetTerritoryAssigned()
        {
            cellType = CellType.SocietyUnassigned;
        }
    }
}
