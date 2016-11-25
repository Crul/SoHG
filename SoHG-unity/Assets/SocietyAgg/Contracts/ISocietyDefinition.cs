using Sohg.SpeciesAgg.Contracts;

namespace Sohg.SocietyAgg.Contracts
{
    public interface ISocietyDefinition
    {
        string Name { get; }
        ISpeciesDefinition Species { get; }
    }
}
