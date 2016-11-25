using Sohg.CrossCutting.Factories.Contracts;
using Sohg.Grids2D.Contracts;

namespace Sohg.GameAgg
{
    public partial class GameEngine
    {
        private IGrid grid;
        private ISohgFactory sohgFactory;

        public GameEngine(ISohgFactory sohgFactory, IGrid grid)
        {
            this.sohgFactory = sohgFactory;
            this.grid = grid;
        }
    }
}
