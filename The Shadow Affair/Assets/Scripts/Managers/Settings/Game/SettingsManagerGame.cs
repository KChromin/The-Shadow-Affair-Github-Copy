using System.Collections;
using System.Collections.Generic;
using SmugRag.ScriptableObjects.Settings;
using UnityEngine;

namespace SmugRag.Managers.Settings
{
    public class SettingsManagerGame : SettingsManagerBase
    {
        protected override void SetNewSettings(ScriptableObject targetSettingsData, ScriptableObject newSettingsData)
        {
            SettingsGameScriptableObject targetSettings = (SettingsGameScriptableObject)targetSettingsData;
            SettingsGameScriptableObject newSettings = (SettingsGameScriptableObject)newSettingsData;

            ApplyChange_Language(targetSettings, newSettings.language);
            ApplyChange_HeadBobbingDisabled(targetSettings, newSettings.headBobbingDisabled);

            //Invoke Settings Update Event//
            Actions.InvokeOnSettingsApplyGame();
        }

        public override bool HasUnsavedChanges()
        {
            SettingsGameScriptableObject currentSettings = (SettingsGameScriptableObject)CurrentSettings;
            SettingsGameScriptableObject temporarySettings = (SettingsGameScriptableObject)TemporarySettings;

            if (currentSettings.language != temporarySettings.language)
            {
                return true;
            }

            if (currentSettings.headBobbingDisabled != temporarySettings.headBobbingDisabled)
            {
                return true;
            }

            return false;
        }

        #region Setting Change Methodes

        public void ApplyChange_Language(SettingsGameScriptableObject targetSettings, short newValue)
        {
            targetSettings.language = newValue;
        }

        public void ApplyChange_HeadBobbingDisabled(SettingsGameScriptableObject targetSettings, bool newValue)
        {
            targetSettings.headBobbingDisabled = newValue;
        }

        #endregion Settings Change Methodes
    }
}