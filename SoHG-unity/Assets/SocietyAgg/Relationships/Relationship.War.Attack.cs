using Sohg.GameAgg.Contracts;
using Sohg.Grids2D.Contracts;
using System.Collections.Generic;
using System.Linq;
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

        private int currentAttacks = 0;

        private void Attack(IWarPlayable game)
        {
            var attackableCells = game.GetAttackableCells(this);
            var attacksAvailables = (We.State.MaximumAttacks - currentAttacks);
            if (attackableCells.Count > attacksAvailables)
            {
                attackableCells = attackableCells
                    .OrderBy(pair => Random.Range(0f, 1f))
                    .Take(attacksAvailables)
                    .ToDictionary(x => x.Key, x => x.Value);
            }
            
            attackableCells.ToList().ForEach(cells =>
                {
                    cells.Key.IsInvolvedInAttack = true;
                    cells.Value.IsInvolvedInAttack = true;
                    game.CreateFight(cells.Key, cells.Value, () => ResolveAttack(game, cells.Key, cells.Value));
                });

            currentAttacks += attackableCells.Count;
        }

        
        private void ResolveAttack(IWarPlayable game, ICell from, ICell target)
        {
            var damageRate = (We.State.Power / Them.State.Power); // TODO randomize
            var result = GetResult(damageRate, game.SohgFactory.Config.AttackDamageTieRateThreshold); // TODO randomize

            Them.State.Kill(damageRate);
            We.State.Kill(1 / damageRate);

            switch (result)
            {
                case AttackResult.Win:
                    game.Invade(from, target);
                    // TODO update stats on win
                    break;
                case AttackResult.Loose:
                    // TODO update stats on loose
                    break;
                case AttackResult.Tie:
                    break;
            }
            // TODO update stats on fight

            currentAttacks--;
            from.IsInvolvedInAttack = false;
            target.IsInvolvedInAttack = false;
        }

        private AttackResult GetResult(float damageRate, float attackDamageTieRateTheshold)
        {
            if (System.Math.Abs(1 - damageRate) < attackDamageTieRateTheshold)
                return AttackResult.Tie;

            if (damageRate > 1)
                return AttackResult.Win;

            return AttackResult.Loose;
        }
    }
}
