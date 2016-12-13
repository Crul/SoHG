using Sohg.TechnologyAgg.Contracts;
using Sohg.CrossCutting;
using Sohg.GameAgg.Contracts;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Sohg.TechnologyAgg.UI
{
    [RequireComponent(typeof(Image))]
    [DisallowMultipleComponent]
    public class TechnologyCategoryColumn : BaseComponent, ITechnologyCategoryColumn
    {
        [SerializeField]
        private RectTransform scrollRectContent;
        [SerializeField]
        private Text title;

        private List<TechnologyBox> technologyBoxes;

        public GameObject Content { get { return scrollRectContent.gameObject; } }

        public void Initialize(ITechnologyCategory technologyCategory)
        {
            GetComponent<Image>().color = technologyCategory.Color;
            title.text = technologyCategory.Name;

            technologyBoxes = GetComponentsInChildren<TechnologyBox>().ToList();
            var scrollContentSize = scrollRectContent.sizeDelta;

            var boxesHeight = technologyBoxes.Sum(box => box.GetComponent<RectTransform>().sizeDelta.y);
            if (technologyBoxes.Count > 1)
            {
                boxesHeight += (technologyBoxes.Count - 1) * scrollRectContent.GetComponent<VerticalLayoutGroup>().spacing;
            }

            scrollContentSize.y = System.Math.Max(boxesHeight, GetComponent<RectTransform>().sizeDelta.y);
            scrollRectContent.sizeDelta = scrollContentSize;
        }

        public void SetState(IRunningGame game)
        {
            technologyBoxes.ForEach(technologyBox => technologyBox.SetState(game.PlayerSpecies.FaithPower));
        }
    }
}
