using Sohg.GameAgg.Contracts;
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

        public void Update()
        {
            var isEffectActive = (society.IsEffectActive[societyAction]);
            gameObject.SetActive(isEffectActive);
        }
    }
}
