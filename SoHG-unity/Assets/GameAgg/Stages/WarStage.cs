using Sohg.GameAgg.Contracts;
using UnityEngine;

namespace Sohg.GameAgg.Stages
{
    [CreateAssetMenu(fileName = "WarStage", menuName = "SoHG/Stages/War")]
    public class WarStage : GameStage<IWarPlayable>
    {
        private int time;

        public override void Start()
        {
            time = 0;
            game.OpenInstructions(
                "The Stories begin" + System.Environment.NewLine +
                "The human species has born and they are not alone." + System.Environment.NewLine +
                System.Environment.NewLine +
                "Good luck!");
            game.Log("Hominid war has started!");
        }

        public override void FixedUpdate()
        {
            if (!game.IsPaused())
            {
                game.EvolveWar(++time);
            }
        }
    }
}
