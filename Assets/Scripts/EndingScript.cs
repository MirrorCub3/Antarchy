using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingScript : MonoBehaviour // Joyce Mai
{
    int ending;
    void Awake()
    {
        ending = PlayerPrefs.GetInt("Ending");
        print(ending);
        GameManagerScript.instance.currDiaManager.StartConversation(ending);
    }
}
