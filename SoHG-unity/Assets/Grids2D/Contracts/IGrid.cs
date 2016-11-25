using Sohg.CrossCutting.Factories.Contracts;

namespace Sohg.Grids2D.Contracts
{
    public interface IGrid
    {
        int TerritoryCount { get; }

        void AddTerritory(ITerritory territory);
        void InitializeBoard(ISohgFactory sohgFactory);
    }
}
