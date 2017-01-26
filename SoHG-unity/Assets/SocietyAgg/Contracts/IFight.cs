using Sohg.GameAgg.Contracts;
using Sohg.Grids2D.Contracts;
using System;

namespace Sohg.SocietyAgg.Contracts
{
    public interface IFight
    {
        void Initialize(IRelationship relationship, ICell from, ICell target, IRunningGame game, Action resolveAttack);
    }
}
