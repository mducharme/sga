using System.Collections.Generic;

namespace Combat
{
    [System.Serializable]
    public class Attack
    {
        public readonly CombatType type;
        public readonly IAttacker attacker;

        public List<Damage> damages = new();

        public float criticalChance;
        public float knockbackForce;
        public float disableDuration;

        public Attack(CombatType type = CombatType.Base, IAttacker attacker =null)
        {
            this.type = type;
            this.attacker = attacker;
        }

        public void AddModifiers(List<AttackModifier> attacks)
        {
            foreach (AttackModifier attack in attacks)
            {
                AddModifier(attack);
            }
        }

        public void AddModifier(AttackModifier attack)
        {
            if (attack.type != CombatType.Base && attack.type != type)
            {
                return;
            }

            foreach (Damage d in attack.damages)
            {
                AddDamage(d);
            }

            criticalChance += attack.criticalChance;
            knockbackForce += attack.knockbackForce;
            disableDuration += attack.disableDuration;
            // @todo Handle buffs
        }

        public void RemoveModifier(AttackModifier attack)
        {
            if (attack.type != CombatType.Base && attack.type != type)
            {
                return;
            }

            foreach (Damage d in attack.damages)
            {
                RemoveDamage(d);
            }


            criticalChance -= attack.criticalChance;
            knockbackForce -= attack.knockbackForce;
            disableDuration -= attack.disableDuration;
        }

        public void AddDamage(DamageCategory type, int amount)
        {
            AddDamage(new Damage(type, amount));
        }

        public void AddDamage(Damage damage)
        {
            Damage exist = damages.Find(d => d.type == damage.type);
            if (exist != null)
            {
                exist.amount += damage.amount;
            }
            else
            {
                damages.Add(damage);
            }
        }

        public void RemoveDamage(Damage damage)
        {
            Damage exist = damages.Find(d => d.type == damage.type);
            if (exist != null)
            {
                exist.amount -= damage.amount;
            }
        }

        public int GetDamageAmountOfType(DamageCategory type)
        {
            Damage exist = damages.Find(d => d.type == type);
            return exist != null ? exist.amount : 0;
        }
    }
}