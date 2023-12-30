using UnityEngine;

namespace SmugRag.Managers.Settings
{
    public abstract class SettingsManagerSettingsChangerBase
    {
        protected ScriptableObject TemporarySettingsBase;
        protected SettingsManagerBase SettingsManagerOption;

        public virtual void Setup(SettingsManagerBase settingsManager, ScriptableObject temporarySettings)
        {
            TemporarySettingsBase = temporarySettings;
            SettingsManagerOption = settingsManager;

            SubscribeToEvents();
        }

        protected abstract void OnSettingsFileLoad();
        protected abstract void OnSettingsFileSave();
        protected abstract void SaveTemporarySettings();

        protected void SubscribeToEvents()
        {
            SettingsManagerOption.OnSettingsFileLoadAction += OnSettingsFileLoad;
            SettingsManagerOption.OnSettingsFileSaveAction += OnSettingsFileSave;
        }

        public void UnsubscribeFromEvents()
        {
            SettingsManagerOption.OnSettingsFileLoadAction -= OnSettingsFileLoad;
            SettingsManagerOption.OnSettingsFileSaveAction -= OnSettingsFileSave;
        }
    }
}