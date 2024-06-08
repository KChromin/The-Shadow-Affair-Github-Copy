using System;

namespace SmugRagGames.Managers.Settings
{
    public class SettingsManagerActions
    {
        public event Action<short, bool> OnSettingsApply;

        public event Action OnSettingsFileLoadRequest;
        public event Action OnSettingsFileSaveRequest;

        #region Invoke Methodes

        /// <param name="settingsType"> 0 - Game, 1 - Display, 2 - Visual, 3 - Audio, 4 - Controls</param>
        /// <param name="applyAllSettings">Apply all settings instead of chosen one</param>
        public void InvokeOnSettingsApply(short settingsType, bool applyAllSettings = false)
        {
            OnSettingsApply?.Invoke(settingsType, applyAllSettings);
        }

        public void InvokeOnSettingsFileLoadRequest()
        {
            OnSettingsFileLoadRequest?.Invoke();
        }

        public void InvokeOnSettingsFileSaveRequest()
        {
            OnSettingsFileSaveRequest?.Invoke();
        }

        #endregion Invoke Methodes
    }
}