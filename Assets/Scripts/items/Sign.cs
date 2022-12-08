using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class Sign : InteractableScript
{
    public string[] signText; 
    
    private void Start()
    {
        SetDialog(signText);
    }


    public override void ItemEffect()
    {
        //no effect
    }
}
