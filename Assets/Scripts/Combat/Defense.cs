﻿using System.Collections.Generic;

namespace Combat
{
    [System.Serializable]
    public class Defense
    {
        public readonly CombatType type;
        public readonly IDefender defender;

        public List<Damage> protections = new();

        public float criticalResist;
        public float knockbackResist;
        public float disableResist;

        public Defense(CombatType type = CombatType.Base, IDefender defender = null)
        {
            this.type = type;
            this.defender = defender;
        }

        public void AddModifiers(List<DefenseModifier> defenses)
        {
            foreach (DefenseModifier defense in defenses)
            {
                AddModifier(defense);
            }
        }

        public void AddModifier(DefenseModifier defense)
        {
            if (defense.type != CombatType.Base && defense.type != type)
            {
                return;
            }

            foreach (Damage protection in defense.protections)
            {
                AddProtection(protection);
            }

            criticalResist += defense.criticalResist;
            knockbackResist += defense.knockbackResist;
            disableResist += defense.disableResist;
        }

        public void RemoveModifier(DefenseModifier defense)
        {
            if (defense.type != CombatType.Base && defense.type != type)
            {
                return;
            }

            foreach (Damage protection in defense.protections)
            {
                RemoveProtection(protection);
            }

            criticalResist -= defense.criticalResist;
            knockbackResist -= defense.knockbackResist;
            disableResist -= defense.disableResist;
        }

        public void AddProtection(DamageCategory type, int amount)
        {
            AddProtection(new Damage(type, amount));
        }

        public void AddProtection(Damage protection)
        {
            Damage exist = protections.Find(d => d.type == protection.type);
            if (exist != null)
            {
                exist.amount += protection.amount;
            }
            else
            {
                protections.Add(protection);
            }
        }

        public void RemoveProtection(Damage protection)
        {
            Damage exist = protections.Find(d => d.type == protection.type);
            if (exist != null)
            {
                exist.amount -= protection.amount;
            }
        }

        public int GetProtectionAmountOfType(DamageCategory type)
        {
            Damage exist = protections.Find(d => d.type == type);
            return exist != null ? exist.amount : 0;
        }
    }
}