namespace Sohg.GameAgg.Contracts
{
    public interface ITechnologyPanel
    {
        void Initialize(IRunningGame game);
        bool IsVisible();
        void Open();
    }
}
