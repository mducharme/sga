using UnityEngine;

namespace Equipment
{
    [CreateAssetMenu(fileName = "EquipmentCategory", menuName = "Items/Equipment Type", order = 0)]
    public class EquipmentCategory : ScriptableObject
    {
        public new string name;

        [Tooltip("If true, will set melee weapon on player when equipping.")]
        public bool isMelee;

        [Tooltip("If true, will set ranged weapon on player when equipping.")]
        public bool isRanged;

        [Tooltip("If true, will set shield on player when equipping.")]
        public bool isShield;

        static public EquipmentCategory LoadFromResourceName(string name)
        {
            return (EquipmentCategory)Resources.Load("Scriptable Objects/Equipment Categories/" + name);
        }
    }
}