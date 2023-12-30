using UnityEngine;

namespace SmugRag.ScriptableObjects.Settings
{
    // [CreateAssetMenu(fileName = "SettingsControlsScriptableObject", menuName = "ScriptableObjects/Settings/ControlSettings")]
    public class SettingsControlsScriptableObject : ScriptableObject
    {
        [Header("Look Settings")]
        [Space, SerializeField, Range(0f, 100f)]
        public float lookSensitivityGeneral = 25f;

        [SerializeField]
        public bool lookUseSeparateSensitivityAxes = false;

        [SerializeField, Range(0f, 100f)]
        public float lookSensitivitySeparateAxesX = 25f;

        [SerializeField, Range(0f, 100f)]
        public float lookSensitivitySeparateAxesY = 20f;

        [SerializeField]
        public bool lookInvertYAxis = false;
    }
}