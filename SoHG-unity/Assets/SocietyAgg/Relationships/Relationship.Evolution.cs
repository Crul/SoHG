using Sohg.GameAgg.Contracts;

namespace Sohg.SocietyAgg.Relationships
{
    public partial class Relationship
    {
        public void Evolve(IEvolvableGame game)
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
