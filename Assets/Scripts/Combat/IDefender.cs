using System.Collections;
using UnityEngine;

namespace Combat
{
    public interface IDefender
    {
        public Result HandleAttack(Attack attack);
        public Defense GetDefense(CombatType type = CombatType.Base);
        public void HitByAttack(Result result);
        public void KilledByAttack(Result result);
    }
}