using System.Collections;
using UnityEngine;

namespace Enemy
{
    [System.Serializable]
    public class Loot
    {
        public Inventory.ItemData item;
        public float chance;
    }
}