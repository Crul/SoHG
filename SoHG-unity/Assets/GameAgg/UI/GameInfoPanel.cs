using System;
using Sohg.CrossCutting;
using Sohg.GameAgg.Contracts;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Sohg.TechnologyAgg.UI;
using Sohg.TechnologyAgg.Contracts;

namespace Sohg.GameAgg.UI
{
    [DisallowMultipleComponent]
    public class GameInfoPanel : BaseComponent, IGameInfoPanel
    {
        [SerializeField]
        private Button menuButton;
        [SerializeField]
        private Button pauseButton;
        [SerializeField]
        private Button technologyButton;
        [SerializeField]
        private Text outputText;
        [SerializeField]
        private GameStatusInfo gameStatusInfo;
        [SerializeField]
        private PausedPanel pausedPanel;
        [SerializeField]
        private TechnologyPanel technologyPanel;

        private int outputTextLines = 4; // TOOO read output text lines from scene
        private int newLineLength = Environment.NewLine.Length;

        public IPausedPanel PausedPanel { get { return pausedPanel; } }
        public IGameStatusInfo GameStatusInfo { get { return gameStatusInfo; } }
        public ITechnologyPanel TechnologyPanel { get { return technologyPanel; } }

        public void Awake()
        {
            outputText.text = string.Empty;
            Enumerable.Range(0, outputTextLines - 1).ToList().ForEach(
                i => outputText.text += Environment.NewLine);

            pauseButton.onClick.AddListener(() => PausedPanel.ShowPause());
            menuButton.onClick.AddListener(() => PausedPanel.ShowMenu());
            technologyButton.onClick.AddListener(() => technologyPanel.Open());

            technologyButton.interactable = false;
        }

        public void EnableTechnologyTree()
        {
            technologyButton.interactable = true;
        }

        public void LogOutput(string log)
        {
            outputText.text = outputText.text
                .Substring(outputText.text.IndexOf(Environment.NewLine) + newLineLength);
            outputText.text += Environment.NewLine + log;
        }
    }
}
