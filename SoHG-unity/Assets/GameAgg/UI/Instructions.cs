using System;
using Sohg.CrossCutting;
using Sohg.GameAgg.Contracts;
using UnityEngine;
using UnityEngine.UI;

namespace Sohg.GameAgg.UI
{
    [DisallowMultipleComponent]
    public class Instructions : BaseComponent, IInstructions
    {
        [SerializeField]
        private Button closeButton;
        [SerializeField]
        private Text instructionsText;

        private Action onCloseAction;

        public void Awake()
        {
            closeButton.onClick.AddListener(() => Close());
        }

        public bool IsOpened()
        {
            return gameObject.activeSelf;
        }

        public void OnClose(Action onCloseAction)
        {
            this.onCloseAction = onCloseAction;
        }

        public void Show(string text)
        {
            instructionsText.text = text;
            transform.SetAsLastSibling();
            gameObject.SetActive(true);
        }

        private void Close()
        {
            if (onCloseAction != null)
            {
                onCloseAction();
                onCloseAction = null;
            }

            gameObject.SetActive(false);
        }
    }
}
