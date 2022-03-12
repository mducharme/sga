using System.Collections.Generic;
using UnityEngine;

namespace Equipment
{
    [CreateAssetMenu(fileName = "Equipment Item", menuName = "Items/Equipment Item", order = 0)]
    public class EquipmentData : ScriptableObject
    {
        static public string RESOURCE_FOLDER = "Data/Equipment/";

        #region Public fields
        public EquipmentCategory type;

        [Tooltip("Equipped attributes modifiers")]
        public Combat.Attributes attributes;

        [Tooltip("Reference to the inventory object of this equipment.")]
        public Inventory.ItemData inventoryData;

        public Combat.Weapon.MeleeData meleeData;
        public Combat.Weapon.RangedData rangedData;
        //public Combat.Weapon.ShieldData shieldData;
        #endregion

        static public EquipmentData LoadFromResourceName(string name)
        {
            return (EquipmentData)Resources.Load(RESOURCE_FOLDER + name);
        }
    }
}