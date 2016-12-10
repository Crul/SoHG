using Sohg.GameAgg.Contracts;

namespace Sohg.SocietyAgg.Relationships
{
    public partial class Relationship
    {
        public void Evolve(IWarPlayable game)
        {
            if (!AreWeNeighbours())
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
