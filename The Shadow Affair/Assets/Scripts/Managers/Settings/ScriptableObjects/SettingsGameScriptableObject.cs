using UnityEngine;

namespace SmugRagGames.Managers.Settings
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/Settings/Game")]

    public class SettingsGameScriptableObject : ScriptableObject
    {
        [Header("Language")]
        [Range(0,1)]
        public short language = 0;

        [Header("Head Bobbing")]
        public bool headBobbingDisable = false;
        [Range(0,1f)]
        public float headBobbingIntensity = 1f;
    }
}
