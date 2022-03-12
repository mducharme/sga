using UnityEngine;

namespace Inventory
{
    [CreateAssetMenu(fileName = "Inventory Item", menuName = "Items/Inventory Item", order = 0)]
    public class ItemData : ScriptableObject
    {
        static public string RESOURCE_FOLDER = "Data/Inventory/";

        public InventoryType type;
        public new string name;
        public int quality;
        public string description;
        public int weight = 0;

        public Equipment.EquipmentData equipmentData;

        static public ItemData LoadFromResourceName(string name)
        {
            return (ItemData)Resources.Load(RESOURCE_FOLDER + name);
        }
    }
}