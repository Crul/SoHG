using System;
using UnityEngine;
using UnityEngine.UI;

namespace Sohg.CrossCutting.UI
{
    public class ValueInfo : BaseComponent
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
