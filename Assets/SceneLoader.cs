using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public GameManager gameManager;
    public Animator transition;
    public float transitionTime;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
            StartCoroutine(LoadScene());
            //Debug.Log("box entered");
        }
    }

 
    IEnumerator LoadScene()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(1);
    }
}
