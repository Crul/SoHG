using Sohg.Grids2D.Contracts;
using Sohg.SocietyAgg.Contracts;
using Sohg.TechnologyAgg.Contracts;

namespace Sohg.GameAgg.Contracts
{
    public interface IEvolvableGame : IRunningGame
    {
        bool CheckDependentTechnologies(ITechnologyDependent technologyDependent);
        void FinishEvolution(bool hasPlayerWon);
        bool Invade(ICell from, ICell target);
        void Kill(ISociety society);
        void OnTechnologyActivated();
        void Shrink(ISociety society);
        void Split(ISociety society);
    }
}
