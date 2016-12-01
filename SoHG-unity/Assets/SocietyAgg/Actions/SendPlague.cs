using Sohg.SocietyAgg.Contracts;
using System.Collections;
using UnityEngine;

namespace Sohg.SocietyAgg.Actions
{
    [CreateAssetMenu(fileName = "SendPlagueAction", menuName = "SoHG/Actions/Send Plague")]
    public class SendPlague : SocietyAction
    {
        [SerializeField]
        private int faithCost;
        [SerializeField]
        private float plagueKillRate;

        private int secondsForRespawn = 3; // TODO move SendPlague.secondsForRespawn to config?

        public override void Execute(ISociety society)
        {
            if (game.ConsumeFaith(faithCost))
            {
                game.ExecuteAction(ExecutePlague(society));
                game.Log("Plague sent to {0}", society.Name);
            }
        }

        private IEnumerator ExecutePlague(ISociety society)
        {
            var effectAlreadyEnabled = (society.IsEffectActive[this]);
            if (!effectAlreadyEnabled)
            {
                society.IsEffectActive[this] = true;
                society.IsActionEnabled[this] = false;

                for (var i = 0; i < 10; i++)
                {
                    society.State.Kill(plagueKillRate);
                    yield return new WaitForFixedUpdate();
                }
                
                society.IsEffectActive[this] = false;

                yield return new WaitForSeconds(secondsForRespawn);
                society.IsActionEnabled[this] = true;
            }
        }
    }
}
