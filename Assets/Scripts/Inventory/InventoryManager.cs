using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    [System.Serializable]
    public class InventoryManager : MonoBehaviour, Game.ISaveable
    {
        [Tooltip("Maximum number of items. 0 = unlimited.")]
        [SerializeField] private int maxSlots = 0;

        [Tooltip("Maximum weight. 0 = unlimited.")]
        [SerializeField] private int maxWeight = 0;

        [SerializeField] private List<ItemData> items = new();

        private float currentWeight;


        public delegate void OnAddItem(ItemData item);
        public delegate void OnRemoveItem(ItemData item);
        public OnAddItem onAddItem;
        public OnRemoveItem onRemoveItem;


        public List<ItemData> Items { get => items; private set { } }
        public float CurrentWeight { get => currentWeight; private set { } }

        public InventoryManager(int maxSlots = 0, int maxWeight = 0)
        {
            this.maxSlots = maxSlots;
            this.maxWeight = maxWeight;
        }

        public bool AddItem(ItemData item)
        {
            if (maxWeight > 0 && item.weight + currentWeight > maxWeight)
            {
                Debug.LogWarning("Item is too heavy for inventory.");
                return false;
            }
            if (maxSlots > 0 && items.Count + 1 > maxSlots)
            {
                Debug.LogWarning("No more space left in inventory.");
                return false;
            }

            onAddItem?.Invoke(item);
            items.Add(item);
            currentWeight += item.weight;
            return true;
        }

        public bool RemoveItem(ItemData item)
        {
            if (!HasItem(item))
            {
                Debug.LogWarning("This item was not in inventory.");
                return false;
            }

            onRemoveItem?.Invoke(item);
            items.Remove(item);
            currentWeight -= item.weight;
            return true;
        }

        public bool HasItem(ItemData item)
        {
            return items.Contains(item);
        }

        [System.Serializable]
        public struct SaveData
        {
            public List<string> items;
        }

        public object PrepareSaveData()
        {
            SaveData saveData = new();
            saveData.items = new();
            //items.ForEach(i => saveData.items.Add(i.type.ToString() + "/" + i.name));
            return saveData;
        }

        public void RestoreSaveData(object save)
        {
            SaveData saveData = (SaveData)save;
            //saveData.items.ForEach(si => items.Add(InventoryItemData.LoadFromResourceName(si)));
        }
    }
}