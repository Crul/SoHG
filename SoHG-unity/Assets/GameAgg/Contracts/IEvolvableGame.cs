using Sohg.Grids2D.Contracts;
using Sohg.TechnologyAgg.Contracts;

namespace Sohg.GameAgg.Contracts
{
    public interface IEvolvableGame : IRunningGame
    {
        bool CheckDependentTechnologies(ITechnologyDependent technologyDependent);
        void FinishEvolution(bool hasPlayerWon);
        void Invade(ICell from, ICell target);
        void OnTechnologyActivated();
    }
}
