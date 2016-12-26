using System.Collections.Generic;
using Sohg.CrossCutting.Contracts;
using Sohg.SocietyAgg.Contracts;
using UnityEngine;

namespace Sohg.SocietyAgg
{
    public partial class Relationship : IRelationship
    {
        public ISociety We { get; private set; }
        public ISociety Them { get; private set; }

        public float FriendshipRange { get; private set; }

        public List<int> MyFrontierCellIndices
        {
            get
            {
                return We.Territory
                    .FrontierCellIndicesByTerritoryIndex[Them.Territory.TerritoryIndex];
            }
        }

        public Relationship(ISociety we, ISociety them, IRelationship originRelationship = null)
        {
            We = we;
            Them = them;
            FriendshipRange = (originRelationship == null ? 0.5f : originRelationship.FriendshipRange); // TODO update Relationship.firendshipRange
        }

        public bool AreWeNeighbours()
        {
            var theirTerritoryIndex = Them.Territory.TerritoryIndex;

            return We.Territory.FrontierCellIndicesByTerritoryIndex.ContainsKey(theirTerritoryIndex)
                && We.Territory.FrontierCellIndicesByTerritoryIndex[theirTerritoryIndex].Count > 0;
        }

        public bool WillingToAttack(ISohgConfig config)
        {
            var friendshipThreshold = (Random.Range(0f, 1f) // TODO configure willing to attack
                * config.FriendshipRangeBottomThresholdForAttack
                * (We.Species == Them.Species ? 0.1 : 1));

            return (FriendshipRange < friendshipThreshold) && ShouldWeAttack(config);
        }

        private bool ShouldWeAttack(ISohgConfig config)
        {
            return ((1 + We.State.Power) / 1 + Them.State.Power) > config.PowerBalanceThresholdForAttack;
        }
    }
}
