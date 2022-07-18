using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using TMPro;

public enum GameState { OVERWORLD, BATTLE }
public class GameManager : MonoBehaviour
{
    public GameState state;
    public BattleSystem battlesystem;
    public PlayerOverWorldController playerController;

    //For UI
    public TextMeshProUGUI text;
    public GameObject option1Button;
    public GameObject option2Button;
    public GameObject option3Button;
    public GameObject option4Button;
    public GameObject nextButton;
   
    public TextMeshProUGUI option1text;
    public TextMeshProUGUI option2text;
    public TextMeshProUGUI option3text;
    public TextMeshProUGUI option4text;

    public GameObject dialogBox;


    //cameras
    public CinemachineVirtualCamera overWorldCam;
    public CinemachineVirtualCamera battleCam;

    public float timer = 0f; // for random encounters
    public int encounterChance; // chances of battle in area
    int encounterNum;  //number to be randomized


    public int gold = 0;
    public int potions = 0;

    bool chestThief = false;
    bool carThief = false;

   
    private void Start()
    {
        state = GameState.OVERWORLD;
        option1Button.SetActive(false);
        option2Button.SetActive(false);
    }
    private void Update()
    {
        if (state == GameState.OVERWORLD)
        {
            timer += Time.deltaTime;
            
            if (timer > 2.0f)
            {
                EncounterChance();
                timer = 0f;
            }
        }
    }


    void EncounterChance()
    {
        encounterNum = Random.Range(1, 101);
        


        if (encounterNum < encounterChance)
        {
            // enter battle music for regular encounters
            StartBattle();
        }
    }

    public void StartBattle()
    {
        Debug.Log("enemy encounter");
        dialogBox.SetActive(false);
        state = GameState.BATTLE;
        StartCoroutine(battlesystem.SetupBattle());
        battleCam.Priority += 2;
    }

    public void EndBattle()
    {

        battleCam.Priority -= 2;
        state = GameState.OVERWORLD;
    }


    public IEnumerator TextBoxDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        dialogBox.SetActive(false);
       
    }


}
