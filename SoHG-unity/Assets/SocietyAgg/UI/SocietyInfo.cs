using Sohg.CrossCutting;
using Sohg.GameAgg.Contracts;
using Sohg.SocietyAgg.Contracts;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Sohg.SocietyAgg.UI
{
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
        private GameObject skillsPanel;
        [SerializeField]
        private SocietyProperty[] societyProperties;
        
        private int colliderMargin = 10; // TODO move to SocietyInfo.colliderMargin config/inspector prop?
        private Image background;
        private RectTransform rectTransform;
        private BoxCollider2D boxCollider2D;

        public IRunningGame Game { get; private set; }
        public ISociety Society { get; private set; }

        public GameObject ActionsPanel { get { return actionsPanel; } }
        public GameObject EffectsPanel { get { return effectsPanel; } }
        public GameObject PropertiesPanel { get { return propertiesPanel; } }
        public GameObject SkillsPanel { get { return skillsPanel; } }

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

        public void Update()
        {
            var worldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var hit = Physics2D.Raycast(worldMousePosition, -Vector2.up);
            if (hit.collider == null || hit.collider.gameObject != gameObject)
            {
                Hide();
            }
        }

        private void InitializeAction(ISocietyAction action)
        {
            Game.SohgFactory.CreateSocietyActionButton(action, this);            
            Game.SohgFactory.CreateSocietyEffectIcon(action, this);
        }

        private void InitializeProperty(SocietyProperty property)
        {
            Game.SohgFactory.CreateSocietyPropertyInfo(property, this);
        }

        private void InitializeSkill(ISkill skill)
        {
            Game.SohgFactory.CreateSocietySkillIcon(skill, this);
        }

        private void InitializeSocietyActions()
        {
            // TODO make (only disbaled) actions and effect pool objects in SocietyInfo ?

            ReturnAllChildrenToPool(actionsPanel);
            ReturnAllChildrenToPool(effectsPanel);
            ReturnAllChildrenToPool(skillsPanel);

            Society.Actions.ToList().ForEach(action => InitializeAction(action));
            Society.Skills.ToList().ForEach(skill => InitializeSkill(skill));
        }

        private void SetPosition()
        {
            var territoryCenter = Society.Territory.GetCenter();
            var societyInfoPosition = gameObject.transform.position;
            societyInfoPosition.x = territoryCenter.x;
            societyInfoPosition.y = territoryCenter.y;
            gameObject.transform.position = societyInfoPosition;

            boxCollider2D.size = new Vector2
            (
                rectTransform.rect.size.x + (colliderMargin * 2),
                rectTransform.rect.size.y + (colliderMargin * 2)
            );
            boxCollider2D.offset = new Vector2(0, rectTransform.rect.size.y / 2);
        }
    }
}
