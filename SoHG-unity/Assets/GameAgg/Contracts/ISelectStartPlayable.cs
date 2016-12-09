using Sohg.Grids2D.Contracts;

namespace Sohg.GameAgg.Contracts
{
    public interface ISelectStartPlayable : IRunningGame
    {
        void ResetGame();
        void CreateSocieties(ICell initialPlayerCell);
        void SetGridSelectionToCell();
        void SetGridSelectionToNone();
    }
}
