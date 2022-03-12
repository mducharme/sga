using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat.Weapon
{
    /**
     * Ranged weapon controller
     */
    public class RangedController : MonoBehaviour
    {
        [SerializeField] private RangedData data;

        [SerializeField] private GameObject targetBullet;
        [SerializeField] private GameObject targetTracer;

        [SerializeField] private List<Transform> emitters = new();

        [SerializeField] private bool isAutoFiring;

        public delegate void OnShoot();
        public OnShoot onShoot;

        public RangedData Data { get => data; set => data = value; }

        public Fighter Attacker { get => attacker; set => attacker = value; }

        // Calculated from weapon's data's fire-per-seconds.
        private float fireRate;
        // Internal timer.
        private float timer;
        // The weapon holder.
        private Fighter attacker;

        private Transform currentEmitter;
        private int currentEmitterIndex = 0;

        private AudioSource audioSource = new();

        private void Awake()
        {
            ResetFireRate();

            timer = 0;
        }

        private void Update()
        {
            timer += Time.deltaTime;
            if (isAutoFiring)
            {
                Shoot();
            }
        }

        public void Shoot()
        {
            if (data == null)
            {
                return;
            }

            if (timer < fireRate)
            {
                return;
            }
            timer = 0;

            onShoot?.Invoke();

            switch (data.emitterMode)
            {
                case RangedData.EmitterMode.Simultaneous:
                    foreach (Transform emitter in emitters)
                    {
                        currentEmitter = emitter;
                        ShootFire();
                    }
                    break;

                case RangedData.EmitterMode.Alternating:
                    currentEmitter = emitters[currentEmitterIndex];

                    ShootFire();

                    currentEmitterIndex++;
                    if (currentEmitterIndex >= emitters.Count)
                    {
                        currentEmitterIndex = 0;
                    }
                    break;
            }
        }

        private void ShootFire()
        {
            switch (data.projectile.fireMode)
            {
                case ProjectileData.FireMode.Bullet:
                    ShootBullet();
                    break;

                case ProjectileData.FireMode.Tracer:
                    ShootTracer();
                    break;
            }
        }

        private void ShootBullet()
        {
            GameObject bullet = Instantiate(targetBullet); // @todo Use object pool
            if (bullet == null)
            {
                return;
            }

            bullet.transform.SetPositionAndRotation(currentEmitter.position, currentEmitter.rotation);

            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.AddForce(data.shootForce * bullet.transform.forward, ForceMode.VelocityChange);

            StartCoroutine(StartFire(bullet));
        }

        private void ShootTracer()
        {
            Ray ray = new(currentEmitter.position, currentEmitter.forward);
            float shotDistance = data.range;
            if (Physics.Raycast(ray, out RaycastHit hit, shotDistance))
            {
                shotDistance = hit.distance;
            }

            GameObject tracer = Instantiate(targetTracer);
            if (tracer == null)
            {
                return;
            }

            LineRenderer tracerLine = tracer.GetComponent<LineRenderer>();
            tracerLine.SetPosition(0, ray.origin);
            tracerLine.SetPosition(1, ray.origin + ray.direction * shotDistance);

            StartCoroutine(StartFire(tracer));
        }

        private IEnumerator StartFire(GameObject fire)
        {
            fire.SetActive(true);

            Attack attack = attacker.GetAttack(CombatType.Ranged);
            attack.AddModifier(data.attackModifier);
            attack.AddModifier(data.projectile.attackModifier);

            AttackCollision attackCollision = fire.GetComponent<AttackCollision>();
            attackCollision.Attack = attack;

            yield return new WaitForSeconds(data.fireLifetime);
            Destroy(fire);
        }

        private void ResetFireRate()
        {
            if (data == null || data.firePerSeconds == 0f)
            {
                fireRate = 0f;
            }
            else
            {
                fireRate = 1 / data.firePerSeconds;
            }
        }
    }
}