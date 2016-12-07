using Sohg.CrossCutting;
using Sohg.GameAgg.Contracts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Sohg.GameAgg.UI
{
    [DisallowMultipleComponent]
    public class PausedPanel : BaseComponent, IPausedPanel
    {
        [SerializeField]
        private GameObject menuPopup;
        [SerializeField]
        private GameObject pausePopup;

        [SerializeField]
        private Button resumeButton;
        [SerializeField]
        private Button exitButton;

        public void Awake()
        {
            resumeButton.onClick.AddListener(() => gameObject.SetActive(false));
            exitButton.onClick.AddListener(() => SceneManager.LoadScene("Menu"));
            gameObject.SetActive(false);
        }

        public void Update()
        {
            if (Input.anyKey && pausePopup.activeSelf)
            {
                pausePopup.SetActive(false);
                gameObject.SetActive(false);
            }
        }

        public bool IsVisible()
        {
            return gameObject.activeSelf;
        }

        public void ShowMenu()
        {
            pausePopup.SetActive(false);
            menuPopup.SetActive(true);
            gameObject.SetActive(true);
        }

        public void ShowPause()
        {
            menuPopup.SetActive(false);
            pausePopup.SetActive(true);
            gameObject.SetActive(true);
        }
    }
}
