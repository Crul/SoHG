namespace Sohg.GameAgg.Contracts
{
    public interface IGameInfoPanel
    {
        IPausedPanel PausedPanel { get; }
        IGameStatusInfo GameStatusInfo { get; }

        void LogOutput(string log);
    }
}
