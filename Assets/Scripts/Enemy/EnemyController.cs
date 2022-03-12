using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Combat.Fighter))]
    public class EnemyController : MonoBehaviour
    {
        #region Fields
        [SerializeField] private EnemyData data;
        [SerializeField] private float deathDelay = 0f;

        private Animator animator;
        private Combat.Fighter fighter;
        #endregion

        #region Properties
        public EnemyData Data { get => data; private set { } }
        public Combat.Fighter Fighter { get => fighter; private set { } }
        #endregion

        #region Unity Lifecycle
        void Awake()
        {
            animator = GetComponent<Animator>();
            fighter = GetComponent<Combat.Fighter>();

            fighter.onHitByAttack += OnHitByAttack;
            fighter.onKilledByAttack += OnKilledByAttack;
        }

        void OnDestroy()
        {
            fighter.onHitByAttack -= OnHitByAttack;
            fighter.onKilledByAttack -= OnKilledByAttack;
        }
        #endregion

        private void OnHitByAttack(Combat.Result result)
        {
            if (result.GetTotalDamage() <= 0)
            {
                // Attack missed (prevented by defense)
                return;
            }


            if (animator != null)
            {
                animator.SetBool("isHit", true);
            }
            if (data.hitSoundEffects != null)
            {
                data.hitSoundEffects.Play();
            }

            // Knockback
            Rigidbody rb = fighter.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(30 * rb.mass * -transform.forward, ForceMode.Impulse);
            }

            //DamagePopup.Create(transform.position, result.GetTotalDamage());
        }

        private void OnKilledByAttack(Combat.Result result)
        {
            StartCoroutine(Death());
        }

        private IEnumerator Death()
        {
            if (animator != null)
            {
                animator.SetBool("isDead", true);
            }
            if (data.deathSoundEffects != null)
            {
                data.deathSoundEffects.Play();
            }
            if (data.deathParticles != null)
            {
                data.deathParticles.Play();
            }

            foreach(Loot loot in data.loots)
            {
                if (Random.Range(0f, 1f) < loot.chance)
                {
                    // @todo Drop item on scene.
                    Player.PlayerController.instance.PickupItem(loot.item);
                }
            }

            yield return new WaitForSeconds(deathDelay);
            Reset();
        }

        private void Reset()
        {
            if (animator != null)
            {
                animator.SetBool("isDead", false);
            }
            fighter.Health.HealAll();
            gameObject.SetActive(false);
            // Destroy(gameObject);
        }
    }
}