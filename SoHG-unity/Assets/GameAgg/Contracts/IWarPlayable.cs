using Sohg.Grids2D.Contracts;
using Sohg.SocietyAgg.Relationships;
using System.Collections.Generic;
using System;

namespace Sohg.GameAgg.Contracts
{
    public interface IWarPlayable : IRunningGame
    {
        Dictionary<ICell, ICell> GetAttackableCells(Relationship relationship);
        void EvolveWar(int time);
        void CreateFight(ICell from, ICell target, Action onEnd);
        void Invade(ICell from, ICell target);
    }
}
