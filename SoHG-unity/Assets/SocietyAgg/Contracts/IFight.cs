using Sohg.Grids2D.Contracts;
using System;

namespace Sohg.SocietyAgg.Contracts
{
    public interface IFight
    {
        void Initialize(ICell from, ICell target, int time, Action resolveAttack);
    }
}
