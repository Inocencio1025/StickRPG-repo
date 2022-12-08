using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEyeScript : Unit
{
    GameObject battleSystemGO;
    BattleSystem battleSystem;
    int attnum;
    


    private void Start()
    {
        battleSystemGO = GameObject.Find("BattleSystem");
        battleSystem = battleSystemGO.GetComponent<BattleSystem>();
        
    }
    public void DoubleAttack()
    {
        do
        {
            attnum = Random.Range(0, 4);
        }
        while (battleSystem.playerUnit[attnum].currentHP <= 0 || battleSystem.playerUnit[attnum] == null);

        battleSystem.isDead = battleSystem.playerUnit[attnum].TakeDamage(damage);


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
        
        specAttRate += 25;
    }

    public override void TakeTurn()
    {
        throw new System.NotImplementedException();
    }
}
