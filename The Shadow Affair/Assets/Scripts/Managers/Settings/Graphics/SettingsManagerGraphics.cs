using System.Collections;
using System.Collections.Generic;
using SmugRag.ScriptableObjects.Settings;
using UnityEngine;

namespace SmugRag.Managers.Settings
{
    public class SettingsManagerGraphics : SettingsManagerBase
    {
        protected override void SetNewSettings(ScriptableObject targetSettingsData, ScriptableObject newSettingsData)
        {
            SettingsGraphicsScriptableObject targetSettings = (SettingsGraphicsScriptableObject)targetSettingsData;
            SettingsGraphicsScriptableObject newSettings = (SettingsGraphicsScriptableObject)newSettingsData;

            
            Actions.InvokeOnSettingsApplyGraphics();
        }

        public override bool HasUnsavedChanges()
        {
            SettingsGraphicsScriptableObject currentSettings = (SettingsGraphicsScriptableObject)CurrentSettings;
            SettingsGraphicsScriptableObject temporarySettings = (SettingsGraphicsScriptableObject)TemporarySettings;

            return false;
        }
    }
}
