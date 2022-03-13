using System.Collections.Generic;
using UnityEngine;

namespace Player.Log
{
    [System.Serializable]
    public class EnemiesLog
    {
        [SerializeField] private readonly Dictionary<Enemy.EnemyData, EnemyLog> enemiesLog = new();

        [SerializeField] private int numEnemiesDiscovered;

        public int NumEnemiesDiscovered { get => numEnemiesDiscovered; private set { } }

        public void LogEnemyHit(Enemy.EnemyData enemy, int damage)
        {
            AddEnemy(enemy);
            enemiesLog[enemy].LogEnemyHit(damage);
        }

        public void LogHitByEnemy(Enemy.EnemyData enemy, int damage)
        {
            AddEnemy(enemy);
            enemiesLog[enemy].LogHitByEnemy(damage);
        }

        public void LogEnemyKilled(Enemy.EnemyData enemy)
        {
            AddEnemy(enemy);
            enemiesLog[enemy].LogEnemyKilled();
        }

        public void LogKilledByEnemy(Enemy.EnemyData enemy)
        {
            AddEnemy(enemy);
            enemiesLog[enemy].LogKilledByEnemy();
        }


        private void AddEnemy(Enemy.EnemyData enemy)
        {
            if (enemiesLog.ContainsKey(enemy) == false)
            {
                enemiesLog.Add(enemy, new EnemyLog());
                numEnemiesDiscovered++;
            }
        }

        [System.Serializable]
        public struct SaveData
        {
            public Dictionary<string, EnemyLog.SaveData> enemiesLog;

            public int numEnemiesDiscovered;
        }

        public object PrepareSaveData()
        {
            SaveData saveData = new();
            saveData.enemiesLog = new();
            foreach (KeyValuePair<Enemy.EnemyData, EnemyLog> log in enemiesLog)
            {
                saveData.enemiesLog.Add(log.Key.name, (EnemyLog.SaveData)log.Value.PrepareSaveData());
            }
            saveData.numEnemiesDiscovered = numEnemiesDiscovered;
            return saveData;
        }

        public void RestoreSaveData(object save)
        {
            SaveData saveData = (SaveData)save;
            numEnemiesDiscovered = saveData.numEnemiesDiscovered;
            // @todo Get enemies log
        }
    }
}