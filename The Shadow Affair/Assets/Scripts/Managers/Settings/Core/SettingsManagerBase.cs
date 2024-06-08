using UnityEngine;

namespace SmugRagGames.Managers.Settings
{
    public abstract class SettingsManagerBase
    {
        protected SettingsManagerBase(ScriptableObject currentSettings, ScriptableObject temporarySettings, ScriptableObject defaultSettings, SettingsManagerActions actions, short settingsType)
        {
            CurrentSettings = currentSettings;
            TemporarySettings = temporarySettings;
            _defaultSettings = defaultSettings;
            _actions = actions;
            _settingsType = settingsType;
        }

        #region Scriptable Objects

        protected readonly ScriptableObject CurrentSettings;

        protected readonly ScriptableObject TemporarySettings;

        private readonly ScriptableObject _defaultSettings;

        #endregion Scriptable Objects

        private readonly SettingsManagerActions _actions;

        private readonly short _settingsType;

        #region Update Settings

        protected abstract void UpdateSettingsValues(ScriptableObject fromSettings, ScriptableObject toSettings);

        public void UpdateSettingsCurrentToTemporary()
        {
            UpdateSettingsValues(CurrentSettings, TemporarySettings);
        }

        public void UpdateSettingsTemporaryToCurrent()
        {
            UpdateSettingsValues(TemporarySettings, CurrentSettings);
            _actions.InvokeOnSettingsApply(_settingsType);
        }

        public void UpdateSettingsDefaultToCurrent()
        {
            UpdateSettingsValues(_defaultSettings, CurrentSettings);
            _actions.InvokeOnSettingsApply(_settingsType);
            UpdateSettingsCurrentToTemporary();
        }

        #endregion Update Settings

        public abstract bool HasUnsavedChanges();
    }
}