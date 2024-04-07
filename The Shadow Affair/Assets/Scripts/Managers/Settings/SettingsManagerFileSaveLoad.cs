using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

//This class operates creating, and loading json files from settings//
namespace SmugRag.Managers.Settings
{
    public class SettingsManagerFileSaveLoad
    {
        private string _settingsDirectoryPath;

        //Action causes all settings manager scripts to regenerate setting files//
        public event Action OnSettingsFilesRegenerationAction;
        public event Action OnSettingsFileLoadAction;
        public event Action OnSettingsFileSaveAction;

        private ScriptableObject[] _settingsObjects;

        public enum SettingType
        {
            Game,
            Display,
            Graphics,
            Audio,
            Controls
        }

        public void Setup(ScriptableObject gameSettings, ScriptableObject displaySettings, ScriptableObject graphicsSettings, ScriptableObject audioSettings, ScriptableObject controlsSettings)
        {
            //Set all current settings ScriptableObjects//
            _settingsObjects = new[] { gameSettings, displaySettings, graphicsSettings, audioSettings, controlsSettings };
        }

        private static string SettingsName(SettingType settingType)
        {
            string settingsName = "";

            settingsName += settingType switch
            {
                SettingType.Game => "Game",
                SettingType.Display => "Display",
                SettingType.Graphics => "Graphics",
                SettingType.Audio => "Audio",
                SettingType.Controls => "Controls",
                _ => throw new ArgumentOutOfRangeException(nameof(settingType), settingType, null)
            };

            return settingsName;
        }

        private void SetDirectoryPath()
        {
            _settingsDirectoryPath = Application.persistentDataPath + "/" + "Settings";

            //If directory does not exists, create it//
            if (!Directory.Exists(_settingsDirectoryPath))
            {
                Directory.CreateDirectory(_settingsDirectoryPath);

                //Generate new setting files//
                OnSettingsFilesRegenerationAction?.Invoke();
            }
        }

        public void LoadFromFile()
        {
            SetDirectoryPath();

            string finalPath = _settingsDirectoryPath + "/" + "Settings.txt";
            string rawConfigFile = File.ReadAllText(finalPath);

            List<string> loadedJsonSettings = new List<string>();

            //Load Json data//
            foreach (Match match in Regex.Matches(rawConfigFile, @"\{[^}]*\}"))
            {
                loadedJsonSettings.Add(match.Value);
            }

            //When data is incomplete, regenerate//
            if (loadedJsonSettings.Count != _settingsObjects.Length)
            {
                OnSettingsFilesRegenerationAction?.Invoke();
                return;
            }

            //Try to assign new settings//
            try
            {
                for (int i = 0; i < _settingsObjects.Length; i++)
                {
                    JsonUtility.FromJsonOverwrite(loadedJsonSettings[i], _settingsObjects[i]);
                }
            }
            catch (Exception e)
            {
                Debug.LogError("Cannot load settings" + " | Regenerating Settings Files!");

                //Recreate setting files//
                OnSettingsFilesRegenerationAction?.Invoke();

                Console.WriteLine(e);
                throw;
            }

            OnSettingsFileLoadAction?.Invoke();
        }

        public void SaveToFile()
        {
            //Path//
            SetDirectoryPath();
            string finalPath = _settingsDirectoryPath + "/" + "Settings.txt";

            int settingsSaveIteration = 0;
            string settingsInText = "[Settings Configuration]" + "\n\n";

            foreach (ScriptableObject settings in _settingsObjects)
            {
                //Add Title//
                settingsInText += SettingsName((SettingType)settingsSaveIteration) + "\n";

                //Add Settings//
                settingsInText += JsonUtility.ToJson(settings, true) + "\n\n";

                settingsSaveIteration++;
            }

            File.WriteAllText(finalPath, settingsInText);

            OnSettingsFileSaveAction?.Invoke();
        }
    }
}