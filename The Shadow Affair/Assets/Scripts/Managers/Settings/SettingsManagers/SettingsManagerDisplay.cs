using UnityEngine;

namespace SmugRagGames.Managers.Settings
{
    public class SettingsManagerDisplay : SettingsManagerBase
    {
        public SettingsManagerDisplay(ScriptableObject currentSettings, ScriptableObject temporarySettings, ScriptableObject defaultSettings, SettingsManagerActions actions, short settingsType) : base(currentSettings, temporarySettings, defaultSettings, actions, settingsType)
        {
        }

        protected override void UpdateSettingsValues(ScriptableObject fromSettings, ScriptableObject toSettings)
        {
            SettingsDisplayScriptableObject fromDisplaySettings = (SettingsDisplayScriptableObject)fromSettings;
            SettingsDisplayScriptableObject toDisplaySettings = (SettingsDisplayScriptableObject)toSettings;

            //Fullscreen//
            toDisplaySettings.fullscreenMode = fromDisplaySettings.fullscreenMode;

            //Resolution//
            toDisplaySettings.aspectRatio = fromDisplaySettings.aspectRatio;
            toDisplaySettings.resolutionPreset16X9 = fromDisplaySettings.resolutionPreset16X9;
            toDisplaySettings.resolutionPreset16X10 = fromDisplaySettings.resolutionPreset16X10;
            toDisplaySettings.resolutionPreset21X9 = fromDisplaySettings.resolutionPreset21X9;

            //Custom Resolution//
            toDisplaySettings.resolutionCustom = fromDisplaySettings.resolutionCustom;
            toDisplaySettings.resolutionCustomHeight = fromDisplaySettings.resolutionCustomHeight;
            toDisplaySettings.resolutionCustomWidth = fromDisplaySettings.resolutionCustomWidth;

            //VSync//
            toDisplaySettings.vSyncMode = fromDisplaySettings.vSyncMode;

            //Custom framerate//
            toDisplaySettings.framerateCustomMax = fromDisplaySettings.framerateCustomMax;
            toDisplaySettings.framerateCustomMaxValue = fromDisplaySettings.framerateCustomMaxValue;

            //Run in background//
            toDisplaySettings.runInBackground = fromDisplaySettings.runInBackground;
        }

        // ReSharper disable once CognitiveComplexity
        public override bool HasUnsavedChanges()
        {
            SettingsDisplayScriptableObject currentSettings = (SettingsDisplayScriptableObject)CurrentSettings;
            SettingsDisplayScriptableObject temporarySettings = (SettingsDisplayScriptableObject)TemporarySettings;

            //Fullscreen//
            if (currentSettings.fullscreenMode != temporarySettings.fullscreenMode)
            {
                return true;
            }

            //Resolution//
            if (currentSettings.aspectRatio != temporarySettings.aspectRatio)
            {
                return true;
            }

            if (currentSettings.resolutionPreset16X9 != temporarySettings.resolutionPreset16X9)
            {
                return true;
            }

            if (currentSettings.resolutionPreset16X10 != temporarySettings.resolutionPreset16X10)
            {
                return true;
            }

            if (currentSettings.resolutionPreset21X9 != temporarySettings.resolutionPreset21X9)
            {
                return true;
            }

            //Custom Resolution//

            if (currentSettings.resolutionCustom != temporarySettings.resolutionCustom)
            {
                return true;
            }

            if (currentSettings.resolutionCustomHeight != temporarySettings.resolutionCustomHeight)
            {
                return true;
            }

            if (currentSettings.resolutionCustomWidth != temporarySettings.resolutionCustomWidth)
            {
                return true;
            }

            //VSync//
            if (currentSettings.vSyncMode != temporarySettings.vSyncMode)
            {
                return true;
            }

            //Custom Framerate//
            if (currentSettings.framerateCustomMax != temporarySettings.framerateCustomMax)
            {
                return true;
            }

            if (currentSettings.framerateCustomMaxValue != temporarySettings.framerateCustomMaxValue)
            {
                return true;
            }

            //Run in background//
            if (currentSettings.runInBackground != temporarySettings.runInBackground)
            {
                return true;
            }

            return false;
        }
    }
}