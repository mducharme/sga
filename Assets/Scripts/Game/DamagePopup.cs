using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Game
{
    public class DamagePopup : MonoBehaviour
    {
        [SerializeField] private TextMeshPro text;
        [SerializeField] private float moveSpeed = 1f;

        private void Update()
        {
            transform.LookAt(Camera.main.transform);
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + moveSpeed * Time.deltaTime, transform.localPosition.z);
        }

        public void SetResult(Combat.Result result)
        {
            string damage = result.GetTotalDamage().ToString();
            if (result.isCritical)
            {
                damage = "<style=\"Critical\">" + damage + "</style>";
            }
            text.text = damage;
        }
    }
}
