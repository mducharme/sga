using System;
using System.Collections.Generic;

namespace Combat
{
    [System.Serializable]
    public class AttackModifier : Game.ISaveable
    {
        public CombatType type;
        public List<Damage> damages = new();

        public float criticalChance;
        public float knockbackForce;
        public float disableDuration;

        public AttackModifier(CombatType type = CombatType.Base)
        {
            this.type = type;
        }

        [System.Serializable]
        public struct SaveData
        {
            public string type;
            public List<Damage.SaveData> damages;

            public float criticalChance;
            public float knockbackForce;
            public float disableDuration;
        }

        public object PrepareSaveData()
        {
            SaveData saveData = new();
            saveData.type = type.ToString();
            saveData.damages = new();
            damages.ForEach(d => saveData.damages.Add((Damage.SaveData)d.PrepareSaveData()));
            saveData.criticalChance = criticalChance;
            saveData.knockbackForce = knockbackForce;
            saveData.disableDuration = disableDuration;
            return saveData;
        }

        public void RestoreSaveData(object save)
        {
            SaveData saveData = (SaveData)save;
            type = (CombatType)Enum.Parse(typeof(CombatType), saveData.type);
            saveData.damages.ForEach(d => damages.Add(new Damage(DamageCategory.LoadFromResourceName(d.type), d.amount)));
            criticalChance = saveData.criticalChance;
            knockbackForce = saveData.knockbackForce;
            disableDuration = saveData.disableDuration;
        }
    }
}