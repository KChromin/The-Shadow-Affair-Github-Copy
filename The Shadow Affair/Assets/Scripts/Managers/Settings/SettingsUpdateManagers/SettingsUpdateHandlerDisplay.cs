using UnityEngine;

namespace SmugRagGames.Managers.Settings
{
    public class SettingsUpdateHandlerDisplay : SettingsUpdateHandlerBase
    {
        public SettingsUpdateHandlerDisplay(ScriptableObject currentSettings, SettingsManagerActions action, short thisSettingsType) : base(currentSettings, action, thisSettingsType)
        {
            _displaySettings = (SettingsDisplayScriptableObject)CurrentSettings;
        }

        private readonly SettingsDisplayScriptableObject _displaySettings;

        #region Presets

        #region Resolutions

        //16:9//
        private static readonly short[] ResolutionPreset16X9Width = { 1280, 1600, 1920, 2048, 2560, 3200, 3840 };
        private static readonly short[] ResolutionPreset16X9Height = { 720, 900, 1080, 1152, 1440, 1800, 2160 };

        //16:10//
        private static readonly short[] ResolutionPreset16X10Width = { 1440, 1680, 1920, 2560, 3840 };
        private static readonly short[] ResolutionPreset16X10Height = { 900, 1050, 1200, 1600, 2400 };

        //21:9//
        private static readonly short[] ResolutionPreset21X9Width = { 2560, 2880, 3440, 3840, 4320, 5120 };
        private static readonly short[] ResolutionPreset21X9Height = { 1080, 1200, 1440, 1600, 1800, 2160 };

        #endregion Resolutions

        #endregion Presets

        #region Settings Apply

        protected override void ApplySettings(short settingsType, bool applyAll)
        {
            base.ApplySettings(settingsType, applyAll);

            SetScreen();
            SetVSyncAndFramerate();
            Application.runInBackground = _displaySettings.runInBackground;
        }

        private void SetScreen()
        {
            //Custom Resolution//
            if (_displaySettings.resolutionCustom)
            {
                SetResolution(_displaySettings.resolutionCustomWidth, _displaySettings.resolutionCustomHeight, _displaySettings.fullscreenMode);
                return;
            }

            //Preset resolutions//
            switch (_displaySettings.aspectRatio)
            {
                case 0:
                    SetResolution(ResolutionPreset16X9Width[_displaySettings.resolutionPreset16X9], ResolutionPreset16X9Height[_displaySettings.resolutionPreset16X9], _displaySettings.fullscreenMode);
                    break;
                case 1:
                    SetResolution(ResolutionPreset16X10Width[_displaySettings.resolutionPreset16X10], ResolutionPreset16X10Height[_displaySettings.resolutionPreset16X10], _displaySettings.fullscreenMode);
                    break;
                case 2:
                    SetResolution(ResolutionPreset21X9Width[_displaySettings.resolutionPreset21X9], ResolutionPreset21X9Height[_displaySettings.resolutionPreset21X9], _displaySettings.fullscreenMode);
                    break;
            }
        }

        private static void SetResolution(short width, short height, short fullscreenMode)
        {
            FullScreenMode newFullScreenMode = FullScreenMode.ExclusiveFullScreen;

            switch (fullscreenMode)
            {
                case 0:
                    newFullScreenMode = FullScreenMode.ExclusiveFullScreen;
                    break;
                case 1:
                    newFullScreenMode = FullScreenMode.MaximizedWindow;
                    break;
                case 2:
                    newFullScreenMode = FullScreenMode.Windowed;
                    break;
            }

            Screen.SetResolution(width, height, newFullScreenMode);
        }

        private void SetVSyncAndFramerate()
        {
            //Custom framerate//
            if (_displaySettings.framerateCustomMax)
            {
                QualitySettings.vSyncCount = 0; //Disable VSync//
                Application.targetFrameRate = _displaySettings.framerateCustomMaxValue;
            }
            else
            {
                //VSync//
                QualitySettings.vSyncCount = _displaySettings.vSyncMode;
                Application.targetFrameRate = -1;
            }
        }

        #endregion Settings Apply
    }
}