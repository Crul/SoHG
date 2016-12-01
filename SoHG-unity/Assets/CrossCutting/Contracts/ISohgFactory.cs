using System;
using Sohg.GameAgg.Contracts;
using Sohg.Grids2D.Contracts;
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
        IFaithRecolectable CreateFaith(IWarPlayable game, ICell faithCell, int faithAmount);
        IFight CreateFight(ICell from, ICell target, Action resolveAttack);
        IGrid CreateGrid();
        IInstructions CreateInstructions();
        IRelationship CreateRelationship(ISociety we, ISociety them);
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
