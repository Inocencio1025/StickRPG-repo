using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : InteractableScript
{
    int amt;
    private void Start()
    {
        SetDialog(amt + " Food eaten");
        this.isCollectable = true;
    }

    public override void ItemEffect()
    {
        //Add health here
    }

}
