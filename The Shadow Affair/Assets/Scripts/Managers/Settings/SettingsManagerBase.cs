using System;
using UnityEngine;

namespace SmugRag.Managers.Settings
{
    public abstract class SettingsManagerBase
    {
        protected ScriptableObject CurrentSettings;
        protected ScriptableObject TemporarySettings;
        private ScriptableObject _defaultSettings;

        private SettingsManagerFileSaveLoad _saveLoadManager;
        private SettingsManagerFileSaveLoad.SettingType _settingType;

        protected SettingsManagerActions Actions;

        public void Setup(SettingsManagerFileSaveLoad saveLoad, SettingsManagerFileSaveLoad.SettingType settingType, ScriptableObject currentSettings, ScriptableObject defaultSettings, ScriptableObject temporarySettings, SettingsManagerActions actionsManager)
        {
            _saveLoadManager = saveLoad;
            _settingType = settingType;

            CurrentSettings = currentSettings;
            _defaultSettings = defaultSettings;
            TemporarySettings = temporarySettings;

            Actions = actionsManager;

            SubscribeToEvents();

            //Try to load settings, or default them//
            LoadSettingsFile();
        }

        #region Settings File

        private void SaveSettingsFile()
        {
            _saveLoadManager.SaveToFile();
        }

        private void LoadSettingsFile()
        {
            //Default Settings, before trying to load save ones//
            SetNewSettings(CurrentSettings, _defaultSettings);

            _saveLoadManager.LoadFromFile();
        }

        #endregion Settings File

        protected abstract void SetNewSettings(ScriptableObject targetSettingsData, ScriptableObject newSettingsData);

        public abstract bool HasUnsavedChanges();

        private void EqualizeTemporarySettingsWithCurrent()
        {
            SetNewSettings(TemporarySettings, CurrentSettings);
        }

        public void ApplyTemporarySettings()
        {
            SetNewSettings(CurrentSettings, TemporarySettings);

            SaveSettingsFile();
        }

        #region Events

        private void SubscribeToEvents()
        {
            _saveLoadManager.OnSettingsFilesRegenerationAction += SaveSettingsFile;
            _saveLoadManager.OnSettingsFileLoadAction += EqualizeTemporarySettingsWithCurrent;
        }

        public void UnsubscribeFromEvents()
        {
            _saveLoadManager.OnSettingsFilesRegenerationAction -= SaveSettingsFile;
            _saveLoadManager.OnSettingsFileLoadAction -= EqualizeTemporarySettingsWithCurrent;
        }

        #endregion Events
    }
}