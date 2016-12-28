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

            Grid.RemoveSocietyTerritories(deathSociety.Territories);

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
            var territoryToShrink = society.Territories
                .OrderBy(territory => Random.Range(0f, 1f))
                .First();

            Grid.ContractSingleCell(territoryToShrink);

            if (society.IsDead)
            {
                Kill(society);
            }
        }

        public void Split(ISociety society)
        {
            ISociety newSociety;
            if (society.Territories.Count > 1)
            {
                newSociety = SplitMultipleTerritorySociety(society);
            }
            else
            {
                newSociety = SplitSingleTerritorySociety(society);
            }

            var totalPopulation = society.State.Population + newSociety.State.Population;
            var totalResources = society.State.Resources + newSociety.State.Resources;
            society.State.OnSplit(newSociety, totalPopulation, totalResources);
            newSociety.State.OnSplit(society, totalPopulation, totalResources);
        }

        private ISociety SplitMultipleTerritorySociety(ISociety society)
        {
            var newSocietyTerritory = society.Territories
                .OrderBy(territory => Random.Range(0f, 1f))
                .First();

            var newSociety = SohgFactory.CreateSociety(society, newSocietyTerritory);

            Grid.OnTerritorySplit(newSociety.Territories[0]);

            return newSociety;
        }

        private ISociety SplitSingleTerritorySociety(ISociety society)
        {
            var territory = society.Territories[0];
            var newSocietyCells = Grid.SplitTerritory(territory);

            var newSociety = SohgFactory.CreateSociety(society, newSocietyCells.ToArray());
            
            Grid.OnTerritorySplit(territory, newSociety.Territories[0]);

            return newSociety;
        }
    }
}
