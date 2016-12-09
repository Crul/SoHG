using Sohg.SocietyAgg.Contracts;
using UnityEngine;
using UnityEngine.UI;

namespace Sohg.SocietyAgg.UI
{
    public class SocietyEffectIcon : SocietyInfoChild, ISocietyEffectIcon
    {
        [SerializeField]
        private Image iconImage;

        protected ISocietyAction societyAction { get; private set; }

        public void Initialize(ISocietyAction societyAction, ISocietyInfo societyInfo)
        {
            Initialize(societyInfo);
            this.societyAction = societyAction;

            iconImage.sprite = societyAction.ActionIcon;
        }

        public void Update()
        {
            var isEffectActive = (society.IsEffectActive[societyAction]);
            gameObject.SetActive(isEffectActive);
        }
    }
}
