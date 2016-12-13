using System;
using Sohg.GameAgg.Contracts;
using Sohg.Grids2D.Contracts;
using Sohg.SocietyAgg.Contracts;
using Sohg.SocietyAgg.UI;
using UnityEngine;
using Sohg.TechnologyAgg.Contracts;
using Sohg.SpeciesAgg.Contracts;

namespace Sohg.CrossCutting.Contracts
{
    public interface ISohgFactory
    {
        ISohgConfig Config { get; }
        IGameDefinition GameDefinition { get; }

        IEndGame CreateEndGame();
        IFaithRecolectable CreateFaith(ISociety society, ICell faithCell, int faithAmount);
        IFight CreateFight(ICell from, ICell target, Action resolveAttack);
        IGrid CreateGrid();
        IInstructions CreateInstructions();
        IRelationship CreateRelationship(ISociety we, ISociety them);
        void CreateSociety(IRunningGame game, ISpecies species, params ICell[] cells);
        ISocietyActionButton CreateSocietyActionButton(ISocietyAction action, ISocietyInfo societyInfo);
        ISocietyEffectIcon CreateSocietyEffectIcon(ISocietyAction action, ISocietyInfo societyInfo);
        ISocietyInfo CreateSocietyInfo(IRunningGame game);
        ISocietySkillIcon CreateSocietySkillIcon(ISkill skill, ISocietyInfo societyInfo);
        ITechnologyCategoryColumn CreateTechnologyCategoryColumn(IEvolvableGame game, ITechnologyCategory technologyCategory, 
            ITechnologyStatesSetter technologyStatesSetter, GameObject technologyPanel);
        ISocietyPropertyInfo CreateSocietyPropertyInfo(SocietyProperty property, ISocietyInfo societyInfo);
        ITerritory CreateTerritory(params ICell[] cells);

        IGrid GetGrid();
        void SetCanvas(Canvas boardCanvas, Canvas boardOverCanvas, Canvas fixedOverCanvas);
    }
}
