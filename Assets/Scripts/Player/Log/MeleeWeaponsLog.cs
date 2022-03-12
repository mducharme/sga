using System.Collections.Generic;
using UnityEngine;

namespace Player.Log
{
    [System.Serializable]
    public class MeleeWeaponsLog : Game.ISaveable
    {
        [SerializeField] private readonly Dictionary<Combat.Weapon.MeleeData, MeleeWeaponLog> weaponsLog = new();

        [Tooltip("Number of times any melee weapon was used.")]
        [SerializeField] private int attacks;

        [Tooltip("Number of times any melee weapon has hit an enemy.")]
        [SerializeField] private int hits;

        [Tooltip("Number of enemies killed with a melee weapon.")]
        [SerializeField] private int kills;

        [Tooltip("Number of times an attack was critical with a melee weapon.")]
        [SerializeField] private int criticals;

        [Tooltip("Total damage inflicted with melee weapons.")]
        [SerializeField] private int damage;

        [SerializeField] private Dictionary<string, int> attacksByCategories = new();
        [SerializeField] private Dictionary<string, int> hitsByCategories = new();
        [SerializeField] private Dictionary<string, int> killsByCategories = new();
        [SerializeField] private Dictionary<string, int> criticalsByCategories = new();
        [SerializeField] private Dictionary<string, int> damageByCategories = new();


        #region Public API
        public void LogAttack(Combat.Weapon.MeleeData melee)
        {
            AddMeleeWeapon(melee);

            attacks++;
            attacksByCategories[melee.category]++;
            weaponsLog[melee].LogAttack();
        }
        #endregion

        private void AddMeleeWeapon(Combat.Weapon.MeleeData melee)
        {
            if (weaponsLog.ContainsKey(melee) == false)
            {
                weaponsLog.Add(melee, new MeleeWeaponLog());
            }

            string category = melee.category;
            if (attacksByCategories.ContainsKey(category) == false)
            {
                attacksByCategories.Add(category, 0);
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
            public Dictionary<string, MeleeWeaponLog.SaveData> weaponsLog;
            public int attacks;
            public int hits;
            public int kills;
            public int criticals;
            public int damage;
            public Dictionary<string, int> attacksByCategories;
            public Dictionary<string, int> hitsByCategories;
            public Dictionary<string, int> killsByCategories;
            public Dictionary<string, int> criticalsByCategories;
            public Dictionary<string, int> damageByCategories;
        }

        public object PrepareSaveData()
        {
            SaveData saveData = new();
            saveData.weaponsLog = new();
            foreach (KeyValuePair<Combat.Weapon.MeleeData, MeleeWeaponLog> kvp in weaponsLog)
            {
                saveData.weaponsLog.Add(kvp.Key.category + "/" + kvp.Key.name, (MeleeWeaponLog.SaveData)kvp.Value.PrepareSaveData());
            }
            saveData.attacks = attacks;
            saveData.hits = hits;
            saveData.kills = kills;
            saveData.criticals = criticals;
            saveData.damage = damage;
            saveData.attacksByCategories = attacksByCategories;
            saveData.hitsByCategories = hitsByCategories;
            saveData.killsByCategories = killsByCategories;
            saveData.criticalsByCategories = criticalsByCategories;
            saveData.damageByCategories = damageByCategories;
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
            attacksByCategories = saveData.attacksByCategories;
            hitsByCategories = saveData.hitsByCategories;
            killsByCategories = saveData.killsByCategories;
            criticalsByCategories = saveData.criticalsByCategories;
            damageByCategories = saveData.damageByCategories;
        }
    }
}