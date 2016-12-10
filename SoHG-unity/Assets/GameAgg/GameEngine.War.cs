using Sohg.GameAgg.Contracts;
using Sohg.Grids2D.Contracts;
using Sohg.SocietyAgg.Contracts;
using System.Linq;
using System.Collections;

namespace Sohg.GameAgg
{
    public partial class GameEngine : IWarPlayable
    {
        private bool? hasPlayerWon;

        public bool CreateFight(IRelationship relationship, int fromCellIndex)
        {
            var from = grid.GetCell(fromCellIndex);
            var target = grid.GetInvadableCell(from, relationship.Them.Territory);
            if (target == null)
            {
                return false;
            }
            
            from.IsInvolvedInAttack = true;
            target.IsInvolvedInAttack = true;

            SohgFactory.CreateFight(from, target, () => relationship.ResolveAttack(this, from, target));

            return true;
        }

        public void EndWar(bool hasPlayerWon)
        {
            this.hasPlayerWon = hasPlayerWon;
        }

        public void ExecuteAction(IEnumerator actionExecution)
        {
            StartCoroutine(actionExecution);
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
