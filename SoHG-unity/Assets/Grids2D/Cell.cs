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
        private float latitude;

        public int CellIndex { get; private set; }
        public bool CanBeInvaded { get; set; }
        public int DistanceToCoast { get; private set; }
        public float FertilityRatio { get; private set; }
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
        
        public void Initialize(int cellIndex, Vector3 worldPosition, int worldRowSize)
        {
            CellIndex = cellIndex;
            WorldPosition = worldPosition;
            cellType = (visible ? CellType.NonSocietyTerritory : CellType.Sea);

            var halfWorldRowSize = (worldRowSize / 2);
            latitude = System.Math.Abs((float)row - halfWorldRowSize) / halfWorldRowSize;
        }

        public bool IsSameTerritory(Cell cell)
        {
            return territoryIndex == cell.territoryIndex;
        }

        public void SetDistanceToCoast(int distanceToCoast)
        {
            DistanceToCoast = distanceToCoast;
            SetFertility();
        }

        public void SetSocietyAssigned()
        {
            cellType = CellType.SocietyTerritory;
        }

        public void SetSocietyUnassigned()
        {
            cellType = CellType.NonSocietyTerritory;
        }

        private void SetFertility()
        {
            var fertilityByLatitude = (1f - (2f * latitude));
            var fertilityByDistanceToCoast = (3f / (DistanceToCoast + 3f));

            FertilityRatio = 0.97f + ((fertilityByLatitude + fertilityByDistanceToCoast) / 40f);
        }
    }
}
