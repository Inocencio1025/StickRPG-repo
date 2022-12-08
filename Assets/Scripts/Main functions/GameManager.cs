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


    

    //flags for player decisions
    bool chestThief = false;
    bool carThief = false;

   
    private void Start()
    {
        state = GameState.OVERWORLD;
        option1Button.SetActive(false);
        option2Button.SetActive(false);
    }

    //activated every frame while moving
    public void ActivateEncounterChance()
    {
        timer += Time.deltaTime;

        if (timer > 2.0f)
        {
            EncounterChance();
            timer = 0f;
        }
        
    }

    //executes when timer runs out in ActivateEncounterChance()
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
        battleCam.Priority += 2;

        StartCoroutine(battlesystem.SetupBattle());
    }

    public void EndBattle()
    {
        battleCam.Priority -= 2;
        state = GameState.OVERWORLD;
    }

}
