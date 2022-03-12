using System.Collections;
using UnityEngine;

namespace Equipment
{
    [System.Serializable]
    public class Slot
    {
        public string id;
        public EquipmentCategory type;

        public Slot(EquipmentCategory type)
        {
            this.type = type;
        }
    }
}