using Sohg.SocietyAgg.Contracts;
using Sohg.CrossCutting.Pooling;
using Sohg.Grids2D.Contracts;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace Sohg.SocietyAgg.UI
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(CanvasGroup))]
    public class FaithRecolectable : PooledObject, IFaithRecolectable
    {
        private  enum FaithRecolectableState
        {
            Undefined,
            Disabled,
            Initialized,
            Recolected
        }

        [SerializeField]
        [Range(0, 1)]
        private float shrinkingBaseRate;

        [SerializeField]
        [Range(0, 1)]
        private float shrinkingLimit;

        private float shrinkingRate;
        private float deShrinkingRate;
        private CanvasGroup canvasGroup;
        private FaithRecolectableState state;

        private int faithAmount;
        private ISociety society;

        public void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            state = FaithRecolectableState.Disabled;

            var button = GetComponent<Button>();
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => RecolectFaith());
        }

        public void Initialize(ISociety society, ICell faithCell, int faithAmount)
        {
            this.society = society;
            this.faithAmount = faithAmount;
            transform.position = faithCell.WorldPosition;
            shrinkingRate = shrinkingBaseRate + ((1 - shrinkingBaseRate) * society.State.FaithShrinkingRateBonus);
            deShrinkingRate = (float)Math.Pow(shrinkingRate, 3);
            SetScale(1);
            canvasGroup.alpha = 1;
            state = FaithRecolectableState.Initialized;
        }

        private void RecolectFaith()
        {
            if (state == FaithRecolectableState.Initialized)
            {
                state = FaithRecolectableState.Recolected;
                society.Species.AddFaith(faithAmount);
            }
        }

        public void Update()
        {
            switch (state)
            {
                case FaithRecolectableState.Initialized:
                    UpdateShrinking();
                    break;

                case FaithRecolectableState.Recolected:
                    UpdateDeshrinking();
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

        private void SetScale(float newScale)
        {
            transform.localScale = new Vector3(newScale, newScale, 1);
            canvasGroup.alpha = newScale;
        }

        private void UpdateShrinking()
        {
            var smallerScale = (transform.localScale.x * shrinkingRate);
            if (smallerScale > shrinkingLimit)
            {
                SetScale(smallerScale);
            }
            else
            {
                End();
            }
        }

        private void UpdateDeshrinking()
        {
            var biggerScale = (transform.localScale.x / deShrinkingRate);
            if (biggerScale < 1)
            {
                SetScale(biggerScale);
                canvasGroup.alpha = canvasGroup.alpha * deShrinkingRate;
            }
            else
            {
                End();
            }
        }
    }
}
