using System;
using UnityEngine;
using SmugRag.Templates.Singletons;

namespace SmugRag.Managers.Settings
{
    public class SettingsManager : SingletonPersistentManager<SettingsManager>
    {
        #region Settings Scriptable Objects

        [field: Header("Current Settings")]
        [field: SerializeField]
        public SettingsManagerData CurrentSettings { get; private set; }

        [Header("Default Settings")]
        [SerializeField]
        private SettingsManagerData defaultSettings;

        [Header("Temporary Settings")]
        [SerializeField]
        private SettingsManagerData temporarySettings;

        #endregion Settings Scriptable Objects

        private SettingsManagerActions _settingsActions;

        private SettingsManagerFileSaveLoad _fileSaveLoadManager;

        //Settings Managers//
        //Game//
        private SettingsManagerGame _gameManager;

        //Display//
        private SettingsManagerDisplay _displayManager;

        //Graphics//
        private SettingsManagerGraphics _graphicsManager;

        //Controls//
        private SettingsManagerControls _controlsManager;

        //Audio//
        private SettingsManagerAudio _audioManager;
        private SettingsUpdaterAudio _audioUpdater;

        [Space]
        public bool forceUpdateSettings;

        private void Update()
        {
            if (forceUpdateSettings)
            {
                _gameManager.ApplyTemporarySettings();
                _displayManager.ApplyTemporarySettings();
                _graphicsManager.ApplyTemporarySettings();
                _controlsManager.ApplyTemporarySettings();
                _audioManager.ApplyTemporarySettings();
                forceUpdateSettings = false;
            }
        }

        #region Setup

        protected override void Awake()
        {
            base.Awake();

            CreateAndSetupManagers();
        }

        private void CreateAndSetupManagers()
        {
            //Setup Event Action Manager//
            _settingsActions = new SettingsManagerActions();

            //File save and load//
            _fileSaveLoadManager = new SettingsManagerFileSaveLoad();
            _fileSaveLoadManager.Setup(CurrentSettings.Game, CurrentSettings.Display, CurrentSettings.Graphics, CurrentSettings.Audio, CurrentSettings.Controls);

            //Game//
            _gameManager = new SettingsManagerGame();
            _gameManager.Setup(_fileSaveLoadManager, SettingsManagerFileSaveLoad.SettingType.Game, CurrentSettings.Game, defaultSettings.Game, temporarySettings.Game, _settingsActions);

            //Display//
            _displayManager = new SettingsManagerDisplay();
            _displayManager.Setup(_fileSaveLoadManager, SettingsManagerFileSaveLoad.SettingType.Display, CurrentSettings.Display, defaultSettings.Display, temporarySettings.Display, _settingsActions);

            //Graphics//
            _graphicsManager = new SettingsManagerGraphics();
            _graphicsManager.Setup(_fileSaveLoadManager, SettingsManagerFileSaveLoad.SettingType.Graphics, CurrentSettings.Graphics, defaultSettings.Graphics, temporarySettings.Graphics, _settingsActions);

            //Controls//
            _controlsManager = new SettingsManagerControls();
            _controlsManager.Setup(_fileSaveLoadManager, SettingsManagerFileSaveLoad.SettingType.Controls, CurrentSettings.Controls, defaultSettings.Controls, temporarySettings.Controls, _settingsActions);

            //Audio//
            _audioManager = new SettingsManagerAudio();
            _audioUpdater = new SettingsUpdaterAudio();
            _audioManager.Setup(_fileSaveLoadManager, SettingsManagerFileSaveLoad.SettingType.Audio, CurrentSettings.Audio, defaultSettings.Audio, temporarySettings.Audio, _settingsActions);
            _audioUpdater.Setup(_settingsActions, CurrentSettings.Audio);
        }

        #endregion Setup

        #region OnDisable

        private void OnDisable()
        {
            UnsubscribeManagersActions();
        }

        private void UnsubscribeManagersActions()
        {
            _gameManager?.UnsubscribeFromEvents();
            _displayManager?.UnsubscribeFromEvents();
            _graphicsManager?.UnsubscribeFromEvents();
            _controlsManager?.UnsubscribeFromEvents();
            _audioManager?.UnsubscribeFromEvents();
        }

        #endregion OnDisable
    }
}