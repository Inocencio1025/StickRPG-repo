using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinScript : MonoBehaviour
{
    GameObject battleSystemGO;
    BattleSystem battleSystem;
    Animator animator;
    bool isDead;
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
    



}
