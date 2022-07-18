using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogManager : MonoBehaviour
{
    public GameManager manager;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogText;
    private Queue<string> sentences;

    bool ChoiceTime = false;
    private void Start()
    {
        sentences = new Queue<string>();

    }

    public void StartDialog(Dialog dialog)
    {
        manager.dialogBox.SetActive(true);
        nameText.text = dialog.name;

        sentences.Clear();

        foreach (string sentence in dialog.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count > 0)
            Debug.Log(sentences.Peek());

        if (sentences.Count == 0)
        {
            EndDialog();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogText.text = sentence;


    }



    void EndDialog()
    {
        Debug.Log("End of conversation");
        manager.dialogBox.SetActive(false);
    }
}
