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
        private ValueInfo yearInfo;
        [SerializeField]
        private ValueInfo populationInfo;
        [SerializeField]
        private ValueInfo faithPowerInfo;
        [SerializeField]
        private ValueInfo totalFaithInfo;

        private IRunningGame game;
        private int currentUpdatesWithNoYearIncrement;
        private int lastUpdatesWithNoYearIncrement;
        private int realYear;

        public int DisplayingYear { get; private set; }

        public void Awake()
        {
            yearInfo.SetValue("-");
            populationInfo.SetValue("-");
            faithPowerInfo.SetValue("-");
            totalFaithInfo.SetValue("-");
        }

        public void SetGame(IRunningGame game)
        {
            this.game = game;
            DisplayingYear = 0;
            realYear = -1;
        }

        public void Update()
        {
            if (game != null && !game.IsPaused() && game.PlayerSpecies != null)
            {
                Refresh();
            }
        }

        private void Refresh()
        {
            DisplayingYear += GetYearIncrement();
            yearInfo.SetValue(DisplayingYear.ToString("### ### ### ### ### ##0")); // TODO number format

            var population = game.PlayerSpecies.Societies.Sum(society => society.State.Population);
            populationInfo
                .SetValue(population.ToString("### ### ### ### ### ##0")); // TODO number format

            faithPowerInfo
                .SetValue(game.PlayerSpecies.FaithPower.ToString("### ### ### ### ### ##0")); // TODO number format

            totalFaithInfo
                .SetValue(game.PlayerSpecies.TotalFaith.ToString("### ### ### ### ### ##0")); // TODO number format
        }

        private int GetYearIncrement()
        {
            if (realYear != game.Year)
            {
                realYear = game.Year;
                lastUpdatesWithNoYearIncrement = (currentUpdatesWithNoYearIncrement == 0 ? 1 : currentUpdatesWithNoYearIncrement);
                currentUpdatesWithNoYearIncrement = 0;
            }
            currentUpdatesWithNoYearIncrement++;

            return System.Math.Max(0, (realYear - DisplayingYear)
                / System.Math.Max(1, lastUpdatesWithNoYearIncrement - currentUpdatesWithNoYearIncrement));
        }
    }
}
