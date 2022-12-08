using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JackScript : Unit
{
    UnitBattleButtons actionButtons;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public override void TakeTurn()
    {
        actionButtons.EnableAttackButton();
        actionButtons.EnableItemButton();

        //execute attack

    }
    
}
