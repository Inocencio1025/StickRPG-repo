using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleHud : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public Slider hpSlider;
    //public GameObject playerhud;
    int maxhp;


    public Animator animator;



    public void SetHUD(Unit unit)
    {
        //animator = playerhud.GetComponent<Animator>();

        nameText.text = unit.unitName;
        hpSlider.maxValue = unit.maxHP;
        hpSlider.value = unit.currentHP;

        maxhp = unit.maxHP;

    }

    public void SetHP(int hp)
    {
        hpSlider.value = hp;
        if (hp < maxhp / 3)
            animator.SetBool("lowHP", true);
        else
            animator.SetBool("lowHP", false);

    }


}
