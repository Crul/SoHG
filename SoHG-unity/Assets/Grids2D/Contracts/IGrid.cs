using Sohg.CrossCutting.Contracts;
using Sohg.GameAgg.Contracts;
using System;
using System.Collections.Generic;
using Sohg.SocietyAgg.Contracts;

namespace Sohg.Grids2D.Contracts
{
    public interface IGrid
    {
        int TerritoryCount { get; }

        void AddOnCellClick(Action<ICell> onCellClick);
        void AddOnTerritoryClick(Action<ITerritory> onTerritoryClick);
        void AddTerritory(ITerritory territory, params ICell[] cells);

        ICell GetCell(int cellIndex);
        List<ICell> GetCoast(ITerritory territory);
        ICell GetInvadableCell(ICell from, ITerritory territory);
        ICell GetRandomCell(Func<ICell, bool> cellFilter);
        ICell GetSeaNextTo(ICell cell);
        ITerritory GetTerritory(ICell cell);

        void ContractSingleCell(ITerritory territory);
        void ExpandSocietiesTerritories(int initialSocietyPopulationLimit);
        bool ExpandSingleCell(ITerritory territory);
        void InitializeBoard(ISohgFactory sohgFactory, IGameDefinition gameDefinition);
        bool InvadeTerritory(ICell from, ICell target);
        void OnTerritorySplit(ITerritory territory1, ITerritory territory2 = null);
        void RedrawIfChanged();
        void RemoveSocietyTerritory(ITerritory territory);
        void SetGridSelectionToNone();
        void SetGridSelectionToCell();
        void SetGridSelectionToTerritory();
        bool SettleFromSea(ISociety society, ICell cell);
        List<ICell> SplitTerritory(ITerritory territory);
    }
}
