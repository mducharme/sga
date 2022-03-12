using System.Collections;
using UnityEngine;

namespace Combat
{
    public interface IAttacker
    {
        public Attack GetAttack(CombatType type = CombatType.Base);

        public void AttackHasHit(Result result);
        public void AttackHasKilled(Result result);
    }
}