using UnityEngine;

namespace SmugRagGames.Managers.Settings
{
    public abstract class SettingsUpdateHandlerBase
    {
        protected SettingsUpdateHandlerBase(ScriptableObject currentSettings, SettingsManagerActions action, short thisSettingsType)
        {
            CurrentSettings = currentSettings;
            _actions = action;
            _thisSettingsType = thisSettingsType;
            SubscribeToEvents();
        }
        
        protected ScriptableObject CurrentSettings;
        private SettingsManagerActions _actions;

        private short _thisSettingsType;

        protected virtual void ApplySettings(short settingsType, bool applyAll)
        {
            //Check if apply should affect this updater//
            if (!applyAll)
            {
                if (_thisSettingsType != settingsType) return;
            }
        }

        private void SubscribeToEvents()
        {
            _actions.OnSettingsApply += ApplySettings;
        }

        public void UnsubscribeFromEvents()
        {
            _actions.OnSettingsApply -= ApplySettings;
        }
    }
}