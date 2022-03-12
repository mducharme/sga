using System.Collections.Generic;
using UnityEngine;

namespace Player.Log
{
    [System.Serializable]
    public class RangedWeaponLog : Game.ISaveable
    {
        [Tooltip("Number of times this weapon was shot.")]
        [SerializeField] private int shots;

        [Tooltip("Number of times this weapon has hit an enemy.")]
        [SerializeField] private int hits;

        [Tooltip("Number of enemies killed with this weapon.")]
        [SerializeField] private int kills;
        
        [Tooltip("Number of times an attack was critical with this weapon.")]
        [SerializeField] private int criticals;

        [Tooltip("Total damage inflicted with this weapon.")]
        [SerializeField] private int damage;


        public void LogShoot()
        {
            shots++;
        }

        [System.Serializable]
        public struct SaveData
        {
            public int shots;
            public int hits;
            public int kills;
            public int criticals;
            public int damage;
        }

        public object PrepareSaveData()
        {
            SaveData saveData = new();
            saveData.shots = shots;
            saveData.hits = hits;
            saveData.kills = kills;
            saveData.criticals = criticals;
            saveData.damage = damage;
            return saveData;
        }

        public void RestoreSaveData(object save)
        {
            SaveData saveData = (SaveData)save;
            shots = saveData.shots;
            hits = saveData.hits;
            kills = saveData.kills;
            criticals = saveData.criticals;
            damage = saveData.damage;
        }
    }
}