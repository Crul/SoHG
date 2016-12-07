using Sohg.TechnologyAgg.Contracts;
using Sohg.CrossCutting;
using Sohg.CrossCutting.Contracts;
using Sohg.GameAgg.Contracts;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Sohg.TechnologyAgg.UI
{
    [DisallowMultipleComponent]
    public class TechnologyPanel : BaseComponent, ITechnologyPanel
    {
        [SerializeField]
        private Button backButton;
        [SerializeField]
        private GameObject technologyCategoriesPanel;

        private List<ITechnologyCategoryBox> technologyCategoryBoxes;
        private IRunningGame game;

        public TechnologyPanel()
        {
            technologyCategoryBoxes = new List<ITechnologyCategoryBox>();
        }

        public void Awake()
        {
            backButton.onClick.AddListener(() => gameObject.SetActive(false));
        }

        public void Initialize(IRunningGame game, List<ITechnologyCategory> technologyCategories)
        {
            this.game = game;

            technologyCategories.ForEach(technologyCategory => AddTechnologyCategory(game.SohgFactory, technologyCategory));
        }

        public bool IsVisible()
        {
            return gameObject.activeSelf;
        }

        public void Open()
        {
            technologyCategoryBoxes.ForEach(technologyCategory => technologyCategory.SetState(game));
            gameObject.transform.SetAsLastSibling();
            gameObject.SetActive(true);
        }

        private void AddTechnologyCategory(ISohgFactory factory, ITechnologyCategory technologyCategory)
        {
            technologyCategoryBoxes
                .Add(factory.CreateTechnologyCategoryBox(game, technologyCategory, technologyCategoriesPanel));
        }
    }
}
