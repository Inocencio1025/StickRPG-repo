using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogManager : MonoBehaviour
{
    //public GameManager manager;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogText;
    public GameObject dialogBox;
    private Queue<string> sentences;
    private bool dialogIsActive;

    //bool ChoiceTime = false;
    private void Start()
    {
        sentences = new Queue<string>();
    }
    
    public void StartDialog(Dialog dialog)
    {
        dialogBox.SetActive(true);
        dialogIsActive = true;    //flag
        nameText.text = dialog.getName();
        sentences.Clear();

        foreach (string sentence in dialog.getSentences())
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        //testing
        if (sentences.Count > 0)
            Debug.Log(sentences.Peek());

        if (sentences.Count == 0)
        {
            EndDialog();
            Debug.Log("End of conversation");
            return;
        }

        string sentence = sentences.Dequeue();
        dialogText.text = sentence;
        

    }

    public void EndDialog()
    {
        sentences.Clear();
        
        dialogBox.SetActive(false);
        dialogIsActive = false;     //flag
    }

    public bool DialogIsActive()
    {
        return dialogIsActive;
    }
}
