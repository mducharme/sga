using UnityEngine;

namespace Combat
{
    public class Fighter : MonoBehaviour, Game.ISaveable
    {
        [SerializeField] private Health health;

        [SerializeField] Attributes coreAttributes;
        [SerializeField] Attributes transientAttributes;
        private readonly Attributes mergedAttributes = new();

        public Health Health { get => health; private set { } }
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
        }

        public Result HandleAttack(Attack attack)
        {
            Result result = Battle.Fight(attack, GetDefense(attack.type));

            health.Damage(result.GetTotalDamage());

            onHitByAttack?.Invoke(result);
            if (attack.attacker != null)
            {
                attack.attacker.onAttackHasHit?.Invoke(result);
            }

            if (health.IsAlive() == false)
            {
                onKilledByAttack?.Invoke(result);
                if (attack.attacker != null)
                {
                    attack.attacker.onAttackHasKilled?.Invoke(result);
                }
            }

            return result;
        }

        public Attack GetAttack(CombatType type = CombatType.Base)
        {
            Attack attack = new(type);
            attack.AddModifiers(coreAttributes.attacks);
            attack.AddModifiers(transientAttributes.attacks);
            return attack;
        }

        public Defense GetDefense(CombatType type = CombatType.Base)
        {
            Defense defense = new(type);
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