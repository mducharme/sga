using System.Collections.Generic;
using UnityEngine;

namespace Player.Log
{
    [System.Serializable]
    public class RangedWeaponsLog : Game.ISaveable
    {
        public Dictionary<Combat.Weapon.RangedData, RangedWeaponLog> weaponsLog = new();

        [Tooltip("Number of times any ranged weapons was shot.")]
        public int shots;

        [Tooltip("Number of times any ranged weapon hit an enemy.")]
        public int hits;

        [Tooltip("Number of enemies killed with by ranged weapons.")]
        public int kills;

        [Tooltip("Number of times an attack was critical with a ranged weapon.")]
        public int criticals;

        [Tooltip("Total damage inflicted with ranged weapons.")]
        public int damage;

        public Dictionary<string, int> shotsByCategories;
        public Dictionary<string, int> hitsByCategories;
        public Dictionary<string, int> killsByCategories;
        public Dictionary<string, int> criticalsByCategories;
        public Dictionary<string, int> damageByCategories;

        public void LogShoot(Combat.Weapon.RangedData ranged)
        {
            AddRangedWeapon(ranged);
            
            shots++;
            shotsByCategories[ranged.category]++;
            weaponsLog[ranged].LogShoot();
        }

        private void AddRangedWeapon(Combat.Weapon.RangedData ranged)
        {
            if (weaponsLog.ContainsKey(ranged) == false)
            {
                weaponsLog.Add(ranged, new RangedWeaponLog());
            }
            string category = ranged.category;
            if (shotsByCategories.ContainsKey(category) == false)
            {
                shotsByCategories.Add(category, 0);
            }
            if (hitsByCategories.ContainsKey(category) == false)
            {
                hitsByCategories.Add(category, 0);
            }
            if (killsByCategories.ContainsKey(category) == false)
            {
                killsByCategories.Add(category, 0);
            }
            if (criticalsByCategories.ContainsKey(category) == false)
            {
                criticalsByCategories.Add(category, 0);
            }
            if (damageByCategories.ContainsKey(category) == false)
            {
                damageByCategories.Add(category, 0);
            }
        }

        [System.Serializable]
        public struct SaveData
        {
            public Dictionary<string, RangedWeaponLog.SaveData> weaponsLog;
            public int shots;
            public int hits;
            public int kills;
            public int criticals;
            public int damage;
            public Dictionary<string, int> shotsByCategories;
            public Dictionary<string, int> hitsByCategories;
            public Dictionary<string, int> killsByCategories;
            public Dictionary<string, int> criticalsByCategories;
            public Dictionary<string, int> damageByCategories;
        }

        public object PrepareSaveData()
        {
            SaveData saveData = new();
            saveData.weaponsLog = new();
            foreach (KeyValuePair<Combat.Weapon.RangedData, RangedWeaponLog> kvp in weaponsLog)
            {
                saveData.weaponsLog.Add(kvp.Key.category+"/"+kvp.Key.name, (RangedWeaponLog.SaveData)kvp.Value.PrepareSaveData());
            }
            saveData.shots = shots;
            saveData.hits = hits;
            saveData.kills = kills;
            saveData.criticals = criticals;
            saveData.damage = damage;
            saveData.shotsByCategories = shotsByCategories;
            saveData.hitsByCategories = hitsByCategories;
            saveData.killsByCategories = killsByCategories;
            saveData.criticalsByCategories = criticalsByCategories;
            saveData.damageByCategories = damageByCategories;
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
            shotsByCategories = saveData.shotsByCategories;
            hitsByCategories = saveData.hitsByCategories;
            killsByCategories = saveData.killsByCategories;
            criticalsByCategories = saveData.criticalsByCategories;
            damageByCategories = saveData.damageByCategories;
        }
    }
}