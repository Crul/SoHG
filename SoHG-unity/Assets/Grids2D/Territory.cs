using Sohg.Grids2D.Contracts;
using Sohg.SocietyAgg.Contracts;

namespace Grids2D
{
    public partial class Territory: ITerritory
    {
        public int TerritoryIndex { get; private set; }
        public ISociety Society { get; private set; }

        public Territory(int territoryIndex)
            : this(territoryIndex.ToString())
        {
            TerritoryIndex = territoryIndex;
        }

        public void SetSociety(ISociety society)
        {
            Society = society;
        }
    }
}
