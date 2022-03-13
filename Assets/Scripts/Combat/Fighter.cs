using UnityEngine;

namespace Combat
{
    public class Fighter : MonoBehaviour, Game.ISaveable, IAttacker, IDefender
    {
        [SerializeField] private Health health;

        [SerializeField] private Weapon.MeleeController meleeWeapon;

        [SerializeField] private Weapon.RangedController rangedWeapon;

        [SerializeField] Attributes coreAttributes;
        [SerializeField] Attributes transientAttributes;
        private readonly Attributes mergedAttributes = new();

        public Health Health { get => health; private set { } }
        public Weapon.MeleeController MeleeWeapon
        {
            get => meleeWeapon; set
            {
                meleeWeapon = value;
                meleeWeapon.Attacker = this;
            }
        }
        public Weapon.RangedController RangedWeapon
        {
            get => rangedWeapon; set
            {
                rangedWeapon = value;
                rangedWeapon.Attacker = this;
            }
        }
        public Attributes Attributes { get => mergedAttributes; private set { } }

        public delegate void OnHitByAttack(Result result);
        public delegate void OnKilledByAttack(Result result);
        public delegate void OnAttackHasHit(Result result);
        public delegate void OnAttackHasKilled(Result result);

        public OnHitByAttack onHitByAttack;
        public OnKilledByAttack onKilledByAttack;
        public OnAttackHasHit onAttackHasHit;
        public OnAttackHasKilled onAttackHasKilled;

        private void Awake()
        {
            mergedAttributes.Add(coreAttributes);
            mergedAttributes.Add(transientAttributes);

            if (meleeWeapon != null)
            {
                meleeWeapon.Attacker = this;
            }
            if (rangedWeapon != null)
            {
                rangedWeapon.Attacker = this;
            }
        }

        public Result HandleAttack(Attack attack)
        {
            Result result = Battle.Fight(attack, GetDefense(attack.type));

            health.Damage(result.GetTotalDamage());

            HitByAttack(result);
            if (attack.attacker != null)
            {
                attack.attacker.AttackHasHit(result);
            }

            if (health.IsAlive() == false)
            {
                KilledByAttack(result);
                if (attack.attacker != null)
                {
                    attack.attacker.AttackHasKilled(result);
                }
            }

            return result;
        }
        public void HitByAttack(Result result)
        {
            onHitByAttack?.Invoke(result);
        }

        public void KilledByAttack(Result result)
        {
            onKilledByAttack?.Invoke(result);
        }

        public void AttackHasHit(Result result)
        {
            onAttackHasHit?.Invoke(result);
        }

        public void AttackHasKilled(Result result)
        {
            onAttackHasKilled?.Invoke(result);
        }

        public Attack GetAttack(CombatType type = CombatType.Base)
        {
            Attack attack = new(type, this);
            attack.AddModifiers(coreAttributes.attacks);
            attack.AddModifiers(transientAttributes.attacks);
            return attack;
        }

        public Defense GetDefense(CombatType type = CombatType.Base)
        {
            Defense defense = new(type, this);
            defense.AddModifiers(coreAttributes.defenses);
            defense.AddModifiers(transientAttributes.defenses);
            return defense;
        }

        public void AddCoreAttributes(Attributes attr)
        {
            coreAttributes.Add(attr);
            mergedAttributes.Add(attr);
        }

        public void RemoveCoreAttributes(Attributes attr)
        {
            coreAttributes.Remove(attr);
            mergedAttributes.Remove(attr);
        }

        public void AddTransientAttributes(Attributes attr)
        {
            transientAttributes.Add(attr);
            mergedAttributes.Add(attr);
        }

        public void RemoveTransientAttributes(Attributes attr)
        {
            transientAttributes.Remove(attr);
            mergedAttributes.Remove(attr);
        }

        [System.Serializable]
        public struct SaveData
        {
            public Health.SaveData health;
        }

        public object PrepareSaveData()
        {
            SaveData saveData = new();
            saveData.health = (Health.SaveData)health.PrepareSaveData();
            return saveData;
        }

        public void RestoreSaveData(object save)
        {
            SaveData saveData = (SaveData)save;
            health.RestoreSaveData(saveData.health);
        }
    }
}