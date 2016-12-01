using Sohg.SocietyAgg.Contracts;
using Sohg.GameAgg.Contracts;
using Sohg.CrossCutting.Pooling;
using Sohg.Grids2D.Contracts;
using UnityEngine;
using UnityEngine.UI;
using System;
namespace Sohg.SocietyAgg.UI
{
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
        private float shrinkingRate;

        [SerializeField]
        [Range(0, 1)]
        private float shrinkingLimit;

        private float deShrinkingRate;
        private CanvasGroup canvasGroup;
        private FaithRecolectableState state;

        private int faithAmount;
        private IWarPlayable game;

        public void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            deShrinkingRate = (float)Math.Pow(shrinkingRate, 3);
            state = FaithRecolectableState.Disabled;
            GetComponent<Button>().onClick.RemoveAllListeners();
            GetComponent<Button>().onClick.AddListener(() => RecolectFaith());
        }

        public void Initialize(IWarPlayable game, ICell faithCell, int faithAmount)
        {
            this.game = game;
            this.faithAmount = faithAmount;
            transform.position = faithCell.WorldPosition;
            SetScale(1);
            canvasGroup.alpha = 1;
            state = FaithRecolectableState.Initialized;
        }

        private void RecolectFaith()
        {
            if (game != null && faithAmount > 0)
            {
                game.AddFaith(faithAmount);
                state = FaithRecolectableState.Recolected;
            }
        }

        public void Update()
        {
            switch (state)
            {
                case FaithRecolectableState.Initialized:
                    var smallerScale = (transform.localScale.x * shrinkingRate);
                    if (smallerScale > shrinkingLimit)
                    {
                        SetScale(smallerScale);
                    }
                    else
                    {
                        End();
                    }
                    break;

                case FaithRecolectableState.Recolected:
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
                    break;
            }
        }

        private void End()
        {
            state = FaithRecolectableState.Disabled;
            game = null;
            faithAmount = 0;
            ReturnToPool();
        }

        private void SetScale(float newScale)
        {
            transform.localScale = new Vector3(newScale, newScale, 1);
        }
    }
}
