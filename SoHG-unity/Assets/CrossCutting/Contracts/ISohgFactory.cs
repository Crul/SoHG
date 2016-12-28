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
        IGameDefinition GameDefinition { get; }

        IFaithRecolectable CreateFaith(ISociety society, ICell faithCell, int faithAmount);
        IFight CreateFight(IRunningGame game, IRelationship relationship, ICell from, ICell target, Action resolveAttack);
        ISociety CreateSociety(IRunningGame game, ISociety originSociety, params ICell[] cells);
        void CreateSociety(IRunningGame game, ISpecies species, params ICell[] cells);
        ISocietyActionButton CreateSocietyActionButton(ISocietyAction action, ISocietyInfo societyInfo);
        ISocietyEffectIcon CreateSocietyEffectIcon(ISocietyAction action, ISocietyInfo societyInfo);
        ISocietySkillIcon CreateSocietySkillIcon(ISkill skill, ISocietyInfo societyInfo);
        ITechnologyCategoryColumn CreateTechnologyCategoryColumn(IEvolvableGame game, ITechnologyCategory technologyCategory,
            ITechnologyStatesSetter technologyStatesSetter, GameObject technologyPanel);
        ISocietyPropertyInfo CreateSocietyPropertyInfo(SocietyProperty property, ISocietyInfo societyInfo);
        ITerritory CreateTerritory(string name, params ICell[] cells);

        void SetCanvas(Canvas boardCanvas, Canvas boardOverCanvas, Canvas fixedOverCanvas);
    }
}
