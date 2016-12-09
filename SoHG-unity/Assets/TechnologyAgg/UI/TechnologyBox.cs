using Sohg.CrossCutting;
using Sohg.GameAgg.Contracts;
using Sohg.TechnologyAgg.Contracts;
using UnityEngine;
using UnityEngine.UI;

namespace Sohg.TechnologyAgg.UI
{
    [DisallowMultipleComponent]
    public class TechnologyBox : BaseComponent, ITechnologyBox
    {
        [SerializeField]
        private Button button;
        [SerializeField]
        private Image technologyIcon;

        private IWarPlayable game;
        private ITechnology technology;
        private ITechnologyStatesSetter technologyStatesSetter;
        private bool hasBeenActivated;

        public void Initialize(IWarPlayable game, ITechnology technology,
            ITechnologyStatesSetter technologyStatesSetter)
        {
            hasBeenActivated = false;
            this.game = game;
            this.technology = technology;
            this.technologyStatesSetter = technologyStatesSetter;

            button.onClick.AddListener(() => ActivateTechnology());
            technologyIcon.sprite = technology.TechnologyIcon;
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
            if (technology.Activate(game))
            {
                hasBeenActivated = true;
                var activatedColor = new Color(0.5f, 1, 0.5f, 0.8f);
                technologyIcon.color = activatedColor;
                var buttonColors = button.colors;
                buttonColors.disabledColor = activatedColor;
                button.colors = buttonColors;
                button.interactable = false;

                technologyStatesSetter.SetTechnologiesStates();
            }
        }
    }
}
