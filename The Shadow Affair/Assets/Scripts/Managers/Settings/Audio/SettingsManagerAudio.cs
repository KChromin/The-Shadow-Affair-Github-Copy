using System.Collections;
using System.Collections.Generic;
using SmugRag.ScriptableObjects.Settings;
using UnityEngine;

namespace SmugRag.Managers.Settings
{
    public class SettingsManagerAudio : SettingsManagerBase
    {
        protected override void SetNewSettings(ScriptableObject targetSettingsData, ScriptableObject newSettingsData)
        {
            SettingsAudioScriptableObject targetSettings = (SettingsAudioScriptableObject)targetSettingsData;
            SettingsAudioScriptableObject newSettings = (SettingsAudioScriptableObject)newSettingsData;

            ApplyChange_AudioVolumeMaster(targetSettings, newSettings.audioVolumeMaster);
            ApplyChange_AudioVolumeSFX(targetSettings, newSettings.audioVolumeSfx);
            ApplyChange_AudioVolumeMusic(targetSettings, newSettings.audioVolumeMusic);
            ApplyChange_AudioVolumeVoice(targetSettings, newSettings.audioVolumeVoice);

            //Invoke Settings Update Event//
            Actions.InvokeOnSettingsApplyAudio();
        }

        public override bool HasUnsavedChanges()
        {
            SettingsAudioScriptableObject currentSettings = (SettingsAudioScriptableObject)CurrentSettings;
            SettingsAudioScriptableObject temporarySettings = (SettingsAudioScriptableObject)TemporarySettings;

            if (currentSettings.audioVolumeMaster != temporarySettings.audioVolumeMaster)
            {
                return true;
            }

            if (currentSettings.audioVolumeSfx != temporarySettings.audioVolumeSfx)
            {
                return true;
            }

            if (currentSettings.audioVolumeMusic != temporarySettings.audioVolumeMusic)
            {
                return true;
            }

            if (currentSettings.audioVolumeVoice != temporarySettings.audioVolumeVoice)
            {
                return true;
            }


            return false;
        }

        #region Setting Change Methodes

        public void ApplyChange_AudioVolumeMaster(SettingsAudioScriptableObject targetSettings, int newValue)
        {
            targetSettings.audioVolumeMaster = newValue;
        }

        public void ApplyChange_AudioVolumeSFX(SettingsAudioScriptableObject targetSettings, int newValue)
        {
            targetSettings.audioVolumeSfx = newValue;
        }

        public void ApplyChange_AudioVolumeMusic(SettingsAudioScriptableObject targetSettings, int newValue)
        {
            targetSettings.audioVolumeMusic = newValue;
        }

        public void ApplyChange_AudioVolumeVoice(SettingsAudioScriptableObject targetSettings, int newValue)
        {
            targetSettings.audioVolumeVoice = newValue;
        }

        #endregion Setting Change Methodes
    }
}