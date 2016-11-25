using Sohg.GameAgg.Contracts;
using Sohg.Grids2D.Contracts;
using UnityEngine;

namespace Sohg.CrossCutting.Factories.Contracts
{
    public interface ISohgFactory
    {
        IRunnableGame CreateGameEngine();
        ITerritory CreateTerritory();
        void SetCanvas(Canvas canvas);
    }
}
