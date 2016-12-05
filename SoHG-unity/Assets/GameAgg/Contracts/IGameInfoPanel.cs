namespace Sohg.GameAgg.Contracts
{
    public interface IGameInfoPanel
    {
        IPausedPanel PausedPanel { get; }
        IGameStatusInfo GameStatusInfo { get; }
        ITechnologyPanel TechnologyPanel { get; }

        void LogOutput(string log);
    }
}
