using System.Collections.Generic;
using UnityEngine;

namespace Equipment
{
    /**
     * Equipment manager
     */
    [System.Serializable]
    public class EquipmentManager: MonoBehaviour, Game.ISaveable
    {
        [SerializeField] private List<Slot> availableSlots = new();

        [SerializeField] private Dictionary<Slot, EquipmentData> items = new();


        public List<Slot> Slots { get => availableSlots; private set { } }
        public Dictionary<Slot, EquipmentData> Items { get => items; private set { } }

        public delegate void OnEquip(EquipmentData item);
        public delegate void OnUnequip(EquipmentData item);
        public OnEquip onEquip;
        public OnUnequip onUnequip;


        public void AddItem(EquipmentData item)
        {
            EquipmentCategory type = item.type;
            if (!HasSlotOfType(type))
            {
                Debug.LogWarning("Equipment does not have any slots of this item type.");
                return;
            }

            onEquip?.Invoke(item);

            if (HasEmptySlotOfType(type))
            {
                // Insert in first empty slot
                List<Slot> slotsOfType = availableSlots.FindAll(s => s.type == type);
                foreach (Slot slot in slotsOfType)
                {
                    if (!items.ContainsKey(slot))
                    {
                        items.Add(slot, item);
                        break;
                    }
                }
            } else
            {
                // Overrides last slot
                Slot lastSlot = availableSlots.FindLast(s => s.type == type);
                RemoveItem(items[lastSlot]);
                items[lastSlot] = item;
            }
        }

        public void AddItem(EquipmentData item, Slot slot)
        {
            if (!availableSlots.Contains(slot))
            {
                Debug.LogWarning("Slot not in available equipment slots.");
                return;
            }
            if (item.type != slot.type)
            {
                Debug.LogWarning("Item and slot types mismatch.");
                return;
            }

            if (items.ContainsKey(slot))
            {
                RemoveItem(items[slot]);
            }
            onEquip?.Invoke(item);
            items.Add(slot, item);
        }

        public void RemoveItem(EquipmentData item)
        {
            foreach(KeyValuePair<Slot, EquipmentData> kvp in items)
            {
                if(kvp.Value == item)
                {
                    items.Remove(kvp.Key);
                    onUnequip?.Invoke(item);
                    return;
                }
            }
        }

        public bool isEquipped(EquipmentData item)
        {
            return items.ContainsValue(item);
        }

        private bool HasSlotOfType(EquipmentCategory type)
        {
            foreach(Slot slot in availableSlots)
            {
                if (slot.type == type)
                {
                    return true;
                }
            }
            return false;
        }

        private bool HasEmptySlotOfType(EquipmentCategory type)
        {
            foreach (Slot slot in availableSlots)
            {
                if (slot.type == type && !items.ContainsKey(slot))
                {
                    return true;
                }
            }
            return false;
        }

        private Slot GetSlotById(string id)
        {
            foreach (Slot slot in availableSlots)
            {
                if (slot.id == id)
                {
                    return slot;
                }
            }
            return null;
        }

        [System.Serializable]
        public struct SaveData
        {
            public Dictionary<string, string> items;
        }

        public object PrepareSaveData()
        {
            SaveData saveData = new();
            saveData.items = new();
            foreach(KeyValuePair<Slot, EquipmentData> kvp in items)
            {
                saveData.items.Add(kvp.Key.id, kvp.Value.type.name + "/" + kvp.Value.name);
            }
            return saveData;
        }

        public void RestoreSaveData(object save)
        {
            SaveData saveData = (SaveData)save;
            if (saveData.items == null)
            {
                return;
            }
            foreach(KeyValuePair<string, string> kvp in saveData.items)
            {
                Slot slot = GetSlotById(kvp.Key);
                EquipmentData item = EquipmentData.LoadFromResourceName(kvp.Value);
                items.Add(slot, item);
            }
        }
    }
}