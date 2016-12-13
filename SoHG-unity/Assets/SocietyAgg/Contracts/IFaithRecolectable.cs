using Sohg.Grids2D.Contracts;

namespace Sohg.SocietyAgg.Contracts
{
    public interface IFaithRecolectable
    {
        void Initialize(ISociety society, ICell faithCell, int faithAmount);
    }
}
