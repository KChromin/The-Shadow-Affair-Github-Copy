using System;
using UnityEngine;

namespace SmugRag.Managers.Settings
{
    public abstract class SettingsManagerBase
    {
        protected ScriptableObject CurrentSettings;
        private ScriptableObject _defaultSettings;
        protected ScriptableObject TemporarySettings;

        private SettingsManagerFileSaveLoad _saveLoadManager;
        private SettingsManagerFileSaveLoad.SettingType _settingType;

        public Action OnSettingsFileLoadAction;
        public Action OnSettingsFileSaveAction;

        public void Setup(SettingsManagerFileSaveLoad saveLoad, SettingsManagerFileSaveLoad.SettingType settingType, ScriptableObject currentSettings, ScriptableObject defaultSettings, ScriptableObject temporarySettings)
        {
            _saveLoadManager = saveLoad;
            _settingType = settingType;

            CurrentSettings = currentSettings;
            _defaultSettings = defaultSettings;
            TemporarySettings = temporarySettings;
            SubscribeToEvents();

            //Try to load settings, or default them//
            LoadSettingsFile();
        }

        protected void SaveSettingsFile()
        {
            _saveLoadManager.SaveToJson(CurrentSettings, _settingType);

            OnSettingsFileSaveAction?.Invoke();
        }

        protected void LoadSettingsFile()
        {
            //Default Settings, before trying to load save ones//
            SetNewSettings(CurrentSettings, _defaultSettings);

            _saveLoadManager.LoadFromJson(CurrentSettings, SettingsManagerFileSaveLoad.SettingType.Controls);

            OnSettingsFileLoadAction?.Invoke();
        }

        protected abstract void SetNewSettings(ScriptableObject targetSettingsData, ScriptableObject newSettingsData);

        public abstract void EqualizeTemporarySettingsWithCurrent();

        public abstract void SaveTemporarySettings();
        
        public abstract bool HasUnsavedChanges();

        protected void SubscribeToEvents()
        {
            _saveLoadManager.SettingsFilesRegenerationAction += SaveSettingsFile;
        }

        public void UnsubscribeFromEvents()
        {
            _saveLoadManager.SettingsFilesRegenerationAction -= SaveSettingsFile;
        }
    }
}