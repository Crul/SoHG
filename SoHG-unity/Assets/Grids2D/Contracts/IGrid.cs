﻿using Sohg.CrossCutting.Contracts;
using Sohg.GameAgg.Contracts;
using System;

namespace Sohg.Grids2D.Contracts
{
    public interface IGrid
    {
        int TerritoryCount { get; }

        void AddOnCellClick(Action<ICell> onCellClick);
        void AddTerritory(ITerritory territory, params ICell[] cells);

        ICell GetCell(int cellIndex);
        ICell GetInvadableCell(ICell from, ITerritory territory);
        ICell GetRandomCell(Func<ICell, bool> cellFilter);
        ITerritory GetTerritory(ICell cell);

        void ExpandSocietiesTerritories(int initialTerritorySizeLimit);
        bool ExpandSingleCell(ITerritory territory);
        void InitializeBoard(ISohgFactory sohgFactory);
        bool InvadeTerritory(ICell from, ICell target);
        void SetGridSelectionToNone();
        void SetGridSelectionToCell();
        void RedrawIfChanged();
    }
}
