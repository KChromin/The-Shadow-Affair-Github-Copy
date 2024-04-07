using UnityEngine;
using FMOD.Studio;
using FMODUnity;
using SmugRag.ScriptableObjects.Settings;

namespace SmugRag.Managers.Settings
{
    public class SettingsUpdaterAudio
    {
        private SettingsManagerActions _actions;
        private SettingsAudioScriptableObject _currentAudioSettings;

        #region FMod audio buses

        private Bus _audioBusMaster;
        private Bus _audioBusSfx;
        private Bus _audioBusMusic;
        private Bus _audioBusVoice;

        #endregion FMod audio buses

        public void Setup(SettingsManagerActions actionsManager, SettingsAudioScriptableObject currentAudioSettings)
        {
            _actions = actionsManager;

            _currentAudioSettings = currentAudioSettings;

            //Assign FMOD Busses//
            _audioBusMaster = RuntimeManager.GetBus("bus:/");
            _audioBusSfx = RuntimeManager.GetBus("bus:/SFX");
            _audioBusMusic = RuntimeManager.GetBus("bus:/Music");
            _audioBusVoice = RuntimeManager.GetBus("bus:/Voice");

            SubscribeToEvents();
        }

        private void ApplyNewSettings()
        {
            _audioBusMaster.setVolume(_currentAudioSettings.audioVolumeMaster * 0.01f);
            _audioBusSfx.setVolume(_currentAudioSettings.audioVolumeSfx * 0.01f);
            _audioBusMusic.setVolume(_currentAudioSettings.audioVolumeMusic * 0.01f);
            _audioBusVoice.setVolume(_currentAudioSettings.audioVolumeVoice * 0.01f);
        }

        #region Events

        private void SubscribeToEvents()
        {
            _actions.OnSettingsApplyAudio += ApplyNewSettings;
        }

        public void UnsubscribeFromEvents()
        {
            _actions.OnSettingsApplyAudio -= ApplyNewSettings;
        }

        #endregion Events
    }
}