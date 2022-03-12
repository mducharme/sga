using UnityEngine;

namespace Combat.Weapon
{
    public class AttackCollision : MonoBehaviour
    {
        private Attack attack;

        public Attack Attack { get => attack; set { attack = value; } }

        public delegate void OnHitFighter(Fighter defender);
        public OnHitFighter onHitFighter;

        public delegate void OnHitAnything();
        public OnHitAnything onHitAnything;


        private void OnCollisionEnter2D(Collision2D collision)
        {
            Fighter defender = collision.gameObject.GetComponent<Fighter>();
            HandleAttack(defender);
        }

        private void OnCollisionEnter(Collision collision)
        {
            Fighter defender = collision.gameObject.GetComponent<Fighter>();
            HandleAttack(defender);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Fighter defender = collision.gameObject.GetComponent<Fighter>();
            HandleAttack(defender);
        }

        private void OnTriggerEnter(Collider collision)
        {
            Fighter defender = collision.gameObject.GetComponent<Fighter>();
            HandleAttack(defender);
        }

        private void HandleAttack(Fighter defender)
        {
            onHitAnything?.Invoke();

            if (attack == null || defender == null)
            {
                return;
            }

            if (defender == attack.attacker)
            {
                return;
            }

            onHitFighter?.Invoke(defender);

            defender.HandleAttack(attack);
        }
    }
}