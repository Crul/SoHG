using Sohg.CrossCutting;
using Sohg.GameAgg.Contracts;
using Sohg.SocietyAgg.Contracts;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Sohg.SocietyAgg.UI
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(CanvasGroup))]
    public class SocietyMarker : BaseComponent, ISocietyMarker
    {
        [SerializeField]
        private Text SocietyName;
        [SerializeField]
        [Range(0, 1)]
        private float transparency;

        private IRunningGame game;
        private ISociety society;
        private CanvasGroup canvasGroup;

        private void Awake()
        {
            gameObject.GetComponent<Button>().onClick
                .AddListener(() => OpenSocietyInfo());
        }

        public void Initialize(IRunningGame game, ISociety society)
        {
            this.game = game;
            this.society = society;
            canvasGroup = gameObject.GetComponent<CanvasGroup>();

            var societyColor = society.Color;
            gameObject.GetComponent<Image>().color = societyColor;

            SocietyName.text = society.Name;
            SocietyName.color = new Color
            (
                societyColor.r / 3,
                societyColor.g / 3,
                societyColor.b / 3,
                1
            );

            DisableHighlight();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Highlight();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            DisableHighlight();
        }

        public void Update()
        {
            var isActive = (gameObject.activeSelf && society != null);
            if (isActive)
            {
                var hasSocietyDied = (society.TerritoryExtension == 0);
                if (hasSocietyDied)
                {
                    Destroy(gameObject);
                }
                else
                {
                    // TODO SocietyMarker.position for multiple territories society?
                    SetPosition(society.Territories[0].GetCenter());
                }
            }
        }

        private void DisableHighlight()
        {
            canvasGroup.alpha = transparency;
            SocietyName.gameObject.SetActive(false);
        }

        private void Highlight()
        {
            canvasGroup.alpha = 1;
            SocietyName.gameObject.SetActive(true);
        }

        private void OpenSocietyInfo()
        {
            game.OpenSocietyInfo(society);
        }

        private void SetPosition(Vector2 position)
        {
            var newPosition = new Vector3(position.x, position.y, transform.position.z);
            transform.position = newPosition;
        }
    }
}
