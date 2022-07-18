using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class Sign : MonoBehaviour
{

    GameManager gameManager;


    public Dialog dialog;

    bool playerInRange = false;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    public void OnExamine()  // change method to button pressed to check button is pressed
    {
        //Debug.Log("button pressed");
        if (playerInRange == true)
        {
            gameManager.nextButton.SetActive(true);
            Dialog();

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
            // Debug.Log("box entered");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
            // Debug.Log("box exited");
            gameManager.nextButton.SetActive(false);

            gameManager.dialogBox.SetActive(false);

        }
    }


    public void Dialog()
    {
        FindObjectOfType<DialogManager>().StartDialog(dialog);
    }





}
