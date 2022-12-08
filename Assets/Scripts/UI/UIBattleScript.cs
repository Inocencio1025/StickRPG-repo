using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitBattleButtons : MonoBehaviour
{
    Canvas battleCanvas;
    public List<Button> attackBtn;
    public List<Button> itemBtn;
    // Start is called before the first frame update
    void Start()
    {
        battleCanvas = GetComponent<Canvas>();
        battleCanvas.enabled = false;
    }
    public void EnableAttackButton()
    {
        foreach(Button btn in attackBtn)
        {
            btn.enabled = true;
        }
    }
    public void EnableItemButton()
    {
        foreach (Button btn in itemBtn)
        {
            btn.enabled = true;
        }
    }
    public void DisableAllButtons()
    {
        foreach(Button btn in attackBtn)
        {
            btn.enabled = false;
        }
    }
    public void OnAttackButton()
    {
        

        //playerUnit[0].animator.SetTrigger("attack");
        //StartCoroutine(PlayerAttack(playerUnit[0], currentTarget));
    }



   
}
