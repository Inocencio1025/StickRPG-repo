using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;



public class HealShells : MonoBehaviour
{

    GameObject shells;
    public TextMeshProUGUI text;
    public GameObject dialogBox;
    public string dialog;

    public bool triggerBattle = false;

    bool playerInRange = false;
    private void Start()
    {
        shells = this.gameObject;
    }
    public void OnExamine()  // change method to button pressed to check button is pressed
    {
        //Debug.Log("button pressed");
        if (playerInRange == true)
        {


            if (dialogBox.activeInHierarchy)
            {
                //Debug.Log("box should NOT appear");
                dialogBox.SetActive(false);
            }
            else
            {
                //Debug.Log("box should appear");
               

                dialogBox.SetActive(true);
                text.text = dialog;
                StartCoroutine(SetInactive());
            }
        }
    }

    IEnumerator SetInactive()
    {
        yield return new WaitForSeconds(2f);

        shells.SetActive(false);
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
            dialogBox.SetActive(false);

        }
    }







}
