namespace Sohg.GameAgg.Contracts
{
    public interface IGameStatusInfo
    {
        int DisplayingYear { get; }

        void SetGame(IRunningGame game);
    }
}
