using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;



public class Chest : MonoBehaviour
{
    public Dialog dialog;
    public Dialog dialog1;
    public GameManager manager;
    
    //public List<string> dialog;
    public BoxCollider2D collider;
    public int goldAmount;

    Animator animator;

    bool playerInRange = false;
    bool looted = false;


    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OnExamine()  // change method to button pressed to check button is pressed
    {
       
        //Debug.Log("button pressed");
        if (playerInRange == true)
        {
            Dialog1();
            manager.nextButton.SetActive(false);
            manager.option1Button.SetActive(true);
            if (!looted)
                manager.option2Button.SetActive(true);

        }
    }

    public void ReadNote()
    {
        FindObjectOfType<DialogManager>().DisplayNextSentence();

        Dialog();
        manager.nextButton.SetActive(true);

        manager.option1Button.SetActive(false);
        manager.option2Button.SetActive(false);


    }
    public void OpenChest() /// things ill need
    {
        looted = true;
        manager.text.text = goldAmount + " gold found!";
        animator.SetBool("Opened", true);
        manager.gold += goldAmount;
         

        manager.option1Button.SetActive(false);
        manager.option2Button.SetActive(false);
        StartCoroutine(CloseDelay());
         
    }
    //

    IEnumerator CloseDelay()
    {

        yield return new WaitForSeconds(2f);
        manager.dialogBox.SetActive(false);
        collider.enabled = false;
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
            manager.dialogBox.SetActive(false);
            manager.option1Button.SetActive(false);
            manager.option2Button.SetActive(false);


            
                


        }


    }


    public void Dialog1()
    {
        FindObjectOfType<DialogManager>().StartDialog(dialog1);
    }

    public void Dialog()
    {
        FindObjectOfType<DialogManager>().StartDialog(dialog);
    }






}
