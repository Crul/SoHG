﻿using System.Collections.Generic;
using Sohg.GameAgg.Contracts;
using Sohg.Grids2D.Contracts;
using Sohg.SocietyAgg.Relationships;
using System;
using Sohg.SocietyAgg.Contracts;
using System.Linq;

namespace Sohg.GameAgg
{
    public partial class GameEngine : IWarPlayable
    {
        public void CreateFight(ICell from, ICell target, Action resolveAttack)
        {
            SohgFactory.CreateFight(from, target, resolveAttack);
        }

        public void EvolveWar(int time)
        {
            if (time % SohgFactory.Config.WarActionsTimeInterval == 0)
            {
                Societies.ForEach(society => society.Evolve(this));
            }

            grid.RedrawIfChanged();

            CheckWinOrLoose();
        }

        public Dictionary<ICell, ICell> GetAttackableCells(Relationship relationship)
        {
            return grid.GetInvadableCells(relationship.We.Territory, relationship.Them.Territory);
        }

        public void Invade(ICell from, ICell target)
        {
            var invadedTerritory = grid.GetTerritory(target);
            grid.InvadeTerritory(from, target);

            if (invadedTerritory.CellCount == 0)
            {
                KillSociety(invadedTerritory.Society);
            }
        }

        private void CheckWinOrLoose()
        {
            var hasPlayerLosen = (PlayerSociety.Territory.CellCount == 0);
            if (hasPlayerLosen)
            {
                endGame.Show(false);
            }
            else
            {
                var hasPlayerWon = (Societies.Count == 1);
                if (hasPlayerWon)
                {
                    endGame.Show(true);
                }
            }
        }

        private void KillSociety(ISociety deathSociety)
        {
            Societies // remove relationships first to prevent pointing to removed societies
                .Where(society => society != deathSociety).ToList()
                .ForEach(society => society.RemoveRelationship(deathSociety));

            Societies.Remove(deathSociety);
        }
    }
}
