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
        IFight CreateFight(IRelationship relationship, ICell from, ICell target, Action resolveAttack);
        ISociety CreateSociety(ISociety originSociety, params ICell[] cells);
        ISociety CreateSociety(ISociety originSociety, ITerritory territory);
        void CreateSociety(ISpecies species, params ICell[] cells);
        ISocietyActionButton CreateSocietyActionButton(ISocietyAction action, ISocietyInfo societyInfo);
        ISocietyEffectIcon CreateSocietyEffectIcon(ISocietyAction action, ISocietyInfo societyInfo);
        ISocietySkillIcon CreateSocietySkillIcon(ISkill skill, ISocietyInfo societyInfo);
        ITechnologyCategoryColumn CreateTechnologyCategoryColumn(ITechnologyCategory technologyCategory,
            ITechnologyStatesSetter technologyStatesSetter, GameObject technologyPanel);
        ISocietyPropertyInfo CreateSocietyPropertyInfo(SocietyProperty property, ISocietyInfo societyInfo);
        ITerritory CreateTerritory(string name, params ICell[] cells);

        void SetGame(IRunningGame game);
    }
}
