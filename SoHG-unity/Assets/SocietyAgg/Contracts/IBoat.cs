using Sohg.GameAgg.Contracts;
using Sohg.Grids2D.Contracts;

namespace Sohg.SocietyAgg.Contracts
{
    public interface IBoat
    {
        void Initialize(IRunningGame game, ISociety society, ICell boatCreationCell);
    }
}
