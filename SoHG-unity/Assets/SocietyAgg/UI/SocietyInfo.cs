using Sohg.SocietyAgg.UI;
using Sohg.CrossCutting;
using Sohg.GameAgg.Contracts;
using Sohg.SocietyAgg.Contracts;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

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

        public IRunningGame Game { get; private set; }
        public ISociety Society { get; private set; }

        public GameObject ActionsPanel { get { return actionsPanel; } }
        public GameObject EffectsPanel { get { return effectsPanel; } }
        public GameObject PropertiesPanel { get { return propertiesPanel; } }

        public void Initialize(IRunningGame game)
        {
            Hide();

            Game = game;
            background = gameObject.GetComponent<Image>();
            rectTransform = gameObject.GetComponent<RectTransform>();
            boxCollider2D = gameObject.GetComponent<BoxCollider2D>();

            Reset();
        }

        public void Hide()
        {
            Society = null;
            gameObject.SetActive(false);
        }

        public void Reset()
        {
            ReturnAllChildrenToPool(propertiesPanel);

            propertyInfos = new List<ISocietyPropertyInfo>();
            societyProperties.ToList().ForEach(property => InitializeProperty(property));
        }

        public void Show(ISociety society)
        {
            SetSociety(society);
            SetPosition();

            transform.SetAsLastSibling();
            gameObject.SetActive(true);
        }

        private void SetSociety(ISociety society)
        {
            Society = society;
            societyName.text = Society.Name;
            background.color = society.Color;

            InitializeSocietyActions();
        }

        public void OnMouseExit()
        {
            Hide();
        }

        private void InitializeAction(ISocietyAction action)
        {
            var actionButton = Game.SohgFactory.CreateSocietyActionButton(action, this);
            actionButtons.Add(actionButton);
            
            var effectIcon = Game.SohgFactory.CreateSocietyEffectIcon(action, this);
            effectIcons.Add(effectIcon);
        }

        private void InitializeProperty(SocietyProperty property)
        {
            var propertyInfo = Game.SohgFactory.CreateSocietyPropertyInfo(property, this);
            propertyInfos.Add(propertyInfo);
        }

        private void InitializeSocietyActions()
        {
            // TODO make (only disbaled) actions and effect pool objects in SocietyInfo ?

            ReturnAllChildrenToPool(actionsPanel);
            ReturnAllChildrenToPool(effectsPanel);

            actionButtons = new List<ISocietyActionButton>();
            effectIcons = new List<ISocietyEffectIcon>();

            Society.Actions.ToList().ForEach(action => InitializeAction(action));
        }

        private void SetPosition()
        {
            gameObject.transform.position = Society.Territory.GetCenter();

            boxCollider2D.size = new Vector2
            (
                rectTransform.rect.size.x + (colliderMargin * 2),
                rectTransform.rect.size.y + (colliderMargin * 2)
            );
            boxCollider2D.offset = new Vector2(0, rectTransform.rect.size.y / 2);
        }
    }
}
