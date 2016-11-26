namespace Sohg.GameAgg.Contracts
{
    public interface IWarPlayable : IRunningGame
    {
        void EvolveWar(int time);
    }
}
