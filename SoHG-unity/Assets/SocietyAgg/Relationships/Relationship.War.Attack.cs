using Sohg.GameAgg.Contracts;
using UnityEngine;

namespace Sohg.SocietyAgg.Relationships
{
    public partial class Relationship
    {
        private enum AttackResult
        {
            Undefined,
            Win,
            Tie,
            Loose
        }

        private void Attack(IWarPlayable game)
        {
            Debug.Log(string.Format("{0} attacking {1}", We.Name, Them.Name));
        }
    }
}
