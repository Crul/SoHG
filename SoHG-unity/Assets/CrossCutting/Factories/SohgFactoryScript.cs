using UnityEngine;
using Sohg.Grids2D.Contracts;
using Sohg.GameAgg.Contracts;
using Grids2D;
using Sohg.GameAgg;
using System.Linq;
using Sohg.GameAgg.Definition;
using Sohg.CrossCutting.Contracts;
using Sohg.SocietyAgg.Contracts;
using Sohg.SocietyAgg;

namespace Sohg.CrossCutting.Factories
{
    [CreateAssetMenu(fileName = "SohgFactory", menuName = "SoHG/Sohg Factory")]
    public class SohgFactoryScript : ScriptableBaseObject, ISohgFactory
    {
        [SerializeField]
        private PrefabFactoryScript PrefabFactory;
        [SerializeField]
        private SohgConfigScript SohgConfig;
        [SerializeField]
        private GameDefinitionScript GameDefinition;

        private Canvas boardCanvas;

        public ISohgConfig Config { get { return SohgConfig; } }

        public IRunnableGame CreateGameEngine()
        {
            var grid = GetGrid();

            return new GameEngine(this, grid, GameDefinition);
        }

        public ITerritory CreateTerritory(params ICell[] cells)
        {
            var grid = GetGrid();
            var territoryIndex = grid.TerritoryCount;
            var territory = new Territory(territoryIndex);
            grid.AddTerritory(territory, cells);

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

        public ISociety CreateSociety(ISocietyDefinition societyDefinition, ICell[] cells)
        {
            if (cells.Length == 0)
            {
                cells = new ICell[]
                {
                    GetGrid().GetRandomCell(cell => cell.IsSocietyUnassigned)
                };
            }

            var territory = CreateTerritory(cells);
            var society = new Society(societyDefinition, territory);
            territory.SetSociety(society);

            var societyMarker = PrefabFactory.InstantiateSocietyMarker(boardCanvas, society.Name);
            societyMarker.Initialize(society);

            cells.ToList().ForEach(cell => cell.SetSocietyAssigned());

            return society;
        }
    }
}
