using Sohg.GameAgg.Contracts;
using Sohg.GameAgg.Features;
using System.Collections.Generic;

namespace Sohg.SocietyAgg.Contracts
{
    public interface IRelationship
    {
        ISociety We { get; }
        ISociety Them { get; }
        float FriendshipRange { get; }

        bool AreWeNeighbours();
        void Evolve(IGameDefinition gameDefinition);
        void OnAttackEnded(IGameDefinition gameDefinition, ISociety aggressor, AttackResult result);
        void OnAttackStarted(IGameDefinition gameDefinition, ISociety aggressor);
        bool WillingToAttack(IGameDefinition gameDefinition);
    }
}
