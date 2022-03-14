using UnityEngine;
using Combat;

namespace Combat.Weapon
{
    [CreateAssetMenu(fileName = "New Shield Object", menuName = "Combat/Shield", order = 0)]
    public class ShieldData : ScriptableObject
    {
        static public string RESOURCE_FOLDER = "Data/Shields/";

        public string id;
        public string category;
        public new string name;
        [TextArea(3, 15)]
        public string description;

        public int level;

        [Header("Stats")]
        public Attributes attributes;

        static public ShieldData LoadFromResourceName(string name)
        {
            return (ShieldData)Resources.Load(RESOURCE_FOLDER + name);
        }
    }
}