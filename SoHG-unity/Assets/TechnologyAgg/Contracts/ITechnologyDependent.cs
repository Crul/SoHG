using Sohg.GameAgg.Contracts;

namespace Sohg.TechnologyAgg.Contracts
{
    public interface ITechnologyDependent
    {
        ITechnology[] RequiredTechnologies { get; }

        void CheckActivation();
        void Initialize(IEvolvableGame game);
    }
}
