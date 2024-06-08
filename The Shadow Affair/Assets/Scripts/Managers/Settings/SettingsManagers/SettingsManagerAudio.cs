using UnityEngine;

namespace SmugRagGames.Managers.Settings
{
    public class SettingsManagerAudio : SettingsManagerBase
    {
        public SettingsManagerAudio(ScriptableObject currentSettings, ScriptableObject temporarySettings, ScriptableObject defaultSettings, SettingsManagerActions actions, short settingsType) : base(currentSettings, temporarySettings, defaultSettings, actions, settingsType)
        {
        }

        protected override void UpdateSettingsValues(ScriptableObject fromSettings, ScriptableObject toSettings)
        {
            SettingsAudioScriptableObject fromAudioSettings = (SettingsAudioScriptableObject)fromSettings;
            SettingsAudioScriptableObject toAudioSettings = (SettingsAudioScriptableObject)toSettings;

            //Volume//
            toAudioSettings.volumeMaster = fromAudioSettings.volumeMaster;
            toAudioSettings.volumeSfx = fromAudioSettings.volumeSfx;
            toAudioSettings.volumeMusic = fromAudioSettings.volumeMusic;
            toAudioSettings.volumeVoice = fromAudioSettings.volumeVoice;

            //Subtitles//
            toAudioSettings.subtitles = fromAudioSettings.subtitles;
        }

        public override bool HasUnsavedChanges()
        {
            SettingsAudioScriptableObject currentSettings = (SettingsAudioScriptableObject)CurrentSettings;
            SettingsAudioScriptableObject temporarySettings = (SettingsAudioScriptableObject)TemporarySettings;

            #region Volume

            //Volume//
            if (currentSettings.volumeMaster != temporarySettings.volumeMaster)
            {
                return true;
            }

            if (currentSettings.volumeSfx != temporarySettings.volumeSfx)
            {
                return true;
            }

            if (currentSettings.volumeMusic != temporarySettings.volumeMusic)
            {
                return true;
            }

            if (currentSettings.volumeVoice != temporarySettings.volumeVoice)
            {
                return true;
            }

            #endregion Volume

            //Subtitles//
            if (currentSettings.subtitles != temporarySettings.subtitles)
            {
                return true;
            }

            return false;
        }
    }
}