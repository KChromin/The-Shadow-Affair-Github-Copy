using UnityEngine;
using SmugRag.Templates.Singletons;

namespace SmugRag.Managers.Settings
{
    public class SettingsManager : SingletonPersistentManager<SettingsManager>
    {
        #region Settings Scriptable Objects

        [field: Header("Current Settings")]
        [field: SerializeField]
        public SettingsManagerData CurrentSettings { get; private set; }

        [Header("Default Settings")]
        [SerializeField]
        private SettingsManagerData defaultSettings;

        [Header("Temporary Settings")]
        [SerializeField]
        private SettingsManagerData temporarySettings;

        #endregion Settings Scriptable Objects

        private SettingsManagerFileSaveLoad _fileSaveLoadManager;
        private SettingsManagerControls _controlsManager;
        public SettingsManagerControlsSettingsChanger ControlsManagerSettingsChanger { get; private set; }

        #region Setup

        protected override void Awake()
        {
            base.Awake();

            CreateAndSetupManagers();
        }

        private void CreateAndSetupManagers()
        {
            //File save and load//
            _fileSaveLoadManager = new SettingsManagerFileSaveLoad();

            //Controls//
            _controlsManager = new SettingsManagerControls();
            ControlsManagerSettingsChanger = new SettingsManagerControlsSettingsChanger();
            _controlsManager.Setup(_fileSaveLoadManager, SettingsManagerFileSaveLoad.SettingType.Controls, CurrentSettings.Controls, defaultSettings.Controls, temporarySettings.Controls);
            ControlsManagerSettingsChanger.Setup(_controlsManager, temporarySettings.Controls);
        }

        #endregion Setup

        #region OnDisable

        private void OnDisable()
        {
            UnsubscribeManagersActions();
        }

        private void UnsubscribeManagersActions()
        {
            _controlsManager.UnsubscribeFromEvents();
            ControlsManagerSettingsChanger.UnsubscribeFromEvents();
        }

        #endregion OnDisable
    }
}