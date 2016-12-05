using Sohg.GameAgg.Contracts;

namespace Sohg.TechnologyAgg.Contracts
{
    public interface ITechnologyPanel
    {
        void Initialize(IRunningGame game);
        bool IsVisible();
        void Open();
    }
}
