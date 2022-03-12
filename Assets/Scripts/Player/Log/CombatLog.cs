using System.Collections.Generic;
using UnityEngine;

namespace Player.Log
{
    [System.Serializable]
    public class CombatLog
    {
        [SerializeField] private int numDeath;
        [SerializeField] private int numEnemiesKilled;

        [SerializeField] private int numHits;
        [SerializeField] private int numCriticalHits;
        [SerializeField] private int numHitsReceived;
        [SerializeField] private int numCriticalHitsReceived;

        [SerializeField] private int totalDamage;
        [SerializeField] private Dictionary<Combat.DamageCategory, int> damageByTypes = new();
        [SerializeField] private int totalDamageReceived;
        [SerializeField] private Dictionary<Combat.DamageCategory, int> damageReceivedByTypes = new();
        
        public int NumDeath { get => numDeath; private set { } }
        public int NumEnemiesKilled { get => numEnemiesKilled; private set { } }
        public int NumHits { get => numHits; private set { } }
        public int NumCriticalHits { get => numCriticalHits; private set { } }
        public int NumHitsReceived { get => numHitsReceived; private set { } }
        public int TotalDamage { get => totalDamage; private set { } }
        public int TotalDamageReceived { get => totalDamageReceived; private set { } }
        
        public void LogEnemyKill()
        {
            numEnemiesKilled++;
        }

        public void LogHit(Combat.Result result)
        {
            int dmg = result.GetTotalDamage();
            totalDamage += dmg;
            foreach (Combat.Damage d in result.damages)
            {
                if (damageByTypes.ContainsKey(d.type) == false)
                {
                    damageByTypes.Add(d.type, 0);
                }
                damageByTypes[d.type] += dmg;
            }
            numHits++;
            if (result.isCritical)
            {
                numCriticalHits++;
            }
        }

        public void LogHitReceived(Combat.Result result)
        {
            int dmg = result.GetTotalDamage();
            totalDamageReceived += dmg;
            foreach (Combat.Damage d in result.damages)
            {
                if (damageReceivedByTypes.ContainsKey(d.type) == false)
                {
                    damageReceivedByTypes.Add(d.type, 0);
                }
                damageReceivedByTypes[d.type] += dmg;
            }
            numHitsReceived++;
            if (result.isCritical)
            {
                numCriticalHitsReceived++;
            }
        }

        public void LogDeath()
        {
            numDeath++;
        }

        [System.Serializable]
        public struct SaveData
        {
            public int numDeath;
            public int numEnemiesKilled;

            public int numHits;
            public int numCriticalHits;
            public int numHitsReceived;
            public int numCriticalHitsReceived;

            public int totalDamage;
            public Dictionary<Combat.DamageCategory, int> damageByTypes;
            public int totalDamageReceived;
            public Dictionary<Combat.DamageCategory, int> damageReceivedByTypes;
        }

        public object PrepareSaveData()
        {
            SaveData saveData = new();
            saveData.numDeath = numDeath;
            saveData.numEnemiesKilled = numEnemiesKilled;
            saveData.numHits = numHits;
            saveData.numCriticalHits = numCriticalHits;
            saveData.numHitsReceived = numHitsReceived;
            saveData.numCriticalHitsReceived = numCriticalHitsReceived;
            saveData.totalDamage = totalDamage;
            saveData.damageByTypes = damageByTypes;
            saveData.totalDamageReceived = totalDamageReceived;
            saveData.damageReceivedByTypes = damageReceivedByTypes;
            return saveData;
        }

        public void RestoreSaveData(object save)
        {
            SaveData saveData = (SaveData)save;
            numDeath = saveData.numDeath;
            numEnemiesKilled = saveData.numEnemiesKilled;
            numHits = saveData.numHits;
            numCriticalHits = saveData.numCriticalHits;
            numHitsReceived = saveData.numHitsReceived;
            numCriticalHitsReceived = saveData.numCriticalHitsReceived;
            totalDamage = saveData.totalDamage;
            damageByTypes = saveData.damageByTypes;
            totalDamageReceived = saveData.totalDamageReceived;
            damageReceivedByTypes = saveData.damageReceivedByTypes;
        }
    }
}