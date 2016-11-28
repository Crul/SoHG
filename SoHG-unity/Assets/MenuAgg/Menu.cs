using Sohg.CrossCutting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Sohg.MenuAgg
{
    public class Menu : BaseComponent
    {
        [SerializeField]
        private Button playButton;
        [SerializeField]
        private Button aboutButton;
        [SerializeField]
        private GameObject aboutPanel;
        [SerializeField]
        private Button closeAboutButton;

        public void Awake()
        {
            playButton.onClick.AddListener(() => SceneManager.LoadScene("SohgGame"));
            aboutButton.onClick.AddListener(() => aboutPanel.SetActive(true));
            closeAboutButton.onClick.AddListener(() => aboutPanel.SetActive(false));
        }
    }
}
