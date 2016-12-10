using Sohg.GameAgg.Contracts;
using Sohg.Grids2D.Contracts;
using System.Collections.Generic;

namespace Sohg.SocietyAgg.Contracts
{
    public interface IRelationship
    {
        bool IsNeighbour { get; }
        ISociety We { get; }
        ISociety Them { get; }

        void Evolve(IWarPlayable game);
        void ResolveAttack(IWarPlayable game, ICell from, ICell target);
        void SetNeighbour(List<ISociety> neighbourSocieties);
    }
}
