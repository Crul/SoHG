using Sohg.SpeciesAgg.Contracts;
using UnityEngine;

namespace Sohg.SocietyAgg.Contracts
{
    public interface ISocietyDefinition
    {
        string Name { get; }
        ISpeciesDefinition Species { get; }
        Color Color { get; }

        float InitialAggressivityRate { get; }
        float InitialTechnologyLevelRate { get; }
    }
}
