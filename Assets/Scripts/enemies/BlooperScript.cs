using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlooperScript : Unit
{
    GameObject battleSystemGO;
    BattleSystem battleSystem;
    bool isDead;
    

    private void Start()
    {
        battleSystemGO = GameObject.Find("BattleSystem");
        battleSystem = battleSystemGO.GetComponent<BattleSystem>();
        
    }
    public void Poison()
    {
        
        battleSystem.playerUnit[0].poisonNum += 3;
        battleSystem.playerUnit[1].poisonNum += 3;

        battleSystem.playerUnit[0].animator.SetTrigger("damaged");
        battleSystem.playerUnit[1].animator.SetTrigger("damaged");

        battleSystem.playerHUD[0].animator.SetBool("poisoned", true);
        battleSystem.playerHUD[1].animator.SetBool("poisoned", true);

    }

    public void AttackMarker()
    {
        battleSystem.playerBattleStationAnimator[0].SetTrigger("attacked");
        battleSystem.playerBattleStationAnimator[1].SetTrigger("attacked");

    }

    public override void TakeTurn()
    {
        throw new System.NotImplementedException();
    }
}
