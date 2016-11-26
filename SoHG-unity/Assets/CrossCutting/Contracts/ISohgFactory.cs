using Sohg.GameAgg.Contracts;
using Sohg.Grids2D.Contracts;
using Sohg.SocietyAgg.Contracts;
using UnityEngine;

namespace Sohg.CrossCutting.Contracts
{
    public interface ISohgFactory
    {
        ISohgConfig Config { get; }

        IRunnableGame CreateGameEngine();
        ITerritory CreateTerritory(params ICell[] cells);
        ISociety CreateSociety(ISocietyDefinition societyDefinition, ICell[] cells);
        void SetCanvas(Canvas canvas);
    }
}
