using Sohg.Grids2D.Contracts;

namespace Grids2D
{
    public partial class Grid2D : IGrid
    {
        public void AddTerritory(ITerritory territory)
        {
            territories.Add((Territory)territory);
            _numTerritories++;
        }
    }
}
