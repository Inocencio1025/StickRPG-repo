using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestKingTrigger : MonoBehaviour
{
    public GameObject forrestKing;
    bool playerInRange = false;
    BattleSystem battleSystem;

    private void Start()
    {
        battleSystem = FindObjectOfType<BattleSystem>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
            //Debug.Log("box entered");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            battleSystem.enemiesInBattle.Add(forrestKing);
            FindObjectOfType<GameManager>().StartBattle();
            playerInRange = false;
            //Debug.Log("box exited");



        }
    }
}
