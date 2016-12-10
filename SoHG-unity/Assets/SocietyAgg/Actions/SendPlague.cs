using Sohg.SocietyAgg.Contracts;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Sohg.SocietyAgg.Actions
{
    [CreateAssetMenu(fileName = "SendPlagueAction", menuName = "SoHG/Actions/Send Plague")]
    public class SendPlague : SocietyAction
    {
        [SerializeField]
        private float plagueKillRate;

        private int secondsForRespawn = 3; // TODO move SendPlague.secondsForRespawn to config?

        public override bool IsActionEnabled(ISociety society)
        {
            var isPlayerSociety = game.PlayerSpecies.Societies.Contains(society);
            if (isPlayerSociety)
            {
                return true;
            }

            var isNeighbourOfPlayer = game.PlayerSpecies.Societies
                .Any(playerSociety => playerSociety.IsNeighbourOf(society));

            return isNeighbourOfPlayer;
        }

        public override void Execute(ISociety society)
        {
            game.ExecuteAction(ExecutePlague(society));
            game.Log("Plague sent to {0}", society.Name);
        }

        private IEnumerator ExecutePlague(ISociety society)
        {
            var effectAlreadyEnabled = (society.IsEffectActive[this]);
            if (!effectAlreadyEnabled)
            {
                society.IsEffectActive[this] = true;

                for (var i = 0; i < 10; i++)
                {
                    society.State.Kill(plagueKillRate);
                    yield return new WaitForFixedUpdate();
                }

                yield return new WaitForSeconds(secondsForRespawn);

                society.IsEffectActive[this] = false;
            }
        }
    }
}
