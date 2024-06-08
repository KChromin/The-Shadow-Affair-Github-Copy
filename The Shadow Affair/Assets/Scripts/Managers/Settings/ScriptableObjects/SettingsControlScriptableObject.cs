using UnityEngine;

namespace SmugRagGames.Managers.Settings
{
    [CreateAssetMenu(fileName = "ControlSettings", menuName = "ScriptableObjects/Settings/Control")]
    public class SettingsControlScriptableObject : ScriptableObject
    {
        [Header("Look")]
        [Range(0, 100f)]
        public float lookSensitivityGeneral = 25f;

        [Space]
        public bool lookSeparateSensitivityAxes = false;

        [Range(0, 100f)]
        public float lookSensitivityXAxis = 30f;

        [Range(0, 100f)]
        public float lookSensitivityYAxis = 20f;

        [Space]
        public bool lookInvertYAxis = false;

        [Header("Input")]
        public bool inputCrouchToggle = true;
    }
}