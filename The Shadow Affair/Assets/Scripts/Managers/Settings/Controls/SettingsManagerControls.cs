using System;
using SmugRag.ScriptableObjects.Settings;
using UnityEngine;

namespace SmugRag.Managers.Settings
{
    public class SettingsManagerControls : SettingsManagerBase
    {
        protected override void SetNewSettings(ScriptableObject targetSettingsData, ScriptableObject newSettingsData)
        {
            SettingsControlsScriptableObject newSettings = (SettingsControlsScriptableObject)newSettingsData;
            SettingsControlsScriptableObject targetSettings = (SettingsControlsScriptableObject)targetSettingsData;

            targetSettings.lookSensitivityGeneral = newSettings.lookSensitivityGeneral;
            targetSettings.lookUseSeparateSensitivityAxes = newSettings.lookUseSeparateSensitivityAxes;
            targetSettings.lookSensitivitySeparateAxesX = newSettings.lookSensitivitySeparateAxesX;
            targetSettings.lookSensitivitySeparateAxesY = newSettings.lookSensitivitySeparateAxesY;
            targetSettings.lookInvertYAxis = newSettings.lookInvertYAxis;
        }

        public override void EqualizeTemporarySettingsWithCurrent()
        {
            SetNewSettings(TemporarySettings, CurrentSettings);
        }

        public override void SaveTemporarySettings()
        {
            SetNewSettings(CurrentSettings, TemporarySettings);
            
            SaveSettingsFile();
        }

        public override bool HasUnsavedChanges()
        {
            SettingsControlsScriptableObject currentSettings = (SettingsControlsScriptableObject)CurrentSettings;
            SettingsControlsScriptableObject temporarySettings = (SettingsControlsScriptableObject)TemporarySettings;

            #region Check every setting

            if (Math.Abs(currentSettings.lookSensitivityGeneral - temporarySettings.lookSensitivityGeneral) > 0.01f)
            {
                return true;
            }

            if (currentSettings.lookUseSeparateSensitivityAxes != temporarySettings.lookUseSeparateSensitivityAxes)
            {
                return true;
            }

            if (Math.Abs(currentSettings.lookSensitivitySeparateAxesX - temporarySettings.lookSensitivitySeparateAxesX) > 0.01f)
            {
                return true;
            }

            if (Math.Abs(currentSettings.lookSensitivitySeparateAxesY - temporarySettings.lookSensitivitySeparateAxesY) > 0.01f)
            {
                return true;
            }

            if (currentSettings.lookInvertYAxis != temporarySettings.lookInvertYAxis)
            {
                return true;
            }

            #endregion Check every setting

            return false;
        }
    }
}