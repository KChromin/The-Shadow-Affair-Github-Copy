using UnityEngine;

namespace SmugRag.ScriptableObjects.Settings
{
    // [CreateAssetMenu(fileName = "SettingsControlsScriptableObject", menuName = "ScriptableObjects/Settings/ControlSettings")]
    public class SettingsControlsScriptableObject : ScriptableObject
    {
        [Header("Look Settings")]
        [Space, Range(0f, 100f)]
        public float lookSensitivityGeneral = 25f;

        public bool lookUseSeparateSensitivityAxes = false;

        [Range(0f, 100f)]
        public float lookSensitivitySeparateAxesX = 25f;

        [Range(0f, 100f)]
        public float lookSensitivitySeparateAxesY = 20f;

        public bool lookInvertYAxis = false;
    }
}