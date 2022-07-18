using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CatFightTrigger : MonoBehaviour
{
    bool playerInRange = false;
    int num = 0;
    public Dialog dialog;

    public GameObject cat;
    BattleSystem battleSystem;
    DialogManager dialogManager;
    private void Start()
    {
        battleSystem = FindObjectOfType<BattleSystem>();
        dialogManager = FindObjectOfType<DialogManager>();
    }
    void OnExamine()
    {
        if (playerInRange == true)
        {
            switch (num)
            {
                case 0:
                    num++;
                    Dialog();
                    break;
                case 1:
                    num++;
                    dialogManager.DisplayNextSentence(); 
                    break;
                case 2:
                    
                    //enter cat battle theme
                    battleSystem.enemiesInBattle.Add(cat);
                    FindObjectOfType<GameManager>().StartBattle();
                    break;
            }
           
        }
    }

    void Dialog()
    {
        dialogManager.StartDialog(dialog);
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
            playerInRange = false;
            //Debug.Log("box exited");



        }
    }
}
