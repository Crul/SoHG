using Sohg.CrossCutting.Contracts;
using System;
using System.Collections.Generic;

namespace Sohg.Grids2D.Contracts
{
    public interface IGrid
    {
        int TerritoryCount { get; }

        void AddOnCellClick(Action<ICell> onCellClick);
        void AddTerritory(ITerritory territory, params ICell[] cells);

        Dictionary<ICell, ICell> GetInvadableCells(ITerritory territory1, ITerritory territory2);
        ICell GetRandomCell(Func<ICell, bool> cellFilter);
        ITerritory GetTerritory(ICell cell);

        void ExpandSocietiesTerritories();
        void InitializeBoard(ISohgFactory sohgFactory);
        void InvadeTerritory(ICell from, ICell target);
        void SetGridSelectionToNone();
        void SetGridSelectionToCell();
        void RedrawIfChanged();
    }
}
