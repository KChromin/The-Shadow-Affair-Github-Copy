using UnityEngine;

namespace SmugRagGames.Managers.Settings
{
    [CreateAssetMenu(fileName = "VisualSettings", menuName = "ScriptableObjects/Settings/Visual")]

    public class SettingsVisualScriptableObject : ScriptableObject
    {
        [Header("Test")]
        public bool test;
    }
}
