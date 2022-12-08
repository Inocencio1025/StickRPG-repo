using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialog 
{
    private string name;

    [TextArea(3, 10)]
    private string[] sentences;

    //constructors
    //for singular statements (without name)
    public Dialog(string sentence)
    {
        sentences = new string[1];
        this.name = "";
        this.sentences[0] = sentence;
    }

    //for singular statements (with name)
    public Dialog(string name, string sentence)
    {
        sentences = new string[1];
        this.name = name;
        this.sentences[0] = sentence;
    }

    //for full dialog (without name)
    public Dialog(string[] sentence)
    {
        
        this.name = "";
        this.sentences = sentence;
    }

    // for full dialog, (with name)
    public Dialog(string name, string[] sentences)
    {
        this.name = name;
        this.sentences = sentences;
    }



    //setters
    public void setName(string name)
    {
        this.name = name;
    }

    public void setSentences(string[] sentences)
    {
        this.sentences = sentences;
    }

    //getters
    public string getName()
    {
        return name;
    }

    public string[] getSentences()
    {
        return sentences;
    }
}
