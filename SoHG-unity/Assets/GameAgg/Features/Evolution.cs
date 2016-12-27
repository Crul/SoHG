using Sohg.GameAgg.Contracts;
using Sohg.SocietyAgg.Contracts;
using System.Linq;
using UnityEngine;

namespace Sohg.GameAgg.Features
{
    [CreateAssetMenu(fileName = "EvolutionFeature", menuName = "SoHG/Features/Evolution")]
    public class Evolution : GameFeature
    {
        public override void Run(IEvolvableGame game, ISociety society)
        {
            society.Species.Evolve(game);
            society.State.Evolve();
            society.Relationships.ToList()
                .ForEach(relationship => relationship.Evolve(game.GameDefinition));
        }
    }
}
