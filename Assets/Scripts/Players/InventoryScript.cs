using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScript : MonoBehaviour
{
    public int gold = 0;
    public int potions = 0;

    public int healShells = 0;


    public void AddGold(int amt)
    {
        gold += amt;
    }
    public void RemoveGold(int amt)
    {
        gold -= amt;
    }
    //-----------------------------------------
    public void AddPotions(int amt)
    {
        potions += amt;
    }
    public void RemovePotions(int amt)
    {
        potions -= amt;
    }
    //-----------------------------------------
    public void AddHealShells(int amt)
    {
        healShells += amt;
    }
    public void RemoveHealShells(int amt)
    {
        healShells -= amt;
    }
    //-----------------------------------------
}
