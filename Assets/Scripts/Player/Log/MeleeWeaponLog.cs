using UnityEngine;

namespace Player.Log
{
    [System.Serializable]
    public class MeleeWeaponLog : Game.ISaveable
    {
        [Tooltip("Number of times this weapon was used.")]
        [SerializeField] private int attacks;

        [Tooltip("Number of times this weapon has hit an enemy.")]
        [SerializeField] private int hits;

        [Tooltip("Number of enemies killed with this weapon.")]
        [SerializeField] private int kills;

        [Tooltip("Number of times an attack was critical with this weapon.")]
        [SerializeField] private int criticals;

        [Tooltip("Total damage inflicted with this weapon.")]
        [SerializeField] private int damage;

        public void LogAttack()
        {
            attacks++;
        }

        [System.Serializable]
        public struct SaveData
        {
            public int attacks;
            public int hits;
            public int kills;
            public int criticals;
            public int damage;
        }

        public object PrepareSaveData()
        {
            SaveData saveData = new();
            saveData.attacks = attacks;
            saveData.hits = hits;
            saveData.kills = kills;
            saveData.criticals = criticals;
            saveData.damage = damage;
            return saveData;
        }

        public void RestoreSaveData(object save)
        {
            SaveData saveData = (SaveData)save;
            attacks = saveData.attacks;
            hits = saveData.hits;
            kills = saveData.kills;
            criticals = saveData.criticals;
            damage = saveData.damage;
        }
    }
}