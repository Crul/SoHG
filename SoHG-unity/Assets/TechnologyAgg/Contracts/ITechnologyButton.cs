using Sohg.GameAgg.Contracts;

namespace Sohg.TechnologyAgg.Contracts
{
    public interface ITechnologyButton
    {
        void Initialize(IRunningGame game, ITechnology technology);
        void SetState(int faithPower);
    }
}
