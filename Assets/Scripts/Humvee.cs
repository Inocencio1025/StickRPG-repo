using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Humvee : MonoBehaviour
{
    public Dialog dialog;
    public Dialog dialog1;
    public GameManager manager;

    Animator animator;
    public BoxCollider2D collider;
    public int goldAmount;

    bool playerInRange = false;

    public void OnExamine()  
    {

        //Debug.Log("button pressed");
        if (playerInRange == true)
        {
            
            Dialog();
            manager.nextButton.SetActive(false);
            manager.option3Button.SetActive(true);
            manager.option4Button.SetActive(true);
            Debug.Log("HAHAHAHHA");
        }
    }

   

    public void WalkAway()
    {
        FindObjectOfType<DialogManager>().DisplayNextSentence();

        Dialog1();
        manager.option3Button.SetActive(false);
        manager.option4Button.SetActive(false);
        StartCoroutine(CloseDialog());
    }

    IEnumerator CloseDialog()
    {
        yield return new WaitForSeconds(1f);
        FindObjectOfType<DialogManager>().DisplayNextSentence();

    }


    public void CarBreakIn() /// things ill need
    {

        manager.text.text = goldAmount + " gold found!";

        //animator.SetBool("Opened", true);

        manager.gold += goldAmount;


        manager.option3Button.SetActive(false);
        manager.option4Button.SetActive(false);
        StartCoroutine(CloseCollider());
    }
    //
    IEnumerator CloseCollider()
    {
        yield return new WaitForSeconds(2f);

        collider.enabled = false;
    }

    public void Dialog()
    {
        FindObjectOfType<DialogManager>().StartDialog(dialog);
    }

    public void Dialog1()
    {
        FindObjectOfType<DialogManager>().StartDialog(dialog1);
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
            manager.option3Button.SetActive(false);
            manager.option4Button.SetActive(false);






        }


    }
}
