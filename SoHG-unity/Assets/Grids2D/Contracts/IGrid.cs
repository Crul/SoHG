using Sohg.CrossCutting.Factories.Contracts;
using System;

namespace Sohg.Grids2D.Contracts
{
    public interface IGrid
    {
        int TerritoryCount { get; }

        void AddOnCellClick(Action<ICell> onCellClick);
        void AddTerritory(ITerritory territory);
        void InitializeBoard(ISohgFactory sohgFactory);
    }
}
