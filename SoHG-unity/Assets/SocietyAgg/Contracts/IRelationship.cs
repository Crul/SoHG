using System.Collections.Generic;
using Sohg.SocietyAgg.Contracts;
using Sohg.GameAgg.Contracts;

namespace Sohg.SocietyAgg.Contracts
{
    public interface IRelationship
    {
        bool IsNeighbour { get; }
        ISociety We { get; }
        ISociety Them { get; }

        void Evolve(IWarPlayable game);
        void SetNeighbour(List<ISociety> neighbourSocieties);
    }
}
