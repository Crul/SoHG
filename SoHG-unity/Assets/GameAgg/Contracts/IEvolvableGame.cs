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
        void KillSociety(ISociety society);
        void OnTechnologyActivated();
        void ShrinkSociety(ISociety society);
    }
}
