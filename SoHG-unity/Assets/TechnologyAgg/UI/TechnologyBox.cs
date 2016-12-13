using Sohg.CrossCutting;
using Sohg.CrossCutting.UI;
using Sohg.GameAgg.Contracts;
using Sohg.TechnologyAgg.Contracts;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Sohg.TechnologyAgg.UI
{
    [RequireComponent(typeof(Image))]
    [DisallowMultipleComponent]
    public class TechnologyBox : BaseComponent, ITechnologyBox
    {
        [SerializeField]
        private Image technologyIcon;
        [SerializeField]
        private Image technologyIconBgr;
        [SerializeField]
        private Text technologyName;
        [SerializeField]
        private ValueInfo technologyCostInfo;

        private Button button;
        private Image background;
        private IEvolvableGame game;
        private ITechnology technology;
        private ITechnologyStatesSetter technologyStatesSetter;
        private bool hasBeenActivated;

        public void Initialize(IEvolvableGame game, ITechnology technology,
            ITechnologyStatesSetter technologyStatesSetter)
        {
            background = GetComponent<Image>();
            hasBeenActivated = false;
            this.game = game;
            this.technology = technology;
            this.technologyStatesSetter = technologyStatesSetter;
            technologyName.text = technology.Name;
            technologyCostInfo.SetValue(technology.FaithCost.ToString("### ###"));

            button = GetComponent<Button>();
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
                background.color = new Color(1, 1, 1, 0.5f);
                technologyIconBgr.color = Color.white;
                technologyIcon.color = Color.white;
                technologyCostInfo.GetComponentsInChildren<Text>().ToList()
                    .ForEach(text => text.color = Color.black);
            }
            else
            {
                var disabledColor = new Color(0.2f, 0.2f, 0.2f, 0.5f);
                background.color = disabledColor;
                technologyIconBgr.color = disabledColor;
                technologyIcon.color = disabledColor;
                technologyCostInfo.GetComponentsInChildren<Text>().ToList()
                    .ForEach(text => text.color = Color.red);
            }
        }

        private void ActivateTechnology()
        {
            if (technology.Activate(game))
            {
                hasBeenActivated = true;
                var activatedColor = new Color(0.5f, 1, 0.5f, 0.8f);
                technologyIcon.color = activatedColor;
                technologyIconBgr.color = activatedColor;
                background.color = activatedColor;
                var buttonColors = button.colors;
                buttonColors.disabledColor = activatedColor;
                button.colors = buttonColors;
                button.interactable = false;
                technologyCostInfo.gameObject.SetActive(false);

                technologyStatesSetter.SetTechnologiesStates();
            }
        }
    }
}
