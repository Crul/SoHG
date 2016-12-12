using Sohg.SocietyAgg.Contracts;
using System.Collections;
using UnityEngine;

namespace Sohg.SocietyAgg.Actions
{
    [CreateAssetMenu(fileName = "InbueWarriorSpiritAction", menuName = "SoHG/Actions/Inbue Warrior Spirit")]
    public class InbueWarriorSpirit : SocietyAction, IPowerBonus
    {
        [SerializeField]
        private float powerBonus;

        private int fixedUpdateCyclesDuration = 30; // TODO move InbueWarriorSpirit.duration to config?

        protected override IEnumerator ExecuteAction(ISociety society)
        {
            game.Log("Warrior Spirit inbued in {0}", society.Name);

            var fixedUpdateCycles = 0;
            while (fixedUpdateCycles++ < fixedUpdateCyclesDuration)
            {
                yield return new WaitForFixedUpdate();
            }
        }

        public float GetPowerBonus(ISociety society)
        {
            return society.IsEffectActive[this] ? powerBonus : 0f;
        }
    }
}
