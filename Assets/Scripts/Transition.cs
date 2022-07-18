using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{

    GameObject playerGO;
    public Animator transition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(StartTransition());
        }
    }

    IEnumerator StartTransition()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        playerGO = GameObject.FindWithTag("Player");
        playerGO.transform.position = this.transform.position;
        transition.SetTrigger("End");
    }

  
}
