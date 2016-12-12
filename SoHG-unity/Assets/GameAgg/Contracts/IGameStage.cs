using Sohg.Grids2D.Contracts;

namespace Sohg.GameAgg.Contracts
{
    public interface IGameStage
    {
        void SetGame(IRunningGame game);

        void Start();
        void FixedUpdate();
        void OnCellClick(ICell cell);
        void OnTerritoryClick(ITerritory territory);
    }
}
