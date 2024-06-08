using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

namespace SmugRagGames.Managers.Settings
{
    public class SettingsManagerFileHandler
    {
        public SettingsManagerFileHandler(SettingsManagerData currentSettings, SettingsManagerActions actions)
        {
            _settingsObjects = new ScriptableObject[] { currentSettings.Game, currentSettings.Display, currentSettings.Visual, currentSettings.Audio, currentSettings.Control };
            _actions = actions;
            SubscribeToEvents();
        }

        private string _directoryPath;

        private readonly ScriptableObject[] _settingsObjects;

        private readonly SettingsManagerActions _actions;

        private void SetDirectoryPath()
        {
            _directoryPath = Application.persistentDataPath + "/" + "Settings";

            //Create directory if not existing already//
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }
        }

        private static string SettingsHeaderName(short settingType)
        {
            string settingsHeader = "";

            switch (settingType)
            {
                case 0:
                    settingsHeader += "[Game]";
                    break;
                case 1:
                    settingsHeader += "[Display]";
                    break;
                case 2:
                    settingsHeader += "[Visual]";
                    break;
                case 3:
                    settingsHeader += "[Audio]";
                    break;
                case 4:
                    settingsHeader += "[Controls]";
                    break;
            }

            return settingsHeader;
        }

        private void SaveSettingsFile()
        {
            SetDirectoryPath();

            string finalPath = _directoryPath + "/" + "Settings.txt";

            string settingsDataInText = "[Settings Configuration]" + "\n\n";

            for (short i = 0; i < _settingsObjects.Length; i++)
            {
                //Add Header//
                settingsDataInText += SettingsHeaderName(i) + "\n";

                //Add Data//
                settingsDataInText += JsonUtility.ToJson(_settingsObjects[i], true) + "\n\n";
            }

            File.WriteAllText(finalPath, settingsDataInText);
        }

        private void LoadSettingsFile()
        {
            SetDirectoryPath();

            string finalPath = _directoryPath + "/" + "Settings.txt";
            string rawSettingsDataFile;

            #region If can load file

            try
            {
                rawSettingsDataFile = File.ReadAllText(finalPath);
            }
            catch (Exception e)
            {
                Debug.LogError("Directory, or file doesn't exist! " + " | Regenerating Settings Files!");

                //Request regeneration//
                _actions.InvokeOnSettingsFileSaveRequest();

                Console.WriteLine(e);
                throw;
            }

            #endregion If can load file

            //Get Json data//
            List<string> settingsDataInJson = new List<string>();
            foreach (Match match in Regex.Matches(rawSettingsDataFile, @"\{[^}]*\}"))
            {
                settingsDataInJson.Add(match.Value);
            }

            //When data is in complete, request settings file regeneration//
            if (settingsDataInJson.Count != _settingsObjects.Length)
            {
                _actions.InvokeOnSettingsFileSaveRequest();
                return;
            }

            #region If can update scriptable objects

            try
            {
                for (int i = 0; i < _settingsObjects.Length; i++)
                {
                    JsonUtility.FromJsonOverwrite(settingsDataInJson[i], _settingsObjects[i]);
                }
            }
            catch (Exception e)
            {
                Debug.LogError("Cannot load settings" + " | Regenerating Settings Files!");

                //Request regeneration//
                _actions.InvokeOnSettingsFileSaveRequest();

                Console.WriteLine(e);
                throw;
            }

            #endregion If can update scriptable objects
        }

        #region Event Subscriptions

        private void SubscribeToEvents()
        {
            _actions.OnSettingsFileSaveRequest += SaveSettingsFile;
            _actions.OnSettingsFileLoadRequest += LoadSettingsFile;
        }

        public void UnsubscribeFromEvents()
        {
            _actions.OnSettingsFileSaveRequest -= SaveSettingsFile;
            _actions.OnSettingsFileLoadRequest -= LoadSettingsFile;
        }

        #endregion Event Subscriptions
    }
}