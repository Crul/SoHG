using Sohg.GameAgg.Contracts;

namespace Sohg.GameAgg.Stages
{
    public abstract class GameStage<TRunningbleGame> : GameStageScript
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
