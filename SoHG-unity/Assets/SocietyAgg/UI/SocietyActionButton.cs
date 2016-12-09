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
        private ISocietyAction societyAction;

        public void Initialize(ISocietyAction societyAction, ISocietyInfo societyInfo)
        {
            Initialize(societyInfo);
            this.societyAction = societyAction;

            button = GetComponent<Button>();
            iconImage.sprite = societyAction.ActionIcon;

            gameObject.GetComponent<Button>().onClick
                .AddListener(() => societyAction.Execute(society));
        }

        public void Update()
        {
            var isButtonEnabled = HasPlayerEnoughFaithPower()
                && !IsEffectActive()
                && IsActionEnabled();

            button.interactable = isButtonEnabled;
        }

        private bool HasPlayerEnoughFaithPower()
        {
            return game.PlayerSpecies.FaithPower > societyAction.FaithCost;
        }

        private bool IsEffectActive()
        {
            return society.IsEffectActive[societyAction];
        }

        private bool IsActionEnabled()
        {
            return societyAction.IsActionEnabled(society);
        }
    }
}
