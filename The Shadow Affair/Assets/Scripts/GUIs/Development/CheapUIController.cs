using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using SmugRagGames.Managers.Input;
using SmugRagGames.Managers.Settings;

namespace SmugRagGames.GUI
{
    public class CheapUIController : MonoBehaviour
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

        void OnStart()
        {
            SceneManager.LoadSceneAsync(1);
        }

        void OnSettings()
        {
        }

        void OnExit()
        {
            Application.Quit();
        }

        private void OnEnable()
        {
            _startButton.clicked += OnStart;
            _settingsButton.clicked += OnSettings;
            _exitButton.clicked += OnExit;

            CursorManager.SetForUI();
        }

        private void OnDisable()
        {
            _startButton.clicked -= OnStart;
            _settingsButton.clicked -= OnSettings;
            _exitButton.clicked -= OnExit;

            CursorManager.SetForGameplay();
        }
    }
}