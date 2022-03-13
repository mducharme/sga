using UnityEngine;

namespace Player.Log
{
    [System.Serializable]
    public class EnemyLog : Game.ISaveable
    {
        [SerializeField] private int numHit;
        [SerializeField] private int numKilled;
        [SerializeField] private int totalDamage;

        [SerializeField] private int numHitBy;
        [SerializeField] private int numDeathBy;
        [SerializeField] private int totalDamageReceivedBy;


        public void LogEnemyHit(int damage)
        {
            numHit++;
            totalDamage += damage;
        }

        public void LogHitByEnemy(int damage)
        {
            numHitBy++;
            totalDamageReceivedBy += damage;
        }

        public void LogEnemyKilled()
        {
            numKilled++;
        }

        public void LogKilledByEnemy()
        {
            numDeathBy++;
        }


        [System.Serializable]
        public struct SaveData
        {
            public int numHit;
            public int numKilled;
            public int totalDamage;

            public int numHitBy;
            public int numDeathBy;
            public int totalDamageReceivedBy;
        }

        public object PrepareSaveData()
        {
            SaveData saveData = new();
            saveData.numHit = numHit;
            saveData.numKilled = numKilled;
            saveData.totalDamage = totalDamage;
            saveData.numHitBy = numHitBy;
            saveData.numDeathBy = numDeathBy;
            saveData.totalDamageReceivedBy = totalDamageReceivedBy;
            return saveData;
        }

        public void RestoreSaveData(object save)
        {
            SaveData saveData = (SaveData)save;
            numHit = saveData.numHit;
            numKilled = saveData.numKilled;
            totalDamage = saveData.totalDamage;
            numHitBy = saveData.numHitBy;
            numDeathBy = saveData.numDeathBy;
            totalDamageReceivedBy = saveData.totalDamageReceivedBy;
        }
    }
}