using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIOverworld : MonoBehaviour
{
    GameObject battleCanvas;

    public List<Button> attackBtn;
    
    public void SetupBattleConvas()
    {
        battleCanvas.SetActive(true);

        disableAllButtons();

        
        

    }

    private void disableAllButtons()
    {
        //disable all buttons
        foreach (Button abtn in attackBtn)
            abtn.interactable = false;
    }

    void SetTurnImages()
    {

    }



    //for setting up playing battlehuds
    //----------------------------------
    //playerHUD[i].SetHUD(playerUnit[i]);
    //playerHUD[i].SetHP(playerUnit[i].currentHP);
    //enemyHUD[0].SetHUD(enemyUnit[0]);
    //enemyHUD[1].SetHUD(enemyUnit[1]);
}
