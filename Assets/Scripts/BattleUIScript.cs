using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUIScript : MonoBehaviour
{
    Canvas BattleCanvas;
    // Start is called before the first frame update
    void Start()
    {
        BattleCanvas = GetComponent<Canvas>();
        BattleCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
