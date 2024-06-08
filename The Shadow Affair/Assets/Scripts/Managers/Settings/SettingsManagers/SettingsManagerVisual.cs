using UnityEngine;

namespace SmugRagGames.Managers.Settings
{
    public class SettingsManagerVisual : SettingsManagerBase
    {
        public SettingsManagerVisual(ScriptableObject currentSettings, ScriptableObject temporarySettings, ScriptableObject defaultSettings, SettingsManagerActions actions, short settingsType) : base(currentSettings, temporarySettings, defaultSettings, actions, settingsType)
        {
        }

        protected override void UpdateSettingsValues(ScriptableObject fromSettings, ScriptableObject toSettings)
        {
            SettingsVisualScriptableObject fromVisualSettings = (SettingsVisualScriptableObject)fromSettings;
            SettingsVisualScriptableObject toVisualSettings = (SettingsVisualScriptableObject)toSettings;

            toVisualSettings.test = fromVisualSettings.test;
        }

        public override bool HasUnsavedChanges()
        {
            SettingsVisualScriptableObject currentSettings = (SettingsVisualScriptableObject)CurrentSettings;
            SettingsVisualScriptableObject temporarySettings = (SettingsVisualScriptableObject)TemporarySettings;

            if (currentSettings.test != temporarySettings.test)
            {
                return true;
            }

            return false;
        }
    }
}