using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    [System.Serializable]
    public class Attributes : Game.ISaveable
    {
        [Header("Movement")]
        public float moveSpeed = 0f;
        public float jumpForce = 0f;
        public int numJumps = 0;
        public float dashForce = 0f;

        [Header("Combat")]
        public int maxHealth;
        public List<AttackModifier> attacks = new();
        public List<DefenseModifier> defenses = new();
        public float meleeSpeed = 0f;
        public float rangedSpeed = 0f;

        [Header("Character")]
        [Tooltip("General luck. Influences item drop chances.")]
        public float luck;

        public void Add(Attributes attributes)
        {
            if (attributes == null)
            {
                return;
            }
            moveSpeed += attributes.moveSpeed;
            jumpForce += attributes.jumpForce;
            numJumps += attributes.numJumps;
            dashForce += attributes.dashForce;
            maxHealth += attributes.maxHealth;
            attributes.attacks.ForEach(att => attacks.Add(att));
            attributes.defenses.ForEach(def => defenses.Add(def));
            meleeSpeed += attributes.meleeSpeed;
            rangedSpeed += attributes.rangedSpeed;
            luck += attributes.luck;
        }

        public void Remove(Attributes attributes)
        {
            if (attributes == null)
            {
                return;
            }
            moveSpeed -= attributes.moveSpeed;
            jumpForce -= attributes.jumpForce;
            numJumps -= attributes.numJumps;
            dashForce -= attributes.dashForce;
            maxHealth -= attributes.maxHealth;
            attributes.attacks.ForEach(att => attacks.Remove(att));
            attributes.defenses.ForEach(def => defenses.Remove(def));
            meleeSpeed -= attributes.meleeSpeed;
            rangedSpeed -= attributes.rangedSpeed;
            luck -= attributes.luck;
        }

        [System.Serializable]
        public struct SaveData
        {
            public float moveSpeed;
            public float jumpForce;
            public int numJumps;
            public float dashForce;

            public int maxHealth;
            public List<AttackModifier.SaveData> attacks;
            public List<DefenseModifier.SaveData> defenses;
            public float meleeSpeed;
            public float rangedSpeed;

            public float luck;
        }

        public object PrepareSaveData()
        {
            SaveData saveData = new();
            saveData.moveSpeed = moveSpeed;
            saveData.jumpForce = jumpForce;
            saveData.numJumps = numJumps;
            saveData.dashForce = dashForce;
            saveData.maxHealth = maxHealth;
            saveData.attacks = new();
            saveData.defenses = new();
            attacks.ForEach(f => saveData.attacks.Add((AttackModifier.SaveData)f.PrepareSaveData()));
            defenses.ForEach(f => saveData.defenses.Add((DefenseModifier.SaveData)f.PrepareSaveData()));
            saveData.meleeSpeed = meleeSpeed;
            saveData.rangedSpeed = rangedSpeed;
            saveData.luck = luck;
            return saveData;
        }

        public void RestoreSaveData(object save)
        {
            SaveData saveData = (SaveData)save;
            moveSpeed = saveData.moveSpeed;
            jumpForce = saveData.jumpForce;
            numJumps = saveData.numJumps;
            dashForce = saveData.dashForce;
            maxHealth = saveData.maxHealth;
            foreach (AttackModifier.SaveData attack in saveData.attacks)
            {
                AttackModifier attackModifier = new();
                attackModifier.RestoreSaveData(attack);
                attacks.Add(attackModifier);
            }
            foreach (DefenseModifier.SaveData defense in saveData.defenses)
            {
                DefenseModifier defenseModifier = new();
                defenseModifier.RestoreSaveData(defense);
                defenses.Add(defenseModifier);
            }
            meleeSpeed = saveData.meleeSpeed;
            rangedSpeed = saveData.rangedSpeed;
            luck = saveData.luck;
        }
    }
}