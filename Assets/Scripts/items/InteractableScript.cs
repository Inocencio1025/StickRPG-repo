using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public abstract class InteractableScript : MonoBehaviour
{
    
    private Dialog dialog;
    private bool playerInRange = false;
    public bool isCollectable;
    public InventoryScript inventory;
   
    private void Awake()
    {
        inventory = FindObjectOfType<InventoryScript>();
    }
    //Overloaded methods for setting dialog
    public void SetDialog(string text)
    {
        dialog = new Dialog(text);
    }
    public void SetDialog(string[] text)
    {
        dialog = new Dialog(text);
    }
    public void SetDialog(string name, string text)
    {
        dialog = new Dialog(name, text);
    }
    public void SetDialog(string name, string[] text)
    {
        dialog = new Dialog(name, text);
    }

    // abstract method for effect of item
    // on player, mostly adding to inventory
    public abstract void ItemEffect();

    public void OnExamine()  
    {
        //Debug.Log("button pressed");
        if (playerInRange == true) 
        {
            ItemEffect();
            Dialog();
            
            if(isCollectable == true)
                Destroy(this.gameObject);
        }
    }
    
    public void Dialog()
    {
        FindObjectOfType<DialogManager>().StartDialog(dialog);
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
           
        }
    }

    

}
