using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSimulator : MonoBehaviour
{
    [SerializeField] private Combat.Fighter fighter1;
    [SerializeField] private Combat.Fighter fighter2;

    public void BaseAttackFighter1()
    {
        Combat.Result result = fighter1.HandleAttack(fighter2.GetAttack(Combat.CombatType.Base));

        string critical = result.isCritical ? " Critical!" : "";
        Debug.Log("Base Attack on fighter 1 has done " + result.GetTotalDamage() + " points of damage." + critical);
    }

    public void BaseAttackFighter2()
    {
        Combat.Result result = fighter2.HandleAttack(fighter1.GetAttack(Combat.CombatType.Base));

        string critical = result.isCritical ? " Critical!" : "";
        Debug.Log("Base Attack on fighter 2 has done " + result.GetTotalDamage() + " points of damage." + critical);
    }

    public void MeleeAttackFighter1()
    {
        Combat.Result result = fighter1.HandleAttack(fighter2.GetAttack(Combat.CombatType.Melee));

        string critical = result.isCritical ? " Critical!" : "";
        Debug.Log("Melee Attack on fighter 1 has done " + result.GetTotalDamage() + " points of damage." + critical);
    }

    public void MeleeAttackFighter2()
    {
        Combat.Result result = fighter2.HandleAttack(fighter1.GetAttack(Combat.CombatType.Melee));

        string critical = result.isCritical ? " Critical!" : "";
        Debug.Log("Melee Attack on fighter 2 has done " + result.GetTotalDamage() + " points of damage." + critical);
    }
}
