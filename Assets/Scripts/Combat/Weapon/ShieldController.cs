using System.Collections;
using UnityEngine;

namespace Combat.Weapon
{
    public class ShieldController : MonoBehaviour
    {
        [SerializeField] private ShieldData data;
        [SerializeField] private GameObject shieldObject;

        public ShieldData Data { get => data; set => data = value; }

        public void StartShield()
        {
            shieldObject.SetActive(true);
        }

        public void StopShield()
        {
            shieldObject.SetActive(false);
        }
    }
}