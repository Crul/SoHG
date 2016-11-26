﻿using System.Collections.Generic;
using Sohg.GameAgg.Contracts;
using Sohg.Grids2D.Contracts;
using Sohg.SocietyAgg.Relationships;
using System;

namespace Sohg.GameAgg
{
    public partial class GameEngine : IWarPlayable
    {
        public void CreateFight(ICell from, ICell target, Action onEnd)
        {
            // TODO animate fight
            onEnd();
        }

        public void EvolveWar(int time)
        {
            if (time % SohgFactory.Config.WarActionsTimeInterval == 0)
            {
                Societies.ForEach(society => society.Evolve(this));
            }

            grid.RedrawIfChanged();
        }

        public Dictionary<ICell, ICell> GetAttackableCells(Relationship relationship)
        {
            return grid.GetInvadableCells(relationship.We.Territory, relationship.Them.Territory);
        }

        public void Invade(ICell from, ICell target)
        {
            grid.InvadeTerritory(from, target);

            var invadedTerritory = grid.GetTerritory(target);
            if (invadedTerritory.CellCount == 0)
            {
            }
        }
    }
}
