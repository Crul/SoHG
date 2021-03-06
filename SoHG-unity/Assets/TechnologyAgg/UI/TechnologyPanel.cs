﻿using Sohg.TechnologyAgg.Contracts;
using Sohg.CrossCutting;
using Sohg.CrossCutting.Contracts;
using Sohg.GameAgg.Contracts;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sohg.CrossCutting.UI;

namespace Sohg.TechnologyAgg.UI
{
    [DisallowMultipleComponent]
    public class TechnologyPanel : BaseComponent, ITechnologyPanel, ITechnologyStatesSetter
    {
        [SerializeField]
        private Button backButton;
        [SerializeField]
        private GameObject technologyCategoriesPanel;
        [SerializeField]
        private ValueInfo availableFaithInfo;

        private List<ITechnologyCategoryColumn> technologyCategoryColumns;
        private IEvolvableGame game;

        public TechnologyPanel()
        {
            technologyCategoryColumns = new List<ITechnologyCategoryColumn>();
        }

        public void Awake()
        {
            backButton.onClick.AddListener(() => gameObject.SetActive(false));
        }

        public void Initialize(IEvolvableGame game, List<ITechnologyCategory> technologyCategories)
        {
            this.game = game;

            technologyCategories.ForEach(technologyCategory => 
                AddTechnologyCategory(game.SohgFactory, technologyCategory));
        }

        public bool IsVisible()
        {
            return gameObject.activeSelf;
        }

        public void Open()
        {
            SetTechnologiesStates();
            gameObject.transform.SetAsLastSibling();
            gameObject.SetActive(true);
        }

        public void SetTechnologiesStates()
        {
            availableFaithInfo.SetValue(game.PlayerSpecies.FaithPower.ToString("### ### ##0"));
            technologyCategoryColumns.ForEach(technologyCategory => technologyCategory.SetState(game));
        }

        private void AddTechnologyCategory(ISohgFactory factory, ITechnologyCategory technologyCategory)
        {
            technologyCategoryColumns
                .Add(factory.CreateTechnologyCategoryColumn(technologyCategory, this, technologyCategoriesPanel));
        }
    }
}
