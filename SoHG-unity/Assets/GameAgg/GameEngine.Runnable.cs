using Sohg.GameAgg.Contracts;

namespace Sohg.GameAgg
{
    public partial class GameEngine : IRunnableGame
    {
        public void Start()
        {
            grid.InitializeBoard(sohgFactory);
        }
    }
}
