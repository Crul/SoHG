using Grids2D;
using Sohg.CrossCutting.Contracts;
using Sohg.Grids2D.Contracts;
using Sohg.GameAgg;
using Sohg.GameAgg.Contracts;
using Sohg.SocietyAgg.Contracts;
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
        private GameDefinition gameDefinition;

        private Canvas boardCanvas;
        private Canvas boardOverCanvas;
        private Canvas fixedOverCanvas;

        public IGameDefinition GameDefinition { get { return gameDefinition; } }

        public IEndGame CreateEndGame()
        {
            return prefabFactory.InstantiateEndGame(boardCanvas);
        }

        public IFaithRecolectable CreateFaith(ISociety society, ICell faithCell, int faithAmount)
        {
            var faithRecolectable = prefabFactory.InstantiateFaithRecolectable(boardOverCanvas, "FaithRecolectable");
            faithRecolectable.Initialize(society, faithCell, faithAmount);

            return faithRecolectable;
        }

        public IFight CreateFight(IRunningGame game, IRelationship relationship, ICell from, ICell target, Action resolveAttack)
        {
            var fight = prefabFactory.InstantiateFight(boardOverCanvas, "Fight");
            fight.Initialize(relationship, from, target, game.GameDefinition.FightDuration, resolveAttack);

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

        public ISociety CreateSociety(IRunningGame game, ISociety originSociety, params ICell[] cells)
        {
            var societyConstructor = (Func<ITerritory, Society>)
                ((territory) => new Society(this, originSociety, territory));

            var society = CreateSociety(game, societyConstructor, cells);

            game.Societies
                .ForEach(otherSociety => AddSocietyRelationships(game.GameDefinition, society, otherSociety, originSociety));

            return society;
        }

        public void CreateSociety(IRunningGame game, ISpecies species, params ICell[] cells)
        {
            var societyConstructor = (Func<ITerritory, Society>)
                ((territory) => new Society(this, species, territory));

            var society = CreateSociety(game, societyConstructor, cells);

            game.Societies
                .ForEach(otherSociety => AddSocietyRelationships(game.GameDefinition, society, otherSociety));
        }

        private ISociety CreateSociety(IRunningGame game, Func<ITerritory, Society> societyConstructor, ICell[] cells)
        {
            if (cells.Length == 0)
            {
                cells = new ICell[]
                {
                    GetGrid().GetRandomCell(cell => cell.IsNonSocietyTerritory)
                };
            }

            var territory = CreateTerritory("SocietyTerritory", cells);
            var society = societyConstructor(territory);
            territory.SetSociety(society);

            var societyMarker = prefabFactory.InstantiateSocietyMarker(boardOverCanvas, society.Name + "Marker");
            societyMarker.Initialize(game, society);

            society.Species.Societies.Add(society);
            game.Societies.Add(society);

            return society;
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

        public ITechnologyCategoryColumn CreateTechnologyCategoryColumn(IEvolvableGame game, ITechnologyCategory technologyCategory,
            ITechnologyStatesSetter technologyStatesSetter, GameObject technologyPanel)
        {
            var technologyCategoryColumn = prefabFactory.InstantiateTechnologyCategoryColumn(technologyPanel, technologyCategory.Name);

            technologyCategory.Technologies.Reverse().ToList()
                .ForEach(technology => CreateTechnologyBox(game, technology, technologyCategory, technologyCategoryColumn, technologyStatesSetter));

            technologyCategoryColumn.Initialize(technologyCategory);

            return technologyCategoryColumn;
        }

        public ITerritory CreateTerritory(string name, params ICell[] cells)
        {
            var grid = GetGrid();
            var territoryIndex = grid.TerritoryCount;
            var territory = new Territory(territoryIndex);
            territory.name = name;
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

        private void AddSocietyRelationships(IGameDefinition gameDefinitiion, ISociety society, ISociety otherSociety, ISociety originSociety = null)
        {
            var isOtherOrigin = (otherSociety == originSociety);
            var originOtherSocietyRelationship = (originSociety == null || isOtherOrigin ? null
                : otherSociety.GetRelationship(originSociety));

            // TODO add special relationship if isOtherOrigin 

            var otherSocietyRelationship = new Relationship(gameDefinition, otherSociety, society, originOtherSocietyRelationship);
            otherSociety.AddRelationship(otherSocietyRelationship);

            var originSocietyRelationship = (originSociety == null || isOtherOrigin ? null
                : originSociety.GetRelationship(otherSociety));

            var societyRelationship = new Relationship(gameDefinition, society, otherSociety, originSocietyRelationship);
            society.AddRelationship(societyRelationship);
        }

        private ITechnologyBox CreateTechnologyBox(IEvolvableGame game, ITechnology technology, ITechnologyCategory technologyCategory,
            ITechnologyCategoryColumn technologyCategoryColumn, ITechnologyStatesSetter technologyStatesSetter)
        {
            var technologyBox = prefabFactory.InstantiateTechnologyBox(technologyCategoryColumn.Content, technology.Name);
            technologyBox.Initialize(game, technology, technologyStatesSetter);

            return technologyBox;
        }
    }
}
