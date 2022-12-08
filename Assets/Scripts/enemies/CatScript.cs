using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatScript : Unit
{
    int strengthStorage;
    
    //constructor
    public CatScript()
    {
        strengthStorage = damage;
    }
    public void Start()
    {
        
    }
    public void setZeroDamage()
    {
        this.setStrength(0);        
    }

    public void NormalDamage() 
    {
        this.setStrength(strengthStorage);
    }

    public override void TakeTurn()
    {
        throw new System.NotImplementedException();
    }
}
