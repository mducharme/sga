using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterExample : MonoBehaviour
{
    [SerializeField] private Combat.Fighter fighter1;
    [SerializeField] private Combat.Fighter fighter2;

    public void AttackFighter1()
    {
        Combat.Attack atk = fighter2.GetAttack();
        Combat.Defense def = fighter1.GetDefense();
        Combat.Result result = Combat.Battle.Fight(atk, def);
        Debug.Log("Attack on fighter 1 has done " + result.GetTotalDamage() + "points of damage");
    }

    public void AttackFighter2()
    {
        Combat.Attack atk = fighter1.GetAttack();
        Combat.Defense def = fighter2.GetDefense();
        Combat.Result result = Combat.Battle.Fight(atk, def);
        Debug.Log("Attack on fighter 2 has done " + result.GetTotalDamage() + "points of damage");
    }
}
