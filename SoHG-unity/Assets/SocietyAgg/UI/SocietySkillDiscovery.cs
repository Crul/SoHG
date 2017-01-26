using Sohg.CrossCutting.Pooling;
using Sohg.CrossCutting.UI;
using Sohg.GameAgg.Contracts;
using Sohg.SocietyAgg.Contracts;
using UnityEngine;
using UnityEngine.UI;

namespace Sohg.SocietyAgg.UI
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(CanvasGroup))]
    [RequireComponent(typeof(Shrinkable))]
    public class SocietySkillDiscovery : PooledObject, ISocietySkillDiscovery
    {
        [SerializeField]
        private Image iconImage;
        [SerializeField]
        private Text discoveryText;

        private IRunningGame game;
        private CanvasGroup canvasGroup;
        private Shrinkable shrinkable;

        public void Awake()
        {
            shrinkable = GetComponent<Shrinkable>();
            canvasGroup = GetComponent<CanvasGroup>();

            shrinkable.OnChange(scale => SetAlpha(scale));
        }

        public void Initialize(IRunningGame game, ISkill skill, ISociety society)
        {
            this.game = game;
            iconImage.sprite = skill.SkillIcon;

            discoveryText.text = string.Format("{0} discovered!", skill.Name);

            // TODO SocietyMarker.position for multiple territories society?
            var societyCenter = society.Territories[0].GetCenter();
            var newPosition = new Vector3(societyCenter.x, societyCenter.y, transform.position.z);
            transform.position = newPosition;

            shrinkable.Initialize();
        }

        public void Update()
        {
            if (game == null || game.IsPaused())
            {
                return;
            }
            
            if (!shrinkable.UpdateDeshrinking())
            {
                ReturnToPool();
            }
        }

        private void SetAlpha(float alpha)
        {
            if (alpha < 1)
            {
                canvasGroup.alpha = alpha;
            }
            else
            {
                canvasGroup.alpha = (1 / alpha);
            }
        }
    }
}
