using System;
using SmugRag.ScriptableObjects.Settings;
using UnityEngine;

namespace SmugRag.Managers.Settings
{
    [Serializable]
    public class SettingsManagerData
    {
        [field: Header("Controls")]
        [field: SerializeField]
        public SettingsControlsScriptableObject Controls { get; set; }
    }
}