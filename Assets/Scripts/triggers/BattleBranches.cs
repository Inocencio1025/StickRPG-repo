using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleBranches : MonoBehaviour
{
    
    public GameObject battleBranches;

  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            battleBranches.SetActive(true);
            //Debug.Log("box entered");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            battleBranches.SetActive(false);


        }


    }
}
