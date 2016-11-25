using Sohg.SocietyAgg.Contracts;

namespace Sohg.Grids2D.Contracts
{
    public interface ITerritory
    {
        int TerritoryIndex { get; }
        ISociety Society { get; }

        void SetSociety(ISociety society);
    }
}
