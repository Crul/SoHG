using Sohg.GameAgg.Contracts;

namespace Sohg.TechnologyAgg.Contracts
{
    public interface ITechnologyBox
    {
        void Initialize(IRunningGame game, ITechnology technology);
        void SetState(int faithPower);
    }
}
