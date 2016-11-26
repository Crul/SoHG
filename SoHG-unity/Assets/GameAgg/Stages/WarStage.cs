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
