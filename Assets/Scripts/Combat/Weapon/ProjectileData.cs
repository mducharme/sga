using UnityEngine;
using Combat;

namespace Combat.Weapon
{
    [CreateAssetMenu(fileName = "ProjectileData", menuName = "Combat/Projectile", order = 0)]
    public class ProjectileData : ScriptableObject
    {

        static public string RESOURCE_FOLDER = "Data/Projectiles/";
        public FireMode fireMode;
        public new string name;
        public AttackModifier attackModifier;

        public enum FireMode
        {
            Bullet,
            Tracer
        }

        static public ProjectileData LoadFromResourceName(string name)
        {
            return (ProjectileData)Resources.Load(RESOURCE_FOLDER + name);
        }
    }
}