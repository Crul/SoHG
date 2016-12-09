using Sohg.Grids2D.Contracts;
using Sohg.SocietyAgg.Contracts;
using Sohg.SocietyAgg.Relationships;
using Sohg.TechnologyAgg.Contracts;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Sohg.GameAgg.Contracts
{
    public interface IWarPlayable : IRunningGame
    {
        bool CheckDependentTechnologies(ITechnologyDependent technologyDependent);
        void CreateFight(ICell from, ICell target, Action resolveAttack);
        void EmitFaith(ISociety society);
        void EndWar(bool hasPlayerWon);
        void ExecuteAction(IEnumerator actionExecution);
        Dictionary<ICell, ICell> GetAttackableCells(Relationship relationship);
        void Invade(ICell from, ICell target);
        void OnTechnologyActivated();
        void RedrawIfChanged();
    }
}
