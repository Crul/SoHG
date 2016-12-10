using Sohg.Grids2D.Contracts;
using Sohg.SocietyAgg.Contracts;
using Sohg.TechnologyAgg.Contracts;
using System.Collections;

namespace Sohg.GameAgg.Contracts
{
    public interface IWarPlayable : IRunningGame
    {
        bool CheckDependentTechnologies(ITechnologyDependent technologyDependent);
        bool CreateFight(IRelationship relationship, int fromCellIndex);
        void EmitFaith(ISociety society);
        void EndWar(bool hasPlayerWon);
        void ExecuteAction(IEnumerator actionExecution);
        void Invade(ICell from, ICell target);
        void OnTechnologyActivated();
        void RedrawIfChanged();
    }
}
