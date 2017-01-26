using Sohg.TechnologyAgg.Contracts;

namespace Sohg.GameAgg.Contracts
{
    public interface IGameInfoPanel
    {
        IPausedPanel PausedPanel { get; }
        IGameStatusInfo GameStatusInfo { get; }
        ITechnologyPanel TechnologyPanel { get; }

        void EnableTechnologyTree();
        void LogOutput(string log);
    }
}
