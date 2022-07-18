using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    // Use this for anything that triggers a dialog
    public Dialog dialog;

    public void Dialog()
    {
        //FindObjectOfType<DialogManager>().StartDialog(dialog);
    }

    // Dialog();
}
