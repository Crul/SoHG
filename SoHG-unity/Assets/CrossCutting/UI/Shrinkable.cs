using System;
using UnityEngine;

namespace Sohg.CrossCutting.UI
{
    public class Shrinkable : BaseComponent
    {
        [SerializeField]
        [Range(0, 1)]
        private float shrinkingBaseRate;

        [SerializeField]
        [Range(0, 1)]
        private float shrinkingLimit;

        [SerializeField]
        [Range(1, 10)]
        private float deshrinkingLimit;

        private float shrinkingRate;
        private float deShrinkingRate;
        private Action<float> onChangeFn;
        
        public void Initialize(float shrinkingRateBonus = 0)
        {
            shrinkingRate = shrinkingBaseRate + ((1 - shrinkingBaseRate) * shrinkingRateBonus);
            deShrinkingRate = (float)Math.Pow(shrinkingRate, 3);
            SetScale(1);
        }

        public void OnChange(Action<float> onChangeFn)
        {
            this.onChangeFn = onChangeFn;
        }

        public void SetScale(float newScale)
        {
            transform.localScale = new Vector3(newScale, newScale, 1);

            if (onChangeFn != null)
            {
                onChangeFn(newScale);
            }
        }

        public bool UpdateShrinking()
        {
            var smallerScale = (transform.localScale.x * shrinkingRate);
            var shouldIShrink = (smallerScale > shrinkingLimit);
            if (shouldIShrink)
            {
                SetScale(smallerScale);
            }

            return shouldIShrink;
        }

        public bool UpdateDeshrinking()
        {
            var biggerScale = (transform.localScale.x / deShrinkingRate);
            var shouldIDeshrink = (biggerScale < deshrinkingLimit);
            if (shouldIDeshrink)
            {
                SetScale(biggerScale);
            }

            return shouldIDeshrink;
        }
    }
}
