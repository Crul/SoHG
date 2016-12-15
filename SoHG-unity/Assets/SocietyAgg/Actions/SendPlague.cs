using Sohg.SocietyAgg.Contracts;
using System;
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
        [SerializeField]
        private int plagueWaveCount;
        
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
                var plagueDeads = Convert
                    .ToInt64(society.State.Population * plagueKillRate);

                society.State.Kill(plagueDeads);
                yield return new WaitForFixedUpdate();
            }

            yield return new WaitForSeconds(secondsForRespawn);
        }
    }
}
