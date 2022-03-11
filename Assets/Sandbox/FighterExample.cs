using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterExample : MonoBehaviour
{
    [SerializeField] private Combat.Fighter fighter1;
    [SerializeField] private Combat.Fighter fighter2;

    public void AttackFighter1()
    {
        Combat.Result result = fighter1.HandleAttack(fighter2.GetAttack());
        Debug.Log("Attack on fighter 1 has done " + result.GetTotalDamage() + "points of damage");
    }

    public void AttackFighter2()
    {
        Combat.Result result = fighter2.HandleAttack(fighter1.GetAttack());
        Debug.Log("Attack on fighter 2 has done " + result.GetTotalDamage() + "points of damage");
    }
}
