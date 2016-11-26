using Sohg.CrossCutting.Contracts;
using Sohg.GameAgg.Contracts;
using Sohg.Grids2D.Contracts;
using Sohg.SocietyAgg.Contracts;
using System.Collections.Generic;

namespace Sohg.GameAgg
{
    public partial class GameEngine
    {
        private IGameDefinition gameDefinition;
        private IGrid grid;
        private ISohgFactory sohgFactory;

        private List<ISociety> societies;

        public GameEngine(ISohgFactory sohgFactory,
            IGrid grid, IGameDefinition gameDefinition)
        {
            this.gameDefinition = gameDefinition;
            this.grid = grid;
            this.sohgFactory = sohgFactory;

            grid.AddOnCellClick(cell => OnGridCellClick(cell));
        }
    }
}
