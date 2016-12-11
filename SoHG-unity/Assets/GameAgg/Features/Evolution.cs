using Sohg.GameAgg.Contracts;
using Sohg.SocietyAgg.Contracts;
using UnityEngine;

namespace Sohg.GameAgg.Features
{
    [CreateAssetMenu(fileName = "EvolutionFeature", menuName = "SoHG/Features/Evolution")]
    public class Evolution : GameFeature
    {
        public override void Run(IEvolvableGame game, ISociety society)
        {
            society.Species.Evolve(game);
        }
    }
}
