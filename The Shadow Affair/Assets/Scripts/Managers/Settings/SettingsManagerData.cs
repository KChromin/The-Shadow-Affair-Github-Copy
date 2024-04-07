using System;
using SmugRag.ScriptableObjects.Settings;
using UnityEngine;

namespace SmugRag.Managers.Settings
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
        
        [field: Header("Graphics")]
        [field: SerializeField]
        public SettingsGraphicsScriptableObject Graphics { get; set; }
        
        [field: Header("Audio")]
        [field: SerializeField]
        public SettingsAudioScriptableObject Audio { get; set; }
        
        [field: Header("Controls")]
        [field: SerializeField]
        public SettingsControlsScriptableObject Controls { get; set; }


    }
}