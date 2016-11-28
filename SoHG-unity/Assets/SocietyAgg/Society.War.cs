using Sohg.GameAgg.Contracts;
using Sohg.SocietyAgg.Contracts;
using UnityEngine;

namespace Sohg.SocietyAgg
{
    public partial class Society : ISociety
    {
        public void Evolve(IWarPlayable game)
        {
            relationships.ForEach(relationship => relationship.Evolve(game));
        }

        public bool ShouldWeAttack(ISociety them)
        {
            return (State.Power / them.State.Power) > config.PowerBalanceThresholdForAttack;
        }
    }
}
