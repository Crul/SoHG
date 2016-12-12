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
            game.ExecuteRoutine(ExecutePlague(society));
            game.Log("Plague sent to {0}", society.Name);
        }

        private IEnumerator ExecutePlague(ISociety society)
        {
            var effectAlreadyEnabled = (society.IsEffectActive[this]);
            if (!effectAlreadyEnabled)
            {
                society.IsEffectActive[this] = true;

                var plagueWaveCount = 10;
                for (var i = 0; i < plagueWaveCount; i++)
                {
                    var plagueDeathRate = (5 * plagueKillRate) / (1 - plagueKillRate);
                    society.State.Kill(plagueDeathRate * society.Territory.CellCount / plagueWaveCount);
                    yield return new WaitForFixedUpdate();
                }

                yield return new WaitForSeconds(secondsForRespawn);

                society.IsEffectActive[this] = false;
            }
        }
    }
}
