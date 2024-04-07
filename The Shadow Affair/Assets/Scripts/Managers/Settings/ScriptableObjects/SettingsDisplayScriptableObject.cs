using UnityEngine;

namespace SmugRag.ScriptableObjects.Settings
{
    [CreateAssetMenu(fileName = "SettingsDisplayScriptableObject", menuName = "ScriptableObjects/Settings/DisplaySettings")]
    public class SettingsDisplayScriptableObject : ScriptableObject
    {
        [Header("Resolution Settings")]
        [Space]
        public short resolutionAspectRatioMode = 0; //0 - 16:9, 1 - 16:10//
        public short resolutionPreset = 1;
        [Space]
        public bool resolutionUseCustom = false;
        public int resolutionCustomHeight = 1080;
        public int resolutionCustomWidth = 1920;

        [Header("VSync")]
        [Space]
        public short vSyncMode = 0;
        [Space]
        public bool framerateCapCustom = false;
        [Range(1, 1000)]
        public short framerateCapValue = 60;
    }

    public class ResolutionData
    {
        public string Name;
        public int Height;
        public int Width;
    }
}