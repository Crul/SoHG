using UnityEngine;
using Sohg.Grids2D.Contracts;
using Sohg.GameAgg.Contracts;
using Grids2D;
using System.Linq;
using Sohg.GameAgg.Definition;
using Sohg.CrossCutting.Contracts;
using Sohg.SocietyAgg.Contracts;
using System;
using Sohg.SocietyAgg.Relationships;
using Sohg.SocietyAgg.UI;
using Sohg.SocietyAgg;
using Sohg.TechnologyAgg.Contracts;

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
        private Canvas boardOverCanvas;
        private Canvas fixedOverCanvas;

        public ISohgConfig Config { get { return sohgConfig; } }
        public IGameDefinition GameDefinition { get { return gameDefinition; } }

        public IEndGame CreateEndGame()
        {
            return prefabFactory.InstantiateEndGame(boardCanvas);
        }

        public IFaithRecolectable CreateFaith(IWarPlayable game, ICell faithCell, int faithAmount)
        {
            var faithRecolectable = prefabFactory.InstantiateFaithRecolectable(boardOverCanvas, "FaithRecolectable");
            faithRecolectable.Initialize(game, faithCell, faithAmount);

            return faithRecolectable;
        }

        public IFight CreateFight(ICell from, ICell target, Action resolveAttack)
        {
            var fight = prefabFactory.InstantiateFight(boardOverCanvas, "Fight");
            fight.Initialize(from, target, Config.FightDuration, resolveAttack);

            return fight;
        }

        public IGrid CreateGrid()
        {
            return prefabFactory.InstantiateGrid(boardCanvas);
        }

        public IInstructions CreateInstructions()
        {
            return prefabFactory.InstantiateInstructions(fixedOverCanvas);
        }

        public IRelationship CreateRelationship(ISociety we, ISociety them)
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

            var societyMarker = prefabFactory.InstantiateSocietyMarker(boardOverCanvas, society.Name + "Marker");
            societyMarker.Initialize(game, society);

            cells.ToList().ForEach(cell => cell.SetSocietyAssigned());

            game.Societies.ForEach(otherSociety =>
            {
                otherSociety.AddRelationship(society);
                society.AddRelationship(otherSociety);
            });

            return society;
        }

        public ISocietyActionButton CreateSocietyActionButton(ISocietyAction action, ISocietyInfo societyInfo)
        {
            var actionButton = prefabFactory.InstantiateSocietyActionButton(societyInfo.ActionsPanel, "SocietyActionButton"); // TODO SocietyActionButtonName
            actionButton.Initialize(action, societyInfo);

            return actionButton;
        }

        public ISocietyEffectIcon CreateSocietyEffectIcon(ISocietyAction action, ISocietyInfo societyInfo)
        {
            var effectIcon = prefabFactory.InstantiateSocietyEffectIcon(societyInfo.EffectsPanel, "SocietyEffectIcon"); // TODO SocietyActionEffect
            effectIcon.Initialize(action, societyInfo);

            return effectIcon;
        }

        public ISocietyInfo CreateSocietyInfo(IRunningGame game)
        {
            var societyInfo = prefabFactory.InstantiateSocietyInfo(boardOverCanvas, "SocietyInfo");
            societyInfo.Initialize(game);

            return societyInfo;
        }

        public ISocietyPropertyInfo CreateSocietyPropertyInfo(SocietyProperty property, ISocietyInfo societyInfo)
        {
            var societyPropertyInfo = prefabFactory
                .InstantiateSocietyPropertyInfo(societyInfo.PropertiesPanel, "SocietyProperty" + property.ToString());

            societyPropertyInfo.Initialize(property);

            return societyPropertyInfo;
        }

        public ITechnologyButton CreateTechnologyButton(IRunningGame game, GameObject technologyPanel, ITechnology technology)
        {
            var technologyButton = prefabFactory.InstantiateTechnologyButton(technologyPanel, technology.Name);
            technologyButton.Initialize(game, technology);

            return technologyButton;
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
            return Grid2D.instance;
        }

        public void SetCanvas(Canvas boardCanvas, Canvas boardOverCanvas, Canvas fixedOverCanvas)
        {
            this.boardCanvas = boardCanvas;
            this.boardOverCanvas = boardOverCanvas;
            this.fixedOverCanvas = fixedOverCanvas;
        }
    }
}
