using Sohg.GameAgg.Contracts;
using Sohg.Grids2D.Contracts;

namespace Sohg.SocietyAgg.Contracts
{
    public interface IFaithRecolectable
    {
        void Initialize(IWarPlayable game, ICell faithCell, int faithAmount);
    }
}
