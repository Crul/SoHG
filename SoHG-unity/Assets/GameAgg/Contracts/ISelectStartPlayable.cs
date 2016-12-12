using Sohg.Grids2D.Contracts;

namespace Sohg.GameAgg.Contracts
{
    public interface ISelectStartPlayable : IRunningGame
    {
        void CreateSocieties(ICell initialPlayerCell);
    }
}
