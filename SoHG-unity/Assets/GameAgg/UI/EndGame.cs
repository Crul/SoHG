using Sohg.CrossCutting;
using Sohg.GameAgg.Contracts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Sohg.GameAgg.UI
{
    [DisallowMultipleComponent]
    public class EndGame : BaseComponent, IEndGame
    {
        [SerializeField]
        private Text titleText;
        [SerializeField]
        private Text contentText;
        [SerializeField]
        private Button continueButton;
        [SerializeField]
        private Button exitButton;

        public void Awake()
        {
            continueButton.onClick.AddListener(() => gameObject.SetActive(false));
            exitButton.onClick.AddListener(() => SceneManager.LoadScene("Menu"));
            transform.SetAsLastSibling();
            gameObject.SetActive(false);
        }

        public void Show(bool hasPlayerWon)
        {
            if (hasPlayerWon)
            {
                ShowWin();
            }
            else
            {
                ShowLoose();
            }
            transform.SetAsLastSibling();
            gameObject.SetActive(true);
        }

        private void ShowWin()
        {
            titleText.color = Color.yellow;
            titleText.text = "You WIN!";

            contentText.text = "Well, that was easy. Wait for the Hard-Mode...";
        }

        private void ShowLoose()
        {
            titleText.color = Color.red;
            titleText.text = "You LOOSE!";

            contentText.text = "Really? Well, at least you ended the game : )";
        }
    }
}
