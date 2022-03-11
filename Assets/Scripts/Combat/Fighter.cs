using UnityEngine;

namespace Combat
{
    public class Fighter : MonoBehaviour, Game.ISaveable
    {
        [SerializeField] private Health health;

        public Health Health { get => health; private set { } }

        [System.Serializable]
        public struct SaveData
        {
            public Health.SaveData health;
        }

        public object PrepareSaveData()
        {
            SaveData saveData = new();
            saveData.health = (Health.SaveData)health.PrepareSaveData();
            return saveData;
        }

        public void RestoreSaveData(object save)
        {
            SaveData saveData = (SaveData)save;
            health.RestoreSaveData(saveData.health);
        }
    }
}