using Sohg.SocietyAgg.Contracts;
using UnityEngine;
using Sohg.GameAgg.Contracts;
using Sohg.GameAgg.Features;
using System.Linq;
using Sohg.Grids2D.Contracts;

namespace Sohg.SocietyAgg
{
    public partial class Relationship : IRelationship
    {
        private bool isAttackInProgress = false;
        private int cyclesWithoutAttack = -1;

        public ISociety We { get; private set; }
        public ISociety Them { get; private set; }

        public float FriendshipRange { get; private set; }

        public Relationship(IGameDefinition gameDefinition, ISociety we, ISociety them, IRelationship originRelationship = null)
        {
            We = we;
            Them = them;
            FriendshipRange = (originRelationship == null
                ? gameDefinition.InitialFriendshipRange
                : originRelationship.FriendshipRange);
        }

        public bool AreWeNeighbours()
        {
            return We.Territories
                .Any(ourTerritory => Them.Territories
                    .Any(theirTerritory => ourTerritory.FrontierCellIndicesByTerritoryIndex.ContainsKey(theirTerritory.TerritoryIndex)
                        && ourTerritory.FrontierCellIndicesByTerritoryIndex[theirTerritory.TerritoryIndex].Count > 0));
        }

        public void Evolve(IGameDefinition gameDefinition)
        {
            if (AreWeNeighbours() && !isAttackInProgress)
            {
                cyclesWithoutAttack++;

                var friendshipChangeRate = Random.Range(-gameDefinition.FriendshipChangeRate, gameDefinition.FriendshipChangeRate);
                if (friendshipChangeRate > 0)
                {
                    FriendshipRange += ((1 - FriendshipRange) * friendshipChangeRate);
                }
                else
                {
                    FriendshipRange += (FriendshipRange * friendshipChangeRate);
                }
            }
        }

        public void OnAttackEnded(IGameDefinition gameDefinition, ISociety aggressor, AttackResult result)
        {
            cyclesWithoutAttack = 0;
            isAttackInProgress = true;
            switch (result)
            {
                case AttackResult.Win:
                    FriendshipRange *= gameDefinition.FriendshipDecreaseOnWinAttack;
                    break;
                case AttackResult.Tie:
                    FriendshipRange *= gameDefinition.FriendshipDecreaseOnTieAttack;
                    break;
                case AttackResult.Loose:
                    FriendshipRange *= gameDefinition.FriendshipDecreaseOnLooseAttack;
                    break;
            }
        }

        public void OnAttackStarted(IGameDefinition gameDefinition, ISociety aggressor)
        {
            FriendshipRange *= gameDefinition.FriendshipDecreaseOnStartedAttack;

            cyclesWithoutAttack = 0;
            isAttackInProgress = false;
        }

        public bool WillingToAttack(IGameDefinition gameDefinition)
        {
            if (!AreWeNeighbours())
            {
                return false;
            }

            // TODO Add PopulationDensity?, Aggresivity?, Stability?, ...
            var friendshipThreshold = (Random.Range(0f, 1f)
                * gameDefinition.FriendshipRangeBottomThresholdForAttack
                * (We.Species == Them.Species ? gameDefinition.SameSpeciesWillingToAttackFactor : 1));

            if (FriendshipRange < 0)
                Debug.Log(string.Format("{0}  <  {1}", FriendshipRange, friendshipThreshold));

            return (FriendshipRange < friendshipThreshold) && ShouldWeAttack(gameDefinition);
        }

        private bool ShouldWeAttack(IGameDefinition gameDefinition)
        {
            return ((1 + We.State.Power) / 1 + Them.State.Power) > gameDefinition.PowerBalanceThresholdForAttack;
        }
    }
}
