using Sohg.GameAgg.Contracts;

namespace Sohg.SocietyAgg.Relationships
{
    public partial class Relationship
    {
        public void Evolve(IWarPlayable game)
        {
            if (!IsNeighbour)
            {
                return;
            }
            
            if (WillingToAttack(game))
            {
                if (We.ShouldWeAttack(Them))
                {
                    Attack(game);
                }
            }
        }
    }
}
