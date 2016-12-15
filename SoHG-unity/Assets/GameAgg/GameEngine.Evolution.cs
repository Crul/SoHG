using Sohg.GameAgg.Contracts;
using Sohg.Grids2D.Contracts;
using Sohg.SocietyAgg.Contracts;
using System.Linq;

namespace Sohg.GameAgg
{
    public partial class GameEngine : IEvolvableGame
    {
        private bool? hasPlayerWon;

        public void FinishEvolution(bool hasPlayerWon)
        {
            Log("evolution finish");
            this.hasPlayerWon = hasPlayerWon;
        }

        public bool Invade(ICell from, ICell target)
        {
            var invadedTerritory = Grid.GetTerritory(target);
            var hasBeenInvaded = Grid.InvadeTerritory(from, target);
            if (hasBeenInvaded)
            {
                var invasor = Grid.GetTerritory(target);

                var invasorName = (invasor.Society != null ? invasor.Society.Name : "NONE");
                var invadedName = (invadedTerritory.Society != null ? invadedTerritory.Society.Name : "NONE");
                UnityEngine.Debug.Log(string.Format("{0} has invaded {1}", invasorName, invadedName));

                if (invadedTerritory.CellCount == 0)
                {
                    KillSociety(invadedTerritory.Society);
                }
            }

            return hasBeenInvaded;
        }

        private void KillSociety(ISociety deathSociety)
        {
            Societies.Remove(deathSociety);
            Species.SelectMany(species => species.Societies)
                // remove relationships first to prevent pointing to removed societies
                .Where(society => society != deathSociety).ToList()
                .ForEach(society => society.RemoveRelationship(deathSociety));

            deathSociety.Species.Societies.Remove(deathSociety);
            Log("{0} has dissapear", deathSociety.Name);

            if (deathSociety.Species.Societies.Count == 0)
            {
                Species.Remove(deathSociety.Species);
                Log("{0} is now extinct", deathSociety.Species.Name);
            }
        }
    }
}
