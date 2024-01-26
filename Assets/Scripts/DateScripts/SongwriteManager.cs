using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SongwriteManager : DateBase // Joyce Mai
{
    [SerializeField] Animator pageAnim;
    [SerializeField] Transform peter;
    [SerializeField] Transform petra;
    [SerializeField] Position hideHere;

    public InputField titleInput;
    [SerializeField] List<InputField> inputs;
    [SerializeField] int maxCharacterCount;
    public string[] text;
    [SerializeField] int blankCheck;// index for the dialogue response to blanks
    [SerializeField] int sureCheck; // index for the dialogue response on finish

    int startChorus = 2;
    int endChorus = 4;
    public string[] chorusText = new string[3];

    void Awake()
    {
        if (SaveSystem.LoadSong() != null) // if there is saved game data somewhere
        {
            LoadSong();
        }
        else
        {
            SaveSong();
        }
        foreach (InputField input in inputs)
        {
            input.characterLimit = maxCharacterCount;
        }
        text = new string[inputs.Count];
    }
    void LoadSong()
    {
        SongData data = SaveSystem.LoadSong();
        titleInput.text = data.title;

        text = data.text;
        for (int i = 0; i < text.Length; i++) // put the existing text into the inputs
        {
            inputs[i].text = text[i];
        }
    }
    public void CheckBlanks()
    {
        bool empty = false;
        foreach (InputField input in inputs)
        {
            if (string.IsNullOrWhiteSpace(input.text)) // if empty, give are you sure warning
            {
                empty = true;
            }
        }
        if(string.IsNullOrWhiteSpace(titleInput.text))
        {
            empty = true;
        }
        if (!empty)
        {
            GameManagerScript.instance.currDiaManager.StartConversation(sureCheck);
        }
        else
        {
            GameManagerScript.instance.currDiaManager.StartConversation(blankCheck);
        }
    }
    public void SaveSong()
    {
        text = new string[inputs.Count];
        for (int i = 0; i < text.Length; i++) // saving the 
        {
            text[i] = inputs[i].text;
            if(i >= startChorus && i <= endChorus)
            {
                chorusText[i - startChorus] = inputs[i].text;
            }
        }
        SaveSystem.SaveSong(this);
        isPlaying(false);
    }
    public void isPlaying(bool playing)
    {
        pageAnim.SetBool("Playing", playing);
        if (playing)
        {
            peter.position = hideHere.postion;
            petra.position = hideHere.postion;
        }
    }
}
