using Sohg.GameAgg.Contracts;

namespace Sohg.GameAgg
{
    public partial class GameEngine : IEndableGame
    {
        public void OpenGameEnding()
        {
            endGame.Show(hasPlayerWon.Value);
        }
    }
}
