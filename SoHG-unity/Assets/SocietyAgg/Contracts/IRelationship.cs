using Sohg.GameAgg.Contracts;
using System.Collections.Generic;

namespace Sohg.SocietyAgg.Contracts
{
    public interface IRelationship
    {
        ISociety We { get; }
        ISociety Them { get; }
        List<int> MyFrontierCellIndices { get; }
        float FriendshipRange { get; }

        bool AreWeNeighbours();
        bool WillingToAttack(IGameDefinition gameDefinition);
    }
}
