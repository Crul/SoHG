using Sohg.Grids2D.Contracts;
using Sohg.SocietyAgg.Contracts;
using Sohg.TechnologyAgg.Contracts;
using System.Collections;

namespace Sohg.GameAgg.Contracts
{
    public interface IEvolvableGame : IRunningGame
    {
        bool CheckDependentTechnologies(ITechnologyDependent technologyDependent);
        bool CreateFight(IRelationship relationship, int fromCellIndex);
        void EmitFaith(ISociety society);
        void ExecuteAction(IEnumerator actionExecution);
        void FinishEvolution(bool hasPlayerWon);
        void Invade(ICell from, ICell target);
        void OnTechnologyActivated();
    }
}
