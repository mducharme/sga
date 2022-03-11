using UnityEngine;

namespace Combat
{
    [CreateAssetMenu(fileName = "DamageCategory", menuName = "Combat/Damage Category", order = 0)]
    public class DamageCategory : ScriptableObject
    {
        public new string name;

        [TextArea(3, 15)]
        public string description;

        // @todo: Color, icon, etc.

        static public DamageCategory LoadFromResourceName(string name)
        {
            return (DamageCategory)Resources.Load("Data/Damage Categories/" + name);
        }
    }
}