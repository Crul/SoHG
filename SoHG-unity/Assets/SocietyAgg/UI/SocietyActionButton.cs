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

        public void Awake()
        {
            button = GetComponent<Button>();
            gameObject.GetComponent<Button>().onClick.AddListener(() => ExecuteAction());
        }

        public void Initialize(ISocietyAction societyAction, ISocietyInfo societyInfo)
        {
            Initialize(societyInfo);
            this.societyAction = societyAction;

            iconImage.sprite = societyAction.ActionIcon;
        }

        public void Update()
        {
            var isButtonEnabled = HasPlayerEnoughFaithPower()
                && !IsEffectActive()
                && IsActionEnabled();

            button.interactable = isButtonEnabled;
        }

        private void ExecuteAction()
        {
            if (game.PlayerSpecies.ConsumeFaith(societyAction.FaithCost))
            {
                societyAction.Execute(society);
                societyInfo.Refresh();
            }
        }

        private bool HasPlayerEnoughFaithPower()
        {
            return game.PlayerSpecies.FaithPower >= societyAction.FaithCost;
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
