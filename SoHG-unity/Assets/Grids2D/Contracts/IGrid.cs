using Sohg.CrossCutting.Contracts;
using System;

namespace Sohg.Grids2D.Contracts
{
    public interface IGrid
    {
        int TerritoryCount { get; }

        void AddTerritory(ITerritory territory, params ICell[] cells);
        ICell GetRandomCell(Func<ICell, bool> cellFilter);

        void AddOnCellClick(Action<ICell> onCellClick);
        void InitializeBoard(ISohgFactory sohgFactory);
        void ExpandSocietiesTerritories();
    }
}
