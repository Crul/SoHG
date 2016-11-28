using Sohg.CrossCutting;
using Sohg.GameAgg.Contracts;
using Sohg.SocietyAgg.Contracts;
using UnityEngine;
using UnityEngine.UI;

namespace Sohg.SocietyAgg.UI
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(CanvasGroup))]
    public class SocietyMarker : BaseComponent, ISocietyMarker
    {
        [SerializeField]
        private Text SocietyName;
        
        private ISociety society;

        private CanvasGroup canvasGroup;
        private static float transparency = 0.85f; // TODO

        public void Initialize(IRunningGame game, ISociety society)
        {
            this.society = society;
            canvasGroup = gameObject.GetComponent<CanvasGroup>();
            gameObject.GetComponent<Image>().color = society.Color;
            SocietyName.text = society.Name;
            SocietyName.color = new Color
            (
                society.Color.r / 3,
                society.Color.g / 3,
                society.Color.b / 3
            );

            DisableHighlight();

            gameObject.GetComponent<Button>().onClick
                .AddListener(() => game.OpenSocietyInfo(society));
        }

        public void Update()
        {
            var isActive = (gameObject.activeSelf && society != null);
            if (isActive)
            {
                var hasSocietyDied = (society.Territory.CellCount == 0);
                if (hasSocietyDied)
                {
                    Destroy(gameObject);
                }
                else
                {
                    SetPosition(society.Territory.GetCenter());
                }
            }
        }

        public void OnMouseOver()
        {
            Highlight();
        }

        public void OnMouseExit()
        {
            DisableHighlight();
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

        private void SetPosition(Vector2 position)
        {
            var newPosition = new Vector3(position.x, position.y, -50); // TODO why -20 needed?
            transform.position = newPosition;
        }
    }
}
