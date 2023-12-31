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

            ApplyChange_LookSensitivityGeneral(targetSettings, newSettings.lookSensitivityGeneral);
            ApplyChange_LookUseSeparateSensitivityAxes(targetSettings, newSettings.lookUseSeparateSensitivityAxes);
            ApplyChange_LookSensitivitySeparateAxisX(targetSettings, newSettings.lookSensitivitySeparateAxesX);
            ApplyChange_LookSensitivitySeparateAxisY(targetSettings, newSettings.lookSensitivitySeparateAxesY);
            ApplyChange_LookInvertYAxis(targetSettings, newSettings.lookInvertYAxis);
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
        
        #region Setting Change Methodes

        public void ApplyChange_LookSensitivityGeneral(SettingsControlsScriptableObject targetSettings, float newValue)
        {
            targetSettings.lookSensitivityGeneral = Mathf.Clamp(newValue, 0, 100);
        }

        public void ApplyChange_LookUseSeparateSensitivityAxes(SettingsControlsScriptableObject targetSettings, bool newValue)
        {
            targetSettings.lookUseSeparateSensitivityAxes = newValue;
        }

        public void ApplyChange_LookSensitivitySeparateAxisX(SettingsControlsScriptableObject targetSettings, float newValue)
        {
            targetSettings.lookSensitivitySeparateAxesX = Mathf.Clamp(newValue, 0, 100);
        }

        public void ApplyChange_LookSensitivitySeparateAxisY(SettingsControlsScriptableObject targetSettings, float newValue)
        {
            targetSettings.lookSensitivitySeparateAxesY = Mathf.Clamp(newValue, 0, 100);
        }

        public void ApplyChange_LookInvertYAxis(SettingsControlsScriptableObject targetSettings, bool newValue)
        {
            targetSettings.lookInvertYAxis = newValue;
        }

        #endregion Setting Change Methodes
    }
}