using Sohg.GameAgg.Contracts;
using Sohg.Grids2D.Contracts;
using Sohg.SocietyAgg.Contracts;
using System.Linq;
using UnityEngine;

namespace Sohg.GameAgg
{
    public partial class GameEngine : IEvolvableGame
    {
        private bool? hasPlayerWon;
        
        public bool Invade(ICell from, ICell target)
        {
            var invadedTerritory = Grid.GetTerritory(target);
            var hasBeenInvaded = Grid.InvadeTerritory(from, target);
            if (hasBeenInvaded)
            {
                var invasor = Grid.GetTerritory(target);

                var invasorName = (invasor.Society != null ? invasor.Society.Name : "NONE");
                var invadedName = (invadedTerritory.Society != null ? invadedTerritory.Society.Name : "NONE");
                Debug.Log(string.Format("{0} has invaded {1}", invasorName, invadedName));

                if (invadedTerritory.Society.IsDead)
                {
                    Kill(invadedTerritory.Society);
                }
            }

            return hasBeenInvaded;
        }

        public void Kill(ISociety deathSociety)
        {
            if (!Societies.Contains(deathSociety))
            {
                return;
            }

            Societies.Remove(deathSociety);
            Species.SelectMany(species => species.Societies)
                // remove relationships first to prevent pointing to removed societies
                .Where(society => society != deathSociety).ToList()
                .ForEach(society => society.RemoveRelationship(deathSociety));

            Grid.RemoveSocietyTerritory(deathSociety.Territory);

            deathSociety.Species.Societies.Remove(deathSociety);
            Log("{0} has dissapear", deathSociety.Name);

            if (deathSociety.Species.Societies.Count == 0)
            {
                Species.Remove(deathSociety.Species);
                Log("{0} is now extinct", deathSociety.Species.Name);
            }
        }

        public void Shrink(ISociety society)
        {
            Grid.ContractSingleCell(society.Territory);

            if (society.IsDead)
            {
                Kill(society);
            }
        }

        public void Split(ISociety society)
        {
            var newSociety = SplitTerritory(society);
            var totalPopulation = society.State.Population + newSociety.State.Population;
            var totalResources = society.State.Resources + newSociety.State.Resources;
            society.State.OnSplit(newSociety, totalPopulation, totalResources);
            newSociety.State.OnSplit(society, totalPopulation, totalResources);
        }

        private ISociety SplitTerritory(ISociety society)
        {
            var newSocietyCells = Grid.SplitTerritory(society.Territory);

            var newSociety = SohgFactory.CreateSociety(society, newSocietyCells.ToArray());
            
            Grid.OnTerritorySplit(society.Territory, newSociety.Territory);

            return newSociety;
        }
    }
}
