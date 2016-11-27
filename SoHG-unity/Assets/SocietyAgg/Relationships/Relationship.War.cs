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
            
            if (We.WillingToAttack())
            {
                if (We.ShouldWeAttack(Them))
                {
                    Attack(game);
                }
            }
        }
    }
}
