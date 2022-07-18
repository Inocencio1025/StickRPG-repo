using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RandomEncounterScript : MonoBehaviour
{
    public BattleSystem battlesystem;
    public int encounterChance; // chances of battle in area
    int encounterNum;  //number to be randomized
  
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        encounterNum = Random.Range(1, 101);
        Debug.Log(encounterNum);


        if (collision.CompareTag("Player"))
            if (encounterNum < encounterChance)
                StartCoroutine(battlesystem.SetupBattle());

        
    }

}
