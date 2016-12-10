using Sohg.GameAgg.Contracts;
using Sohg.Grids2D.Contracts;

namespace Sohg.SocietyAgg.Contracts
{
    public interface IRelationship
    {
        ISociety We { get; }
        ISociety Them { get; }

        bool AreWeNeighbours();
        void Evolve(IWarPlayable game);
        void ResolveAttack(IWarPlayable game, ICell from, ICell target);
    }
}
