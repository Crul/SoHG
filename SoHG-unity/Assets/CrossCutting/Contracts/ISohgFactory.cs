using Sohg.GameAgg.Contracts;
using Sohg.Grids2D.Contracts;
using Sohg.SocietyAgg;
using Sohg.SocietyAgg.Contracts;
using UnityEngine;

namespace Sohg.CrossCutting.Contracts
{
    public interface ISohgFactory
    {
        ISohgConfig Config { get; }
        IGameDefinition GameDefinition { get; }

        IRelationship CreateRelationship(Society we, ISociety them);
        ISociety CreateSociety(IRunningGame game, ISocietyDefinition societyDefinition, ICell[] cells);
        ITerritory CreateTerritory(params ICell[] cells);
        IGrid GetGrid();
        void SetCanvas(Canvas canvas);
    }
}
