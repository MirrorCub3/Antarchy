using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SongData //Joyce Mai
{
    public string title; 
    public string[] text;
    public string[] chorus;
    public SongData(SongwriteManager song)
    {
        title = song.titleInput.text; 
        text = song.text;
        chorus = song.chorusText;
    }
}
