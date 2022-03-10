using System.IO;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class SaveManager : MonoBehaviour, ISaveable
    {
        public const string SAVEFILE_FOLDER = "/save/";
        public const string SAVEFILE_EXT = "gamesave";
        public const string QUICKSAVE_NAME = "Quicksave";


        private string saveDir;

        private void Awake()
        {
            saveDir = Application.persistentDataPath + SAVEFILE_FOLDER;
        }

        public List<FileInfo> GetSavedGames()
        {
            DirectoryInfo info = new(saveDir);
            info.Create(); // Ensure dir exists
            return info.GetFiles().OrderByDescending(f => f.LastWriteTime).ToList<FileInfo>();
        }

        public void QuickSave()
        {
            SaveTo(QUICKSAVE_NAME);
        }

        public void SaveTo(string gameId)
        {
            string json = JsonUtility.ToJson(PrepareSaveData(), true);

            string fileName = saveDir + gameId + "." + SAVEFILE_EXT;
            FileInfo file = new(fileName);
            file.Directory.Create(); // Ensure dir exists
            File.WriteAllText(fileName, json);
        }

        public void LoadLatest()
        {
            string latestSave = GetSavedGames().First<FileInfo>().Name;
            LoadGame(latestSave);
        }

        public void LoadGame(string saveId)
        {
            string fileName = saveDir + saveId;
            string json = File.ReadAllText(fileName);
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);
            RestoreSaveData(saveData);
        }

        [System.Serializable]
        public struct SaveData
        {
            public string scene;
            public Player.PlayerController.SaveData player;
        }

        public object PrepareSaveData()
        {
            SaveData data = new();
            data.scene = SceneManager.GetActiveScene().name;
            data.player = (Player.PlayerController.SaveData)Player.PlayerController.instance.PrepareSaveData();
            return data;
        }

        public void RestoreSaveData(object data)
        {
            SaveData saveData = (SaveData)data;
            if (SceneManager.GetActiveScene().name != saveData.scene)
            {
                SceneManager.LoadScene(saveData.scene);
            }
            Player.PlayerController.instance.RestoreSaveData(saveData.player);
        }

    }
}