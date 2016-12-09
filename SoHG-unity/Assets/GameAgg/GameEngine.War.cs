using Sohg.GameAgg.Contracts;
using Sohg.Grids2D.Contracts;
using Sohg.SocietyAgg.Relationships;
using Sohg.SocietyAgg.Contracts;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Sohg.GameAgg
{
    public partial class GameEngine : IWarPlayable
    {
        private bool? hasPlayerWon;

        public void CreateFight(ICell from, ICell target, Action resolveAttack)
        {
            SohgFactory.CreateFight(from, target, resolveAttack);
        }

        public void EndWar(bool hasPlayerWon)
        {
            this.hasPlayerWon = hasPlayerWon;
        }

        public void ExecuteAction(IEnumerator actionExecution)
        {
            StartCoroutine(actionExecution);
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

        public void RedrawIfChanged()
        {
            grid.RedrawIfChanged();
        }

        private void KillSociety(ISociety deathSociety)
        {
            Species.SelectMany(species => species.Societies)
                // remove relationships first to prevent pointing to removed societies
                .Where(society => society != deathSociety).ToList()
                .ForEach(society => society.RemoveRelationship(deathSociety));

            deathSociety.Species.Societies.Remove(deathSociety);

            if (deathSociety.Species.Societies.Count == 0)
            {
                Species.Remove(deathSociety.Species);
            }
        }
    }
}
