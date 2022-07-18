using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEyeScript : MonoBehaviour
{
    GameObject battleSystemGO;
    BattleSystem battleSystem;
    int attnum;
    Unit unit;


    private void Start()
    {
        battleSystemGO = GameObject.Find("BattleSystem");
        battleSystem = battleSystemGO.GetComponent<BattleSystem>();
        unit = GetComponent<Unit>();
    }
    public void DoubleAttack()
    {
        do
        {
            attnum = Random.Range(0, 4);
        }
        while (battleSystem.playerUnit[attnum].currentHP <= 0 || battleSystem.playerUnit[attnum] == null);

        battleSystem.isDead = battleSystem.playerUnit[attnum].TakeDamage(unit.damage);


        if (battleSystem.isDead)
            battleSystem.playerUnit[attnum].animator.SetTrigger("defeated");
        else 
            battleSystem.playerUnit[attnum].animator.SetTrigger("damaged");

        battleSystem.playerHUD[attnum].SetHP(battleSystem.playerUnit[attnum].currentHP);
        battleSystem.playerHUD[attnum].animator.SetTrigger("damagedUI");


        Debug.Log(attnum);
    }

    public void IncreaseSpecialRate()
    {
        
        unit.specAttRate += 25;
    }
}
