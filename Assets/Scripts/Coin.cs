using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Coin : MonoBehaviour
{
    GameManager manager;
    public TextMeshProUGUI text;
    public GameObject dialogBox;
    public string dialog = " gold found";

    public bool playerInRange = false;
    public int coinAmount;

    private void Start()
    {
        manager = FindObjectOfType<GameManager>();
    }
    public void OnExamine() 
    {
        //Debug.Log("button pressed");
        if (playerInRange == true)
        {
            
            
            //Debug.Log("box should appear");
            dialogBox.SetActive(true);
            text.text = coinAmount + " " + dialog;
            
            manager.gold += coinAmount;

            
            //Debug.Log(coinAmount + " has been added");
            StartCoroutine(manager.TextBoxDelay(2f));
            StartCoroutine(Close());
        }
    }

    IEnumerator Close()
    {
        yield return new WaitForSeconds(2f);

        this.gameObject.SetActive(false);
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
        playerInRange = false;
    }

    

}
