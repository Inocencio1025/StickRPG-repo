using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatTree : MonoBehaviour
{
    GameManager manager;
    Animator animator;
    AudioSource treeShake;

    bool playerInRange = false;
    float timer;
    float maxTime;



    private void Start()
    {
        timer = 0f;
        maxTime = Random.Range(2f, 7f);
        animator = GetComponent<Animator>();
        treeShake = GetComponent<AudioSource>();
        manager = FindObjectOfType<GameManager>();

    }

    private void Update()
    {
        if (manager.state == GameState.OVERWORLD)
        {
            if (playerInRange)
            {
                timer += Time.deltaTime;

                if (timer > maxTime)
                {
                    animator.SetTrigger("shake");
                    treeShake.Play();
                    maxTime = Random.Range(1f, 5f);
                    timer = 0f;
                }
            }
        }
    }
    void OnExamine()
    {

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
