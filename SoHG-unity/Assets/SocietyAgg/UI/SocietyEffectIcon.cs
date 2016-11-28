using Sohg.SocietyAgg.Contracts;
using UnityEngine;
using UnityEngine.UI;

namespace Sohg.SocietyAgg.UI
{
    public class SocietyEffectIcon : SocietyInfoChild, ISocietyEffectIcon
    {
        [SerializeField]
        private Image iconImage;

        public override void Initialize(ISocietyAction societyAction, ISocietyInfo societyInfo)
        {
            base.Initialize(societyAction, societyInfo); // DO NOT REMOVE

            iconImage.sprite = societyAction.ActionIcon;
        }

        public override void SetEnable(bool isEnabled)
        {
            gameObject.SetActive(isEnabled);
        }
    }
}
