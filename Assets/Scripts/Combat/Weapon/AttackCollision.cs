using UnityEngine;

namespace Combat.Weapon
{
    public class AttackCollision : MonoBehaviour
    {
        private Attack attack;

        public Attack Attack { get => attack; set { attack = value; } }

        public delegate void OnHitFighter(IDefender defender);
        public OnHitFighter onHitFighter;

        public delegate void OnHitAnything();
        public OnHitAnything onHitAnything;


        private void OnCollisionEnter2D(Collision2D collision)
        {
            IDefender defender = collision.gameObject.GetComponent<IDefender>();
            HandleAttack(defender);
        }

        private void OnCollisionEnter(Collision collision)
        {
            IDefender defender = collision.gameObject.GetComponent<IDefender>();
            HandleAttack(defender);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            IDefender defender = collision.gameObject.GetComponent<IDefender>();
            HandleAttack(defender);
        }

        private void OnTriggerEnter(Collider collision)
        {
            IDefender defender = collision.gameObject.GetComponent<IDefender>();
            HandleAttack(defender);
        }

        private void HandleAttack(IDefender defender)
        {
            onHitAnything?.Invoke();

            if (attack == null || defender == null)
            {
                return;
            }

            onHitFighter?.Invoke(defender);

            defender.HandleAttack(attack);
        }
    }
}