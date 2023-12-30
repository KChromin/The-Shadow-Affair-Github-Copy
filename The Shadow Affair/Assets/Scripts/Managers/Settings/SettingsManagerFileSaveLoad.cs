using System;
using System.IO;
using UnityEngine;

//This class operates creating, and loading json files from settings//
namespace SmugRag.Managers.Settings
{
    public class SettingsManagerFileSaveLoad
    {
        private string _settingsDirectoryPath;
        
        //Action causes all settings manager scripts to regenerate setting files//
        public Action SettingsFilesRegenerationAction;

        public enum SettingType
        {
            Controls,
            Video,
            Graphics,
            Audio
        }

        private static string SettingFileEnding(SettingType settingType)
        {
            string pathEnding = "/";

            pathEnding += settingType switch
            {
                SettingType.Controls => "Controls",
                SettingType.Video => "Video",
                SettingType.Graphics => "Graphics",
                SettingType.Audio => "Audio",
                _ => throw new ArgumentOutOfRangeException(nameof(settingType), settingType, null)
            };

            pathEnding += ".json";

            return pathEnding;
        }

        private void SetDirectoryPath()
        {
            _settingsDirectoryPath = Application.persistentDataPath + "/" + "Settings";

            //If directory does not exists, create it//
            if (!Directory.Exists(_settingsDirectoryPath))
            {
                Directory.CreateDirectory(_settingsDirectoryPath);

                //Generate new setting files//
                SettingsFilesRegenerationAction?.Invoke();
            }
        }

        public void SaveToJson(ScriptableObject scriptableObject, SettingType settingType)
        {
            SetDirectoryPath();

            string finalPath = _settingsDirectoryPath + SettingFileEnding(settingType);

            string jsonData = JsonUtility.ToJson(scriptableObject, true);

            File.WriteAllText(finalPath, jsonData);
        }

        public void LoadFromJson(ScriptableObject scriptableObject, SettingType settingType)
        {
            SetDirectoryPath();

            string finalPath = _settingsDirectoryPath + SettingFileEnding(settingType);

            //If cannot assign all values (f.e file is broken) or load, regenerate all files//
            try
            {
                string jsonData = File.ReadAllText(finalPath);
                JsonUtility.FromJsonOverwrite(jsonData, scriptableObject);
            }
            catch (Exception e)
            {
                Debug.LogError("Cannot load settings - " + settingType + " | Regenerating Settings Files!");

                //Recreate setting files//
                SettingsFilesRegenerationAction?.Invoke();
                
                Console.WriteLine(e);
                throw;
            }
        }
    }
}