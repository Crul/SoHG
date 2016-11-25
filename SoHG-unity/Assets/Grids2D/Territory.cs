using Sohg.Grids2D.Contracts;

namespace Grids2D
{
    public partial class Territory: ITerritory
    {
        public int TerritoryIndex { get; private set; }

        public Territory(int territoryIndex)
            : this(territoryIndex.ToString())
        {
            TerritoryIndex = territoryIndex;
        }
    }
}
