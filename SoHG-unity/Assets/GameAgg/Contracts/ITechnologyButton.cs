namespace Sohg.GameAgg.Contracts
{
    public interface ITechnologyButton
    {
        void Initialize(IRunningGame game, ITechnology technology);
        void SetState(int faithPower);
    }
}
