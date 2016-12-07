using Sohg.CrossCutting;
using Sohg.CrossCutting.UI;
using Sohg.GameAgg.Contracts;
using UnityEngine;

namespace Sohg.GameAgg.UI
{
    [DisallowMultipleComponent]
    public class GameStatusInfo : BaseComponent, IGameStatusInfo
    {
        [SerializeField]
        private ValueInfo populationInfo;
        [SerializeField]
        private ValueInfo faithPowerInfo;
        [SerializeField]
        private ValueInfo totalFaithInfo;

        public void Awake()
        {
            populationInfo.SetValue("-");
            faithPowerInfo.SetValue("-");
            totalFaithInfo.SetValue("-");
        }

        public void SetValues(IRunningGame game)
        {
            var populationAmount = game.PlayerSociety.State.PopulationAmount;
            populationInfo
                .SetValue(populationAmount.ToString("### ### ### ### ### ##0")); // TODO number format

            faithPowerInfo
                .SetValue(game.FaithPower.ToString("### ### ### ### ### ##0")); // TODO number format

            totalFaithInfo
                .SetValue(game.TotalFaith.ToString("### ### ### ### ### ##0")); // TODO number format
        }
    }
}
