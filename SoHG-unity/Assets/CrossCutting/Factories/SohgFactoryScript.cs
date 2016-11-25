using UnityEngine;
using Sohg.CrossCutting.Factories.Contracts;
using Sohg.Grids2D.Contracts;
using Sohg.GameAgg.Contracts;
using Grids2D;
using Sohg.GameAgg;

namespace Sohg.CrossCutting.Factories
{
    [CreateAssetMenu(fileName = "SohgFactory", menuName = "SoHG/Sohg Factory")]
    public class SohgFactoryScript : ScriptableBaseObject, ISohgFactory
    {
        public PrefabFactoryScript PrefabFactory;
        private Canvas boardCanvas;
        
        public IRunnableGame CreateGameEngine()
        {
            var grid = GetGrid();

            return new GameEngine(this, grid);
        }

        public ITerritory CreateTerritory()
        {
            var grid = GetGrid();
            var territoryIndex = grid.TerritoryCount;
            var territory = new Territory(territoryIndex);
            grid.AddTerritory(territory);

            return territory;
        }

        public void SetCanvas(Canvas boardCanvas)
        {
            this.boardCanvas = boardCanvas;
        }

        private IGrid GetGrid()
        {
            var grid = (IGrid)Grid2D.instance;
            if (grid == null)
            {
                grid = PrefabFactory.InstantiateGrid(boardCanvas);
            }

            return grid;
        }
    }
}
