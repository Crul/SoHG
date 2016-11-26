using Sohg.GameAgg.Contracts;

namespace Sohg.GameAgg
{
    public partial class GameEngine : IWarPlayable
    {
        public void EvolveWar(int time)
        {
            Societies.ForEach(society => society.Evolve(this));
        }
    }
}
