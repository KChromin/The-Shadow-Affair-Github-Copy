using SmugRag.ScriptableObjects.Settings;
using SmugRag.Managers.Settings;
using UnityEngine;

namespace SmugRag.Managers
{
    public class SettingsManagerControlsSettingsChanger : SettingsManagerSettingsChangerBase
    {
        private SettingsControlsScriptableObject _temporarySettings;
        private SettingsManagerControls _controlsManager;

        public override void Setup(SettingsManagerBase settingsManager, ScriptableObject temporarySettings)
        {
            base.Setup(settingsManager, temporarySettings);

            //Cast to proper type//
            _temporarySettings = (SettingsControlsScriptableObject)TemporarySettingsBase;
            _controlsManager = (SettingsManagerControls)SettingsManagerOption;
        }
        
        protected override void OnSettingsFileLoad()
        {
            _controlsManager.EqualizeTemporarySettingsWithCurrent();
        }
        protected override void OnSettingsFileSave()
        {
            _controlsManager.EqualizeTemporarySettingsWithCurrent();
        }

        protected override void SaveTemporarySettings()
        {
            _controlsManager.SaveTemporarySettings();
        }


        #region Setting Change Methodes

        public void Change_LookSensitivityGeneral(float newValue)
        {
            _temporarySettings.lookSensitivityGeneral = newValue;
        }

        public void Change_LookUseSeparateSensitivityAxes(bool newValue)
        {
            _temporarySettings.lookUseSeparateSensitivityAxes = newValue;
        }
        
        public void Change_LookSensitivitySeparateAxisX(float newValue)
        {
            _temporarySettings.lookSensitivitySeparateAxesX = newValue;
        }
        
        public void Change_LookSensitivitySeparateAxisY(float newValue)
        {
            _temporarySettings.lookSensitivitySeparateAxesY = newValue;
        }
        
        public void Change_LookInvertYAxis(bool newValue)
        {
            _temporarySettings.lookInvertYAxis = newValue;
        }

        #endregion Setting Change Methodes
    }
}