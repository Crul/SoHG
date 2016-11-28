using Sohg.SocietyAgg.Contracts;
using UnityEngine;
using UnityEngine.UI;

namespace Sohg.SocietyAgg.UI
{
    [RequireComponent(typeof(Button))]
    public class SocietyActionButton : SocietyInfoChild, ISocietyActionButton
    {
        [SerializeField]
        private Image iconImage;

        private Button button;
        
        public override void Initialize(ISocietyAction societyAction, ISocietyInfo societyInfo)
        {
            button = GetComponent<Button>();

            base.Initialize(societyAction, societyInfo); // DO NOT REMOVE

            iconImage.sprite = societyAction.ActionIcon;

            gameObject.GetComponent<Button>().onClick
                .AddListener(() => societyAction.Execute(societyInfo.Society));
        }

        public override void SetEnable(bool isEnabled)
        {
            button.interactable = isEnabled;
        }
    }
}
