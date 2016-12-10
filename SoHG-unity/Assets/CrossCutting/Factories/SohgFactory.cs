using Grids2D;
using Sohg.CrossCutting.Contracts;
using Sohg.Grids2D.Contracts;
using Sohg.GameAgg;
using Sohg.GameAgg.Contracts;
using Sohg.SocietyAgg.Contracts;
using Sohg.SocietyAgg.Relationships;
using Sohg.SocietyAgg.UI;
using Sohg.SocietyAgg;
using Sohg.SpeciesAgg.Contracts;
using Sohg.TechnologyAgg.Contracts;
using System;
using System.Linq;
using UnityEngine;

namespace Sohg.CrossCutting.Factories
{
    [CreateAssetMenu(fileName = "SohgFactory", menuName = "SoHG/Sohg Factory")]
    public class SohgFactory : ScriptableBaseObject, ISohgFactory
    {
        [SerializeField]
        private PrefabFactory prefabFactory;
        [SerializeField]
        private SohgConfig sohgConfig;
        [SerializeField]
        private GameDefinition gameDefinition;

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

        public void CreateSociety(IRunningGame game, ISpecies species, ICell[] cells)
        {
            if (cells.Length == 0)
            {
                cells = new ICell[]
                {
                    GetGrid().GetRandomCell(cell => cell.IsSocietyUnassigned)
                };
            }

            var territory = CreateTerritory(cells);
            var society = new Society(this, species, territory);
            territory.SetSociety(society);

            var societyMarker = prefabFactory.InstantiateSocietyMarker(boardOverCanvas, society.Name + "Marker");
            societyMarker.Initialize(game, society);

            cells.ToList().ForEach(cell => cell.SetSocietyAssigned());

            game.Species.SelectMany(otherSpecies => otherSpecies.Societies).ToList()
                .ForEach(otherSociety =>
                {
                    otherSociety.AddRelationship(society);
                    society.AddRelationship(otherSociety);
                });

            species.Societies.Add(society);
        }

        public ISocietyActionButton CreateSocietyActionButton(ISocietyAction action, ISocietyInfo societyInfo)
        {
            var actionButton = prefabFactory.InstantiateSocietyActionButton(societyInfo.ActionsPanel, "SocietyActionButton"); // TODO SocietyActionButton name
            actionButton.Initialize(action, societyInfo);

            return actionButton;
        }

        public ISocietyEffectIcon CreateSocietyEffectIcon(ISocietyAction action, ISocietyInfo societyInfo)
        {
            var effectIcon = prefabFactory.InstantiateSocietyEffectIcon(societyInfo.EffectsPanel, "SocietyEffectIcon"); // TODO SocietyActionEffect name
            effectIcon.Initialize(action, societyInfo);

            return effectIcon;
        }

        public ISocietyInfo CreateSocietyInfo(IRunningGame game)
        {
            var societyInfo = prefabFactory.InstantiateSocietyInfo(boardOverCanvas, "SocietyInfo");
            societyInfo.Initialize(game);

            return societyInfo;
        }

        public ISocietySkillIcon CreateSocietySkillIcon(ISkill skill, ISocietyInfo societyInfo)
        {
            var skillIcon = prefabFactory.InstantiateSocietySkillIcon(societyInfo.SkillsPanel, "SocietySkillIcon"); // TODO SocietySkillIcon name
            skillIcon.Initialize(skill, societyInfo);

            return skillIcon;
        }

        public ISocietyPropertyInfo CreateSocietyPropertyInfo(SocietyProperty property, ISocietyInfo societyInfo)
        {
            var societyPropertyInfo = prefabFactory
                .InstantiateSocietyPropertyInfo(societyInfo.PropertiesPanel, "SocietyProperty" + property.ToString());

            societyPropertyInfo.Initialize(property, societyInfo);

            return societyPropertyInfo;
        }

        public ITechnologyCategoryBox CreateTechnologyCategoryBox(IWarPlayable game, ITechnologyCategory technologyCategory,
            ITechnologyStatesSetter technologyStatesSetter, GameObject technologyPanel)
        {
            var technologyCategoryBox = prefabFactory.InstantiateTechnologyCategoryBox(technologyPanel, technologyCategory.Name);

            technologyCategory.Technologies.ToList()
                .ForEach(technology => CreateTechnologyBox(game, technology, technologyCategory, technologyCategoryBox, technologyStatesSetter));

            technologyCategoryBox.Initialize(technologyCategory);

            return technologyCategoryBox;
        }

        private ITechnologyBox CreateTechnologyBox(IWarPlayable game, ITechnology technology, ITechnologyCategory technologyCategory,
            ITechnologyCategoryBox technologyCategoryBox, ITechnologyStatesSetter technologyStatesSetter)
        {
            var technologyBox = prefabFactory.InstantiateTechnologyBox(technologyCategoryBox.Content, technology.Name);
            technologyBox.Initialize(game, technology, technologyStatesSetter);

            return technologyBox;
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
