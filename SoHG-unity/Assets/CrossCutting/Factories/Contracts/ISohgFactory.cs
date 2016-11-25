using Sohg.Grids2D.Contracts;
using UnityEngine;

namespace Sohg.CrossCutting.Factories.Contracts
{
    public interface ISohgFactory
    {
        IGrid CreateGrid(Canvas canvas);
    }
}
