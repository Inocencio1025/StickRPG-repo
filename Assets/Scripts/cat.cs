using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cat : MonoBehaviour
{
    int damageHolder;
    Unit unit;
    public void Start()
    {
        unit = GetComponent<Unit>();
        damageHolder = unit.damage;

    }
    public void ZeroDamage()
    {
        
        unit.damage = 0;
        
    }

    public void NormalDamage()
    {
        unit.damage = damageHolder;
    }
}
