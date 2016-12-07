using Sohg.CrossCutting;
using Sohg.GameAgg.Contracts;
using Sohg.TechnologyAgg.Contracts;
using UnityEngine;
using UnityEngine.UI;

namespace Sohg.TechnologyAgg.UI
{
    [RequireComponent(typeof(Button))]
    public class TechnologyBox : BaseComponent, ITechnologyBox
    {
        [SerializeField]
        private Image technologyIcon;

        private Button button;

        private IRunningGame game;
        private ITechnology technology;
        private bool hasBeenActivated;

        public void Initialize(IRunningGame game, ITechnology technology)
        {
            hasBeenActivated = false;
            this.game = game;
            this.technology = technology;
            technologyIcon.sprite = technology.TechnologyIcon;

            button = GetComponent<Button>();
            button.onClick.AddListener(() => ActivateTechnology());
        }

        public void SetState(int faithPower)
        {
            if (hasBeenActivated)
            {
                return;
            }

            var isActivatable = (technology.FaithCost <= faithPower);
            button.interactable = isActivatable;
            if (isActivatable)
            {
                technologyIcon.color = new Color(1, 1, 1, 1f);
            }
            else
            {
                technologyIcon.color = new Color(0.2f, 0.2f, 0.2f, 0.6f);
            }
        }

        private void ActivateTechnology()
        {
            game.OnTechnologyActivated(technology);

            hasBeenActivated = true;
            var activatedColor = new Color(0.5f, 1, 0.5f, 0.8f);
            technologyIcon.color = activatedColor;
            var buttonColors = button.colors;
            buttonColors.disabledColor = activatedColor;
            button.colors = buttonColors;
            button.interactable = false;
        }
    }
}
