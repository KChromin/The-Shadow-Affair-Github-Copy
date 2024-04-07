using System;

namespace SmugRag.Managers.Settings
{
    public class SettingsManagerActions
    {
        public event Action OnSettingsApplyGame;
        public event Action OnSettingsApplyDisplay;
        public event Action OnSettingsApplyGraphics;
        public event Action OnSettingsApplyAudio;
        public event Action OnSettingsApplyControls;
        
        #region Invoke Mehodes

        public void InvokeOnSettingsApplyGame()
        {
            OnSettingsApplyGame?.Invoke();
        }
        
        public void InvokeOnSettingsApplyDisplay()
        {
            OnSettingsApplyDisplay?.Invoke();
        }
        
        public void InvokeOnSettingsApplyGraphics()
        {
            OnSettingsApplyGraphics?.Invoke();
        }

        
        public void InvokeOnSettingsApplyAudio()
        {
            OnSettingsApplyAudio?.Invoke();
        }
        
        public void InvokeOnSettingsApplyControls()
        {
            OnSettingsApplyControls?.Invoke();
        }

        #endregion Invoke Mehodes
    }
}