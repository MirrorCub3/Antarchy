using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// make sure this is the hardcoded stuff, it's not changeble on build
[CreateAssetMenu]
public class GameSettings : ScriptableObject // Joyce Mai
{
    [Tooltip("what to spawn if null player in scene")]
    public GameObject playerPrefab;
    public int maxCharacters = 13;

    [Tooltip("how much the character's hearts go down per day")]
    public float dailyDrop = -5;
    [Tooltip("gain hearts faster by going on dates")]
    public float dateMult = 2f; //gain hearts faster by dating rather than hanging out

    [Tooltip("strings to look for in scene to set game state")]
    public string textDetect = "Text"; // the string to look for in the scene with dialogue
    public string dayDetect = "Day"; // the string to look for in the scene with dialogue
    public string gameDetect = "Game"; // the string to look for in the game scenes

    [Tooltip("List of Days: will compare this to the saved number to find the saved level")]
    public List<string> Days;
    public string breakEnd;

    // break up endings
    public int petraBreak = 0;
    public int peterBreak = 1;

    // good endings 
    public int petraLove = 2;
    public int peterLove = 3;
    public int friends = 4;

    public float maxVol = 100;
    public float minVol = 0;

    public float maxMult = 10;
    public float minMult = 1;

    public int LastIndex()
    {
        return (Days.Count - 1);
    }
}
