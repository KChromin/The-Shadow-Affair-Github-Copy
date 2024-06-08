using UnityEngine;

namespace SmugRagGames.Managers.Settings
{
    [CreateAssetMenu(fileName = "AudioSettings", menuName = "ScriptableObjects/Settings/Audio")]
    public class SettingsAudioScriptableObject : ScriptableObject
    {
        [Header("Audio Volume")]
        [Range(0, 100)]
        public byte volumeMaster = 100;
        [Range(0, 100)]
        public byte volumeSfx = 80;
        [Range(0, 100)]
        public byte volumeMusic = 80;
        [Range(0, 100)]
        public byte volumeVoice = 80;

        [Header("Subtitles")]
        public bool subtitles = false;
    }
}