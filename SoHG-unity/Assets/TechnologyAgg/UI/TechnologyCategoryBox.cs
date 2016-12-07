using Sohg.TechnologyAgg.Contracts;
using Sohg.CrossCutting;
using Sohg.GameAgg.Contracts;
using UnityEngine.UI;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;

namespace Sohg.TechnologyAgg.UI
{
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(ScrollRect))]
    [DisallowMultipleComponent]
    public class TechnologyCategoryBox : BaseComponent, ITechnologyCategoryBox
    {
        private GameObject scrollRectContent;
        private List<TechnologyBox> technologyBoxes;

        public GameObject Content
        {
            get
            {
                if (scrollRectContent == null)
                {
                    scrollRectContent = GetComponent<ScrollRect>().content.gameObject;
                }

                return scrollRectContent;
            }
        }

        public void Initialize(ITechnologyCategory technologyCategory)
        {
            // TODO set name 
            technologyBoxes = GetComponent<ScrollRect>().content.GetComponentsInChildren<TechnologyBox>().ToList();
            GetComponent<Image>().color = technologyCategory.Color;
        }

        public void SetState(IRunningGame game)
        {
            technologyBoxes.ForEach(technologyBox => technologyBox.SetState(game.FaithPower));
        }
    }
}
