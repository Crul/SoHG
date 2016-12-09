using Sohg.CrossCutting;
using Sohg.CrossCutting.UI;
using Sohg.GameAgg.Contracts;
using System.Linq;
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
            var populationAmount = game.PlayerSpecies.Societies.Sum(society => society.State.PopulationAmount);
            populationInfo
                .SetValue(populationAmount.ToString("### ### ### ### ### ##0")); // TODO number format

            faithPowerInfo
                .SetValue(game.PlayerSpecies.FaithPower.ToString("### ### ### ### ### ##0")); // TODO number format

            totalFaithInfo
                .SetValue(game.PlayerSpecies.TotalFaith.ToString("### ### ### ### ### ##0")); // TODO number format
        }
    }
}
