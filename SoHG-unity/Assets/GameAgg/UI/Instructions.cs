using Sohg.CrossCutting;
using Sohg.GameAgg.Contracts;
using UnityEngine;
using UnityEngine.UI;

namespace Sohg.GameAgg.UI
{
    public class Instructions : BaseComponent, IInstructions
    {
        [SerializeField]
        private Button closeButton;
        [SerializeField]
        private Text instructionsText;

        public void Awake()
        {
            closeButton.onClick.AddListener(() => gameObject.SetActive(false));
        }

        public bool IsOpened()
        {
            return gameObject.activeSelf;
        }

        public void Show(string text)
        {
            instructionsText.text = text;
            transform.SetAsLastSibling();
            gameObject.SetActive(true);
        }
    }
}
