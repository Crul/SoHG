﻿using Sohg.CrossCutting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Sohg.MenuAgg
{
    [DisallowMultipleComponent]
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
        [SerializeField]
        private Button exitButton;

        public void Awake()
        {
            playButton.onClick.AddListener(() => SceneManager.LoadScene("SohgGame"));
            aboutButton.onClick.AddListener(() => aboutPanel.SetActive(true));
            closeAboutButton.onClick.AddListener(() => aboutPanel.SetActive(false));
            exitButton.onClick.AddListener(() => Application.Quit());
        }
    }
}
