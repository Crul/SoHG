using Sohg.Grids2D.Contracts;
using Sohg.SocietyAgg.Relationships;
using Sohg.TechnologyAgg.Contracts;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Sohg.GameAgg.Contracts
{
    public interface IWarPlayable : IRunningGame
    {
        void OnTechnologyActivated();
        bool CheckDependentTechnologies(ITechnologyDependent technologyDependent);
        void CreateFight(ICell from, ICell target, Action resolveAttack);
        void EndWar(bool hasPlayerWon);
        void EvolveWar(int time);
        void ExecuteAction(IEnumerator actionExecution);
        Dictionary<ICell, ICell> GetAttackableCells(Relationship relationship);
        void Invade(ICell from, ICell target);
    }
}
