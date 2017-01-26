using Sohg.SocietyAgg.Contracts;
using Sohg.CrossCutting.Pooling;
using Sohg.Grids2D.Contracts;
using UnityEngine;
using UnityEngine.UI;
using Sohg.GameAgg.Contracts;
using Sohg.CrossCutting.UI;

namespace Sohg.SocietyAgg.UI
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Shrinkable))]
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(CanvasGroup))]
    public class FaithRecolectable : PooledObject, IFaithRecolectable
    {
        private enum FaithRecolectableState
        {
            Undefined,
            Disabled,
            Initialized,
            Recolected
        }

        private Button button;
        private CanvasGroup canvasGroup;
        private FaithRecolectableState state;
        private Shrinkable shrinkable;

        private IRunningGame game;
        private int faithAmount;
        private ISociety society;

        public void Awake()
        {
            button = GetComponent<Button>();
            canvasGroup = GetComponent<CanvasGroup>();
            shrinkable = GetComponent<Shrinkable>();

            state = FaithRecolectableState.Disabled;

            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => RecolectFaith());

            shrinkable.OnChange(scale => SetAlpha(scale));
        }

        public void Initialize(IRunningGame game, ISociety society, ICell faithCell, int faithAmount)
        {
            this.game = game;
            this.society = society;
            this.faithAmount = faithAmount;
            transform.position = faithCell.WorldPosition;
            shrinkable.Initialize(society.State.FaithShrinkingRateBonus);
            state = FaithRecolectableState.Initialized;
        }

        public void Update()
        {
            if (game == null || game.IsPaused())
            {
                return;
            }

            switch (state)
            {
                case FaithRecolectableState.Initialized:
                    if (!shrinkable.UpdateShrinking())
                    {
                        End();
                    }
                    break;

                case FaithRecolectableState.Recolected:
                    if (!shrinkable.UpdateDeshrinking())
                    {
                        End();
                    }
                    break;
            }
        }

        private void End()
        {
            state = FaithRecolectableState.Disabled;
            society = null;
            faithAmount = 0;
            ReturnToPool();
        }

        private void RecolectFaith()
        {
            if (state == FaithRecolectableState.Initialized)
            {
                state = FaithRecolectableState.Recolected;
                society.Species.AddFaith(faithAmount);
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
