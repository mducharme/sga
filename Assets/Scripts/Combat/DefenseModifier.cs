using System;
using System.Collections.Generic;

namespace Combat
{
    [System.Serializable]
    public class DefenseModifier : Game.ISaveable
    {
        public CombatType type;

        public List<Damage> protections = new();

        public float criticalResist;
        public float knockbackResist;
        public float disableResist;

        public DefenseModifier(CombatType type = CombatType.Base)
        {
            this.type = type;
        }

        [System.Serializable]
        public struct SaveData
        {
            public string type;
            public List<Damage.SaveData> protections;

            public float criticalResist;
            public float knockbackResist;
            public float disableResist;
        }

        public object PrepareSaveData()
        {
            SaveData saveData = new();
            saveData.type = type.ToString();
            saveData.protections = new();
            protections.ForEach(f => saveData.protections.Add((Damage.SaveData)f.PrepareSaveData()));
            saveData.criticalResist = criticalResist;
            saveData.knockbackResist = knockbackResist;
            saveData.disableResist = disableResist;
            return saveData;
        }

        public void RestoreSaveData(object save)
        {
            SaveData saveData = (SaveData)save;
            type = (CombatType)Enum.Parse(typeof(CombatType), saveData.type);
            saveData.protections.ForEach(d => protections.Add(new Damage(DamageCategory.LoadFromResourceName(d.type), d.amount)));
            criticalResist = saveData.criticalResist;
            knockbackResist = saveData.knockbackResist;
            disableResist = saveData.disableResist;
        }
    }
}