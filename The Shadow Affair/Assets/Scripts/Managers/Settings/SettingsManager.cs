using System;
using System.Collections.Generic;
using SmugRagGames.Patterns.Singleton;
using UnityEngine;

namespace SmugRagGames.Managers.Settings
{
    public class SettingsManager : SingletonPersistent<SettingsManager>
    {
        #region Settings

        [field: Header("Settings")]
        [field: SerializeField]
        public SettingsManagerData CurrentSettings { get; private set; }

        [field: SerializeField]
        public SettingsManagerData TemporarySettings { get; private set; }

        [field: SerializeField]
        protected SettingsManagerData DefaultSettings { get; private set; }

        #endregion Settings

        #region Settings Managers

        //Game//
        private SettingsManagerGame _managerGame;

        //Display//
        private SettingsManagerDisplay _managerDisplay;

        //Visual//
        private SettingsManagerVisual _managerVisual;

        //Audio//
        private SettingsManagerAudio _managerAudio;

        //Control//
        private SettingsManagerControl _managerControl;

        #endregion Settings Managers

        #region Settings Update Handlers

        //Display//
        private SettingsUpdateHandlerDisplay _updateHandlerDisplay;

        #endregion Settings Update Handlers

        #region Settings Types

        private enum SettingsType
        {
            Game,
            Display,
            Visual,
            Audio,
            Control
        }

        #endregion Settings Types

        private SettingsManagerActions _actions;

        private SettingsManagerFileHandler _fileHandler;

        #region Setup

        protected override void Awake()
        {
            base.Awake();
            Setup();

            _actions.InvokeOnSettingsFileLoadRequest();
        }

        private void Setup()
        {
            //Actions//
            _actions = new SettingsManagerActions();

            //File handler//
            _fileHandler = new SettingsManagerFileHandler(CurrentSettings, _actions);

            #region Settings Managers

            //Game//
            _managerGame = new SettingsManagerGame(CurrentSettings.Game, TemporarySettings.Game, DefaultSettings.Game, _actions, (short)SettingsType.Game);

            //Display//
            _managerDisplay = new SettingsManagerDisplay(CurrentSettings.Display, TemporarySettings.Display, DefaultSettings.Display, _actions, (short)SettingsType.Display);

            //Visual//
            _managerVisual = new SettingsManagerVisual(CurrentSettings.Visual, TemporarySettings.Visual, DefaultSettings.Visual, _actions, (short)SettingsType.Visual);

            //Audio//
            _managerAudio = new SettingsManagerAudio(CurrentSettings.Audio, TemporarySettings.Audio, DefaultSettings.Audio, _actions, (short)SettingsType.Audio);

            //Control//
            _managerControl = new SettingsManagerControl(CurrentSettings.Control, TemporarySettings.Control, DefaultSettings.Control, _actions, (short)SettingsType.Control);

            #endregion Settings Managers

            #region Update Handlers

            //Display//
            _updateHandlerDisplay = new SettingsUpdateHandlerDisplay(CurrentSettings.Display, _actions, (short)SettingsType.Display);

            #endregion Update Handlers
        }

        #endregion Setup

        #region Public methodes

        private SettingsManagerBase GetManagerBySettingsType(short settingsType)
        {
            return settingsType switch
            {
                0 => _managerGame,
                1 => _managerDisplay,
                2 => _managerVisual,
                3 => _managerAudio,
                4 => _managerControl,
                _ => null
            };
        }

        public void ApplySettings(short settingsType)
        {
            SettingsManagerBase settingsManager = GetManagerBySettingsType(settingsType);
            settingsManager.UpdateSettingsTemporaryToCurrent();
        }

        public void DefaultSettingsValue(short settingsType)
        {
            SettingsManagerBase settingsManager = GetManagerBySettingsType(settingsType);
            settingsManager.UpdateSettingsDefaultToCurrent();
        }

        public void UpdateTemporarySettings(short settingsType)
        {
            SettingsManagerBase settingsManager = GetManagerBySettingsType(settingsType);
            settingsManager.UpdateSettingsCurrentToTemporary();
        }

        public bool UpdateUnsavedChanges(short settingsType)
        {
            return settingsType switch
            {
                0 => _managerGame.HasUnsavedChanges(),
                1 => _managerDisplay.HasUnsavedChanges(),
                2 => _managerVisual.HasUnsavedChanges(),
                3 => _managerAudio.HasUnsavedChanges(),
                4 => _managerControl.HasUnsavedChanges(),
                _ => false
            };
        }

        #endregion Public methodes

        private void OnDisable()
        {
            _fileHandler?.UnsubscribeFromEvents();
        }
    }
}