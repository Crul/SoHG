using Sohg.GameAgg.Contracts;

namespace Sohg.GameAgg
{
    public partial class GameEngine : IRunningGame
    {
        public bool IsPaused()
        {
            return false;
        }
    }
}
