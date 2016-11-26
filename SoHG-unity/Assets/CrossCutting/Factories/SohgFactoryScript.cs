using UnityEngine;
using Sohg.Grids2D.Contracts;
using Sohg.GameAgg.Contracts;
using Grids2D;
using System.Linq;
using Sohg.GameAgg.Definition;
using Sohg.CrossCutting.Contracts;
using Sohg.SocietyAgg.Contracts;
using Sohg.SocietyAgg;
using System;
using Sohg.SocietyAgg.Relationships;

namespace Sohg.CrossCutting.Factories
{
    [CreateAssetMenu(fileName = "SohgFactory", menuName = "SoHG/Sohg Factory")]
    public class SohgFactoryScript : ScriptableBaseObject, ISohgFactory
    {
        [SerializeField]
        private PrefabFactoryScript prefabFactory;
        [SerializeField]
        private SohgConfigScript sohgConfig;
        [SerializeField]
        private GameDefinitionScript gameDefinition;

        private Canvas boardCanvas;

        public ISohgConfig Config { get { return sohgConfig; } }
        public IGameDefinition GameDefinition { get { return gameDefinition; } }

        public IRelationship CreateRelationship(Society we, ISociety them)
        {
            return new Relationship(we, them);
        }

        public ISociety CreateSociety(IRunningGame game, ISocietyDefinition societyDefinition, ICell[] cells)
        {
            if (cells.Length == 0)
            {
                cells = new ICell[]
                {
                    GetGrid().GetRandomCell(cell => cell.IsSocietyUnassigned)
                };
            }

            var territory = CreateTerritory(cells);
            var society = new Society(this, societyDefinition, territory);
            territory.SetSociety(society);

            var societyMarker = prefabFactory.InstantiateSocietyMarker(boardCanvas, society.Name);
            societyMarker.Initialize(society);

            cells.ToList().ForEach(cell => cell.SetSocietyAssigned());

            game.Societies.ForEach(otherSociety =>
            {
                otherSociety.AddRelationship(society);
                society.AddRelationship(otherSociety);
            });

            return society;
        }

        public ITerritory CreateTerritory(params ICell[] cells)
        {
            var grid = GetGrid();
            var territoryIndex = grid.TerritoryCount;
            var territory = new Territory(territoryIndex);
            grid.AddTerritory(territory, cells);

            return territory;
        }

        public IGrid GetGrid()
        {
            var grid = (IGrid)Grid2D.instance;
            if (grid == null)
            {
                grid = prefabFactory.InstantiateGrid(boardCanvas);
            }

            return grid;
        }

        public void SetCanvas(Canvas boardCanvas)
        {
            this.boardCanvas = boardCanvas;
        }
    }
}
