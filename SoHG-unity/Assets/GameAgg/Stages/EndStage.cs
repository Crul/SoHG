using Sohg.GameAgg.Contracts;
using UnityEngine;

namespace Sohg.GameAgg.Stages
{
    [CreateAssetMenu(fileName = "EndStage", menuName = "SoHG/Stages/End")]
    public class EndStage : GameStage<IEndableGame>
    {
        public override void Start()
        {
            game.OpenGameEnding();
            game.Log("End game!");
        }
    }
}
