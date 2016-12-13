using Sohg.CrossCutting.Contracts;
using System.Collections.Generic;

namespace Sohg.SocietyAgg.Contracts
{
    public interface IRelationship
    {
        ISociety We { get; }
        ISociety Them { get; }
        List<int> MyFrontierCellIndices { get; }

        bool AreWeNeighbours();
        bool WillingToAttack(ISohgConfig config);
    }
}
