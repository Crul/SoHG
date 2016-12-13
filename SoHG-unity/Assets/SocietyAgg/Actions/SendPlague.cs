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

        private int plagueWaveCount = 10;// TODO move SendPlague.plagueWaveCount to config?
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

        protected override IEnumerator ExecuteAction(ISociety society)
        {
            game.Log("Plague sent to {0}", society.Name);

            for (var i = 0; i < plagueWaveCount; i++)
            {
                var plagueDeathRate = (5 * plagueKillRate) / (1 - plagueKillRate);
                society.State.Kill(plagueDeathRate * society.Territory.CellCount / plagueWaveCount);
                yield return new WaitForFixedUpdate();
            }

            yield return new WaitForSeconds(secondsForRespawn);
        }
    }
}
