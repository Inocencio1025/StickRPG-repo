using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinScript : Unit
{
    GameObject battleSystemGO;
    BattleSystem battleSystem;
    Unit unit;

    

    private void Start()
    {
        battleSystemGO = GameObject.Find("BattleSystem");
        battleSystem = battleSystemGO.GetComponent<BattleSystem>();
        unit = GetComponent<Unit>();
        animator = GetComponent<Animator>();
    }

    public void LowerCounterRate()
    {
        unit.counterRate /= 2;
    }

    public override void TakeTurn()
    {
        throw new System.NotImplementedException();
    }
}
