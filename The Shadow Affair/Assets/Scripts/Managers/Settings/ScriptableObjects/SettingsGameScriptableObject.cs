using UnityEngine;

namespace SmugRag.ScriptableObjects.Settings
{
   [CreateAssetMenu(fileName = "SettingsGameScriptableObject", menuName = "ScriptableObjects/Settings/GameSettings")]
    public class SettingsGameScriptableObject : ScriptableObject
    {
        [Header("Language")]
        [Space]
        public short language = 0;
        
        [Header("Head Bobbing")]
        [Space]
        public bool headBobbingDisabled = false;
    }
}
