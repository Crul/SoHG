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
using Sohg.SocietyAgg.UI;

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

        public void CreateFight(ICell from, ICell target, Action resolveAttack)
        {
            var fight = prefabFactory.InstantiateFight(boardCanvas, "Fight");
            fight.Initialize(from, target, Config.FightDuration, resolveAttack);
        }

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

            var societyMarker = prefabFactory.InstantiateSocietyMarker(boardCanvas, society.Name + "Marker");
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
            var societyInfo = prefabFactory.InstantiateSocietyInfo(boardCanvas, "SocietyInfo");
            societyInfo.Initialize(game);

            return societyInfo;
        }

        public ISocietyPropertyInfo CreateSocietyPropertyInfo(SocietyProperty property, SocietyInfo societyInfo)
        {
            var societyPropertyInfo = prefabFactory
                .InstantiateSocietyPropertyInfo(societyInfo.PropertiesPanel, "SocietyProperty" + property.ToString());

            societyPropertyInfo.Initialize(property);

            return societyPropertyInfo;
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
