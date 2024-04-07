using SmugRag.ScriptableObjects.Settings;
using UnityEngine;

namespace SmugRag.Managers.Settings
{
    public class SettingsManagerDisplay : SettingsManagerBase
    {
        protected override void SetNewSettings(ScriptableObject targetSettingsData, ScriptableObject newSettingsData)
        {
            SettingsDisplayScriptableObject targetSettings = (SettingsDisplayScriptableObject)targetSettingsData;
            SettingsDisplayScriptableObject newSettings = (SettingsDisplayScriptableObject)newSettingsData;

            #region Apply All Settings

            ApplyChange_ResolutionAspectRatioMode(targetSettings, newSettings.resolutionAspectRatioMode);
            ApplyChange_ResolutionPreset(targetSettings, newSettings.resolutionPreset);
            ApplyChange_ResolutionUseCustom(targetSettings, newSettings.resolutionUseCustom);
            ApplyChange_ResolutionCustomHeight(targetSettings, newSettings.resolutionCustomHeight);
            ApplyChange_ResolutionCustomWidth(targetSettings, newSettings.resolutionCustomWidth);
            ApplyChange_VSyncMode(targetSettings, newSettings.vSyncMode);
            ApplyChange_FramerateCapCustom(targetSettings, newSettings.framerateCapCustom);
            ApplyChange_FramerateCapValue(targetSettings, newSettings.framerateCapValue);

            #endregion Apply All Settings

            Actions.InvokeOnSettingsApplyDisplay();
        }

        public override bool HasUnsavedChanges()
        {
            SettingsDisplayScriptableObject currentSettings = (SettingsDisplayScriptableObject)CurrentSettings;
            SettingsDisplayScriptableObject temporarySettings = (SettingsDisplayScriptableObject)TemporarySettings;

            #region Check every setting

            if (currentSettings.resolutionAspectRatioMode != temporarySettings.resolutionAspectRatioMode)
            {
                return true;
            }

            if (currentSettings.resolutionPreset != temporarySettings.resolutionPreset)
            {
                return true;
            }

            if (currentSettings.resolutionUseCustom != temporarySettings.resolutionUseCustom)
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

            if (currentSettings.vSyncMode != temporarySettings.vSyncMode)
            {
                return true;
            }

            if (currentSettings.framerateCapCustom != temporarySettings.framerateCapCustom)
            {
                return true;
            }

            if (currentSettings.framerateCapValue != temporarySettings.framerateCapValue)
            {
                return true;
            }

            #endregion Check every setting

            return false;
        }

        #region Setting Change Methodes

        public void ApplyChange_ResolutionAspectRatioMode(SettingsDisplayScriptableObject targetSettings, short newValue)
        {
            targetSettings.resolutionAspectRatioMode = newValue;
        }

        public void ApplyChange_ResolutionPreset(SettingsDisplayScriptableObject targetSettings, short newValue)
        {
            targetSettings.resolutionPreset = newValue;
        }

        public void ApplyChange_ResolutionUseCustom(SettingsDisplayScriptableObject targetSettings, bool newValue)
        {
            targetSettings.resolutionUseCustom = newValue;
        }

        public void ApplyChange_ResolutionCustomHeight(SettingsDisplayScriptableObject targetSettings, int newValue)
        {
            targetSettings.resolutionCustomHeight = newValue;
        }

        public void ApplyChange_ResolutionCustomWidth(SettingsDisplayScriptableObject targetSettings, int newValue)
        {
            targetSettings.resolutionCustomWidth = newValue;
        }

        public void ApplyChange_VSyncMode(SettingsDisplayScriptableObject targetSettings, short newValue)
        {
            targetSettings.vSyncMode = newValue;
        }

        public void ApplyChange_FramerateCapCustom(SettingsDisplayScriptableObject targetSettings, bool newValue)
        {
            targetSettings.framerateCapCustom = newValue;
        }

        public void ApplyChange_FramerateCapValue(SettingsDisplayScriptableObject targetSettings, short newValue)
        {
            targetSettings.framerateCapValue = newValue;
        }

        #endregion Setting Change Methodes
    }
}