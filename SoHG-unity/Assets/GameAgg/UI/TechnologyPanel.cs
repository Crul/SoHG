﻿using Sohg.CrossCutting;
using Sohg.CrossCutting.Contracts;
using Sohg.GameAgg.Contracts;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Sohg.GameAgg.UI
{
    public class TechnologyPanel : BaseComponent, ITechnologyPanel
    {
        [SerializeField]
        private Button backButton;

        private List<ITechnologyButton> buttons;
        private IRunningGame game;

        public TechnologyPanel()
        {
            buttons = new List<ITechnologyButton>();
        }

        public void Awake()
        {
            backButton.onClick.AddListener(() => gameObject.SetActive(false));
        }

        public void Initialize(IRunningGame game)
        {
            this.game = game;

            game.GameDefinition.Technologies.ToList()
                .ForEach(technology => AddTechnology(game.SohgFactory, technology));
        }

        public bool IsVisible()
        {
            return gameObject.activeSelf;
        }

        public void Open()
        {
            buttons.ForEach(button => button.SetState(game.FaithPower));
            gameObject.transform.SetAsLastSibling();
            gameObject.SetActive(true);
        }

        private void AddTechnology(ISohgFactory factory, ITechnology technology)
        {
            buttons.Add(factory.CreateTechnologyButton(game, gameObject, technology));
        }
    }
}