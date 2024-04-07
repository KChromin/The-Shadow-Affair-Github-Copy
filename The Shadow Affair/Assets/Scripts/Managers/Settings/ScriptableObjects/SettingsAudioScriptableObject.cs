using UnityEngine;

namespace SmugRag.ScriptableObjects.Settings
{
    [CreateAssetMenu(fileName = "SettingsAudio", menuName = "ScriptableObjects/Settings/AudioSettings")]
    public class SettingsAudioScriptableObject : ScriptableObject
    {
        [Header("Audio Volume Settings")]
        [Range(0, 100)]
        public int audioVolumeMaster = 100;
        [Range(0, 100)]
        public int audioVolumeSfx = 80;
        [Range(0, 100)]
        public int audioVolumeMusic = 80;
        [Range(0, 100)]
        public int audioVolumeVoice = 80;
    }
}