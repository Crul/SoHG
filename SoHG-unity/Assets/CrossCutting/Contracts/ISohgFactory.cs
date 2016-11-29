using System;
using Sohg.GameAgg.Contracts;
using Sohg.Grids2D.Contracts;
using Sohg.SocietyAgg;
using Sohg.SocietyAgg.Contracts;
using Sohg.SocietyAgg.UI;
using UnityEngine;

namespace Sohg.CrossCutting.Contracts
{
    public interface ISohgFactory
    {
        ISohgConfig Config { get; }
        IGameDefinition GameDefinition { get; }

        IEndGame CreateEndGame();
        void CreateFight(ICell from, ICell target, Action resolveAttack);
        IInstructions CreateInstructions();
        IRelationship CreateRelationship(Society we, ISociety them);
        ISociety CreateSociety(IRunningGame game, ISocietyDefinition societyDefinition, ICell[] cells);
        ISocietyActionButton CreateSocietyActionButton(ISocietyAction action, ISocietyInfo societyInfo);
        ISocietyEffectIcon CreateSocietyEffectIcon(ISocietyAction action, ISocietyInfo societyInfo);
        ISocietyInfo CreateSocietyInfo(IRunningGame game);
        ISocietyPropertyInfo CreateSocietyPropertyInfo(SocietyProperty property, SocietyInfo societyInfo);
        ITerritory CreateTerritory(params ICell[] cells);
        IGrid GetGrid();
        void SetCanvas(Canvas canvas);
    }
}
