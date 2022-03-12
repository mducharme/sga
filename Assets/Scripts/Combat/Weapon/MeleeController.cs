using UnityEngine;
using System.Collections;

namespace Combat.Weapon
{
    public class MeleeController : MonoBehaviour
    {
        [SerializeField] private MeleeData data;

        [SerializeField] private AttackCollision attackCollision;

        [SerializeField] private bool isAutoAttacking;

        // Calculated from the weapon data's attacks-per-second.
        private float attackRate;
        // Internal timer.
        private float timer;
        // The weapon holder.
        private Fighter attacker;

        public delegate void OnAttack();
        public OnAttack onAttack;

        public MeleeData Data { get => data; set => data = value; }
        public Fighter Attacker { get => attacker; set => attacker = value; }


        private void Start()
        {
            ResetAttackRate();
            timer = 0;
        }

        private void Update()
        {
            timer += Time.deltaTime;
            if (isAutoAttacking)
            {
                Attack();
            }
        }

        public void Attack()
        {
            if (data == null || attacker == null)
            {
                return;
            }

            // @todo: Combo (1-2-3)
            if (timer < attackRate)
            {
                return;
            }
            timer = 0;

            onAttack?.Invoke();
            StartCoroutine(StartAttack());
        }

        private IEnumerator StartAttack()
        {
            attackCollision.Attack = attacker.GetAttack(CombatType.Melee);
            attackCollision.gameObject.SetActive(true);
            yield return new WaitForSeconds(data.attackDuration);
            attackCollision.Attack = null;
            attackCollision.gameObject.SetActive(false);
        }

        private void ResetAttackRate()
        {
            if (data == null || data.attacksPerSeconds == 0f)
            {
                attackRate = 0f;
            }
            else
            {
                attackRate = 1 / data.attacksPerSeconds;
            }
        }

    }
}