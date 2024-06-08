using System;
using UnityEngine;

namespace SmugRagGames.Managers.Settings
{
    [Serializable]
    public class SettingsManagerData
    {
        [field: Header("Game")]
        [field: SerializeField]
        public SettingsGameScriptableObject Game { get; set; }

        [field: Header("Display")]
        [field: SerializeField]
        public SettingsDisplayScriptableObject Display { get; set; }

        [field: Header("Visual")]
        [field: SerializeField]
        public SettingsVisualScriptableObject Visual { get; set; }

        [field: Header("Audio")]
        [field: SerializeField]
        public SettingsAudioScriptableObject Audio { get; set; }

        [field: Header("Control")]
        [field: SerializeField]
        public SettingsControlScriptableObject Control { get; set; }
    }
}