using Sohg.CrossCutting.Pooling;
using UnityEngine;
using UnityEngine.UI;

namespace Sohg.CrossCutting.UI
{
    [DisallowMultipleComponent]
    public class ValueInfo : PooledObject
    {
        [SerializeField]
        private Text titleText;
        [SerializeField]
        private Text valueText;

        public void SetTitle(string title)
        {
            titleText.text = title;
        }

        public void SetValue(string value)
        {
            valueText.text = value;
        }
    }
}
