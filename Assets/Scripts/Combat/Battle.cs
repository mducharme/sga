using System;

namespace Combat
{
    public class Battle
    {
        public static float CRITICAL_MODIFIER = 1.5f;

        static public Result Fight(Attack attack, Defense defense)
        {
            Result result = new(attack, defense);

            result.isCritical = UnityEngine.Random.Range(0f, 1f) < attack.criticalChance;
            result.knockbackForce = Math.Max(0, attack.knockbackForce - defense.knockbackResist);
            result.disableDuration = Math.Max(0, attack.disableDuration - defense.disableResist);

            foreach (Damage attackDamage in attack.damages)
            {
                int amount = attackDamage.amount - defense.GetProtectionAmountOfType(attackDamage.type);
                if (result.isCritical)
                {
                    amount = (int)MathF.Ceiling(amount * CRITICAL_MODIFIER);
                }
                result.AddDamage(attackDamage.type, amount);
            }

            return result;
        }

    }
}