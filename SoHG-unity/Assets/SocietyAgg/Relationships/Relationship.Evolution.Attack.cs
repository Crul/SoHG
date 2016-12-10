using Sohg.GameAgg.Contracts;
using Sohg.Grids2D.Contracts;
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

        public void ResolveAttack(IEvolvableGame game, ICell from, ICell target)
        {
            var damageRate = (We.State.Power / Them.State.Power); // TODO randomize
            var result = GetResult(damageRate, game.SohgFactory.Config.AttackDamageTieRateThreshold); // TODO randomize

            var ourDeathRate = 0f;
            var theirDeathRate = 0f;
            
            // TODO Kill configuration to SohgConfig
            if (damageRate > 1)
            { // we win
                ourDeathRate = 0;
                theirDeathRate = (damageRate / (damageRate + 100));
            }
            else
            { // we loose
                ourDeathRate = damageRate;
                theirDeathRate = (damageRate / (damageRate + 100));
            }
            
            We.State.Kill(ourDeathRate);
            Them.State.Kill(theirDeathRate);

            switch (result)
            {
                case AttackResult.Win:
                    game.Invade(from, target);
                    game.Log("{0} has invaded {1}", We.Name, Them.Name);
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

        public bool WillingToAttack(IEvolvableGame game)
        {
            var friendShipThreshold = (Random.Range(0f, 1f) 
                * game.SohgFactory.Config.FriendshipRangeBottomThresholdForAttack);

            return friendshipRange < friendShipThreshold;
        }

        private void Attack(IEvolvableGame game)
        {
            var frontierCellIndices = We.Territory
                .FrontierCellsByTerritoryIndex[Them.Territory.TerritoryIndex]
                .OrderBy(cell => Random.Range(0f, 1f))
                .ToList();

            var availableAttacks = (We.State.MaximumAttacks - currentAttacks);
            var currentFrontierCellIndex = 0;

            while(true)
            {
                var frontierCellIndex = frontierCellIndices[currentFrontierCellIndex];
                if (game.CreateFight(this, frontierCellIndex))
                {
                    currentAttacks++;
                    break;
                }

                currentFrontierCellIndex++;
                if (currentFrontierCellIndex >= frontierCellIndices.Count)
                {
                    break;
                }
            }
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
