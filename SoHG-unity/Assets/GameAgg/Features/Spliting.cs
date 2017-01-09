using Sohg.GameAgg.Contracts;
using Sohg.SocietyAgg.Contracts;
using UnityEngine;

namespace Sohg.GameAgg.Features
{
    [CreateAssetMenu(fileName = "SplitingFeature", menuName = "SoHG/Features/Spliting")]
    public class Spliting : GameFeature
    {
        private bool isActive;

        public override void Run(IEvolvableGame game, ISociety society)
        {
            if (isActive)
            {
                SplitSociety(game, society);
            }
            else if (game.AreNonPlayerSpeciesDead)
            {
                // TODO split societies on activation
                isActive = true;
            }
        }

        private void SplitSociety(IEvolvableGame game, ISociety society)
        {
            var splitingProbabitly = society.State.SplitingProbability;
            if (splitingProbabitly == 0)
            {
                return;
            }

            var shouldSocietyBeSplit = (Random.Range(0f, 1f) < splitingProbabitly);
            if (shouldSocietyBeSplit)
            {
                game.Split(society);
            }
        }
    }
}
