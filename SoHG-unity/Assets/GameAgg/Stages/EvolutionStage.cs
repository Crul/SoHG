using Sohg.GameAgg.Features;
using Sohg.GameAgg.Contracts;
using System;
using System.Linq;
using UnityEngine;
using Sohg.Grids2D.Contracts;

namespace Sohg.GameAgg.Stages
{
    [CreateAssetMenu(fileName = "EvolutionStage", menuName = "SoHG/Stages/Evolution")]
    public class EvolutionStage : GameStage<IEvolvableGame>
    {
        private int time;
        private int currentSocietyIndex;

        [SerializeField]
        private GameFeature[] features;

        private IGameFeature[] Features {get { return features; } }

        public override void OnTerritoryClick(ITerritory territory)
        {
            if (territory.Society == null)
            {
                game.CloseSocietyInfo();
            }
            else
            {
                game.OpenSocietyInfo(territory.Society);
            }
        }

        public override void Start()
        {
            time = 0;
            currentSocietyIndex = 0;

            game.OpenInstructions(
                    "The Stories begin" + System.Environment.NewLine +
                    "The human species has born and they are not alone." + System.Environment.NewLine +
                    System.Environment.NewLine +
                    "Good luck!");

            game.Log("Hominid evolution has started!");
        }

        public override void FixedUpdate()
        {
            time++;

            // TODO fix time for full cycle
            if (!game.IsPaused() && (time % game.SohgFactory.Config.EvolutionActionsTimeInterval == 0))
            {
                currentSocietyIndex++;
                if (currentSocietyIndex >= game.Societies.Count)
                {
                    currentSocietyIndex = 0;
                }

                var society = game.Societies[currentSocietyIndex];
                Features.ToList().ForEach(feature => feature.Run(game, society));
                CheckWinOrLoose();
            }
            else
            {
                GC.Collect();
            }
        }

        private void CheckWinOrLoose()
        {
            var hasPlayerLosen = (game.PlayerSpecies.Societies.Count == 0);
            if (hasPlayerLosen)
            {
                Finish(false);
            }
            else
            {
                // TODO: this should be enough (but is not): var hasPlayerWon = (game.Species.Count == 1);
                var hasPlayerWon = (game.Species.Count(species =>  species.Societies.Sum(society => society.Territory.CellCount) > 0) == 1);
                if (hasPlayerWon)
                {
                    Finish(true);
                }
            }
        }

        private void Finish(bool hasPlayerWon)
        {
            game.FinishEvolution(hasPlayerWon);
            game.NextStage();
        }
    }
}
