using System;
using UnityEngine;

namespace SmugRagGames.Managers.Settings
{
    public class SettingsManagerGame : SettingsManagerBase
    {
        public SettingsManagerGame(ScriptableObject currentSettings, ScriptableObject temporarySettings, ScriptableObject defaultSettings, SettingsManagerActions actions, short settingsType) : base(currentSettings, temporarySettings, defaultSettings, actions, settingsType)
        {
        }


        protected override void UpdateSettingsValues(ScriptableObject fromSettings, ScriptableObject toSettings)
        {
            SettingsGameScriptableObject fromGameSettings = (SettingsGameScriptableObject)fromSettings;
            SettingsGameScriptableObject toGameSettings = (SettingsGameScriptableObject)toSettings;

            //Language//
            toGameSettings.language = fromGameSettings.language;

            //Head Bobbing//
            toGameSettings.headBobbingDisable = fromGameSettings.headBobbingDisable;
            toGameSettings.headBobbingIntensity = fromGameSettings.headBobbingIntensity;
        }

        public override bool HasUnsavedChanges()
        {
            SettingsGameScriptableObject currentSettings = (SettingsGameScriptableObject)CurrentSettings;
            SettingsGameScriptableObject temporarySettings = (SettingsGameScriptableObject)TemporarySettings;

            //Language//
            if (currentSettings.language != temporarySettings.language)
            {
                return true;
            }

            //Head bobbing//
            if (currentSettings.headBobbingDisable != temporarySettings.headBobbingDisable)
            {
                return true;
            }

            if (Math.Abs(currentSettings.headBobbingIntensity - temporarySettings.headBobbingIntensity) > 0.01f)
            {
                return true;
            }

            return false;
        }
    }
}