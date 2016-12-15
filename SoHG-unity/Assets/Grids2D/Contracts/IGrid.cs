using Sohg.CrossCutting.Contracts;
using System;

namespace Sohg.Grids2D.Contracts
{
    public interface IGrid
    {
        int TerritoryCount { get; }

        void AddOnCellClick(Action<ICell> onCellClick);
        void AddOnTerritoryClick(Action<ITerritory> onTerritoryClick);
        void AddTerritory(ITerritory territory, params ICell[] cells);

        ICell GetCell(int cellIndex);
        ICell GetInvadableCell(ICell from, ITerritory territory);
        ICell GetRandomCell(Func<ICell, bool> cellFilter);
        ITerritory GetTerritory(ICell cell);

        void ContractSingleCell(ITerritory territory);
        void ExpandSocietiesTerritories(int initialSocietyPopulationLimit);
        bool ExpandSingleCell(ITerritory territory);
        void InitializeBoard(ISohgFactory sohgFactory);
        bool InvadeTerritory(ICell from, ICell target);
        void SetGridSelectionToNone();
        void SetGridSelectionToCell();
        void SetGridSelectionToTerritory();
        void RedrawIfChanged();
    }
}
