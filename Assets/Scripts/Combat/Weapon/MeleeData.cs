using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;

namespace Combat.Weapon
{
    [CreateAssetMenu(fileName = "New Melee Weapon", menuName = "Combat/Melee Weapon", order = 2)]
    public class MeleeData : ScriptableObject
    {
        static public string RESOURCE_FOLDER = "Data/Melee Weapons/";

        public string id;
        public string category;
        public new string name;
        [TextArea(3, 15)]
        public string description;

        public int level;

        [Tooltip("Attack mode [when holding the attack button].")]
        public AttackMode attackMode;

        [Tooltip("Attack hit rate (or speed) in hits per seconds.")]
        public float attacksPerSeconds;
        [Tooltip("Duration of the attack, in seconds.")]
        public float attackDuration = 0.3f;

        [Header("Stats")]
        public AttackModifier attackModifier;
        public DefenseModifier defenseModifier;
        //public BuffChance[] buffChances;

        public enum AttackMode
        {
            Default,    // Default attack mode (hit on attack)
            Charge,     // Charge while holding attack, hit force depends on charge %
            AutoAttack  // Holding the attack repeatedly hits
        }

        static public MeleeData LoadFromResourceName(string name)
        {
            return (MeleeData)Resources.Load(RESOURCE_FOLDER + name);
        }
    }
}