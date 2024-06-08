using System;
using UnityEngine;

namespace SmugRagGames.Managers.Settings
{
    public class SettingsManagerControl : SettingsManagerBase
    {
        public SettingsManagerControl(ScriptableObject currentSettings, ScriptableObject temporarySettings, ScriptableObject defaultSettings, SettingsManagerActions actions, short settingsType) : base(currentSettings, temporarySettings, defaultSettings, actions, settingsType)
        {
        }

        protected override void UpdateSettingsValues(ScriptableObject fromSettings, ScriptableObject toSettings)
        {
            SettingsControlScriptableObject fromControlSettings = (SettingsControlScriptableObject)fromSettings;
            SettingsControlScriptableObject toControlSettings = (SettingsControlScriptableObject)toSettings;

            //Look//
            toControlSettings.lookSensitivityGeneral = fromControlSettings.lookSensitivityGeneral;
            toControlSettings.lookSeparateSensitivityAxes = fromControlSettings.lookSeparateSensitivityAxes;
            toControlSettings.lookSensitivityXAxis = fromControlSettings.lookSensitivityXAxis;
            toControlSettings.lookSensitivityYAxis = fromControlSettings.lookSensitivityYAxis;
            toControlSettings.lookInvertYAxis = fromControlSettings.lookInvertYAxis;
            toControlSettings.inputCrouchToggle = fromControlSettings.inputCrouchToggle;
        }

        public override bool HasUnsavedChanges()
        {
            SettingsControlScriptableObject currentSettings = (SettingsControlScriptableObject)CurrentSettings;
            SettingsControlScriptableObject temporarySettings = (SettingsControlScriptableObject)TemporarySettings;

            //Look//
            if (Math.Abs(currentSettings.lookSensitivityGeneral - temporarySettings.lookSensitivityGeneral) > 0.01f)
            {
                return true;
            }

            if (currentSettings.lookSeparateSensitivityAxes != temporarySettings.lookSeparateSensitivityAxes)
            {
                return true;
            }

            if (Math.Abs(currentSettings.lookSensitivityXAxis - temporarySettings.lookSensitivityXAxis) > 0.01f)
            {
                return true;
            }

            if (Math.Abs(currentSettings.lookSensitivityYAxis - temporarySettings.lookSensitivityYAxis) > 0.01f)
            {
                return true;
            }

            if (currentSettings.lookInvertYAxis != temporarySettings.lookInvertYAxis)
            {
                return true;
            }

            if (currentSettings.inputCrouchToggle != temporarySettings.inputCrouchToggle)
            {
                return true;
            }

            return false;
        }
    }
}