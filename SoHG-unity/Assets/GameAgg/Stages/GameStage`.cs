using Sohg.GameAgg.Contracts;

namespace Sohg.GameAgg.Stages
{
    public abstract class GameStage<TRunningbleGame> : GameStage
        where TRunningbleGame : IRunningGame
    {
        protected TRunningbleGame game
        {
            get
            {
                return GetGame<TRunningbleGame>();
            }
        }
    }
}
