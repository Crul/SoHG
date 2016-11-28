using Sohg.SocietyAgg.UI;
using Sohg.CrossCutting;
using Sohg.GameAgg.Contracts;
using Sohg.SocietyAgg.Contracts;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Sohg.SocietyAgg.UI
{
    // TODO hide when society is dead
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(RectTransform))]
    public class SocietyInfo : BaseComponent, ISocietyInfo
    {
        [SerializeField]
        private Text societyName;
        [SerializeField]
        private GameObject actionsPanel;
        [SerializeField]
        private GameObject effectsPanel;
        [SerializeField]
        private GameObject propertiesPanel;
        [SerializeField]
        private SocietyProperty[] societyProperties;

        private int colliderMargin = 10; // TODO move to SocietyInfo.colliderMargin config/inspector prop?
        private Image background;
        private RectTransform rectTransform;
        private BoxCollider2D boxCollider2D;
        private List<ISocietyActionButton> actionButtons;
        private List<ISocietyEffectIcon> effectIcons;
        private List<ISocietyPropertyInfo> propertyInfos;

        public ISociety Society { get; private set; }

        public GameObject ActionsPanel { get { return actionsPanel;  } }
        public GameObject EffectsPanel { get { return effectsPanel; } }
        public GameObject PropertiesPanel { get { return propertiesPanel; } }

        public void Initialize(IRunningGame game)
        {
            actionButtons = new List<ISocietyActionButton>();
            effectIcons = new List<ISocietyEffectIcon>();
            propertyInfos = new List<ISocietyPropertyInfo>();

            background = gameObject.GetComponent<Image>();
            rectTransform = gameObject.GetComponent<RectTransform>();
            boxCollider2D = gameObject.GetComponent<BoxCollider2D>();

            game.Definition.SocietyActions.ToList()
                .ForEach(action => InitializeAction(game, action));

            societyProperties.ToList()
                .ForEach(property => InitializeProperty(game, property));
            
            Hide();
        }

        public void Hide()
        {
            Society = null;
            gameObject.SetActive(false);
        }

        public void Show(ISociety society)
        {
            Society = society;
            societyName.text = Society.Name;
            background.color = society.Color;
            gameObject.transform.position = Society.Territory.GetCenter();
            propertyInfos.ForEach(propertyInfo => propertyInfo.SetSociety(Society));

            transform.SetAsLastSibling();
            Update();

            gameObject.SetActive(true);

            StartCoroutine(SetSize());
        }

        private IEnumerator SetSize()
        {
            yield return new WaitForFixedUpdate();
            boxCollider2D.size = new Vector2
            (
                rectTransform.rect.size.x + (colliderMargin * 2),
                rectTransform.rect.size.y + (colliderMargin * 2)
            );
            boxCollider2D.offset = new Vector2(0, rectTransform.rect.size.y / 2);
        }

        public void OnMouseExit()
        {
            Hide();
        }

        public void Update()
        {
            if (gameObject.activeSelf && Society != null)
            {
                // TODO change this with actionButton.SetSociety like propertyInfo
                actionButtons.ForEach(actionButton =>
                    actionButton.SetEnable(Society.IsActionEnabled[actionButton.SocietyAction]));

                // TODO change this with effectIcon.SetSociety like propertyInfo
                effectIcons.ForEach(effectIcon =>
                    effectIcon.SetEnable(Society.IsEffectActive[effectIcon.SocietyAction]));
            }
        }

        private void InitializeAction(IRunningGame game, ISocietyAction action)
        {
            action.Initialize(game);

            var actionButton = game.SohgFactory.CreateSocietyActionButton(action, this);
            actionButtons.Add(actionButton);

            var effectIcon = game.SohgFactory.CreateSocietyEffectIcon(action, this);
            effectIcons.Add(effectIcon);
        }

        private void InitializeProperty(IRunningGame game, SocietyProperty property)
        {
            var propertyInfo = game.SohgFactory.CreateSocietyPropertyInfo(property, this);
            propertyInfos.Add(propertyInfo);
        }
    }
}
