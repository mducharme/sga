using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat.Weapon
{
    [CreateAssetMenu(fileName = "New Ranged Weapon", menuName = "Combat/Ranged Weapon")]
    public class RangedData : ScriptableObject
    {
        static public string RESOURCE_FOLDER = "Data/Ranged Weapons/";

        public string id;
        public string category;
        public new string name;
        [TextArea(3, 15)]
        public string description;

        public int level;

        public ProjectileData projectile; // @todo Support multiple.

        public ShootingMode shootingMode = ShootingMode.Single;
        public EmitterMode emitterMode = EmitterMode.Alternating;

        public float shootForce = 25f;
        public float recoilForce = 0f;
        public float firePerSeconds = 5f;
        public float fireLifetime = 2f;
        public float range = 50f;

        public AttackModifier attackModifier;

        public enum ShootingMode
        {
            Single,
            Burst,
            AutoFire,
            Charge
        };

        public enum EmitterMode
        {
            Simultaneous,
            Alternating
        };

        static public RangedData LoadFromResourceName(string name)
        {
            return (RangedData)Resources.Load(RESOURCE_FOLDER + name);
        }
    }
}