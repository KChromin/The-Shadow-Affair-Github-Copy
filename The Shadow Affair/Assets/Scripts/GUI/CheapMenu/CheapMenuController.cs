using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

namespace SmugRag.GUI
{
    public class CheapMenuController : MonoBehaviour
    {
        private VisualElement _menuUI;

        private Button _startButton;
        private Button _settingsButton;
        private Button _exitButton;

        private void Awake()
        {
            _menuUI = GetComponent<UIDocument>().rootVisualElement;

            _startButton = _menuUI.Q<Button>("StartButton");
            _settingsButton = _menuUI.Q<Button>("SettingsButton");
            _exitButton = _menuUI.Q<Button>("ExitButton");
        }

        private void OnStart()
        {
            SceneManager.LoadSceneAsync(1);
        }

        private void OnSettings()
        {
            Debug.Log("Settings");
        }

        private void OnExit()
        {
            Application.Quit();
        }

        private void OnEnable()
        {
            _startButton.clicked += OnStart;
            _settingsButton.clicked += OnSettings;
            _exitButton.clicked += OnExit;
        }

        private void OnDisable()
        {
            _startButton.clicked -= OnStart;
            _settingsButton.clicked -= OnSettings;
            _exitButton.clicked -= OnExit;
        }
    }
}