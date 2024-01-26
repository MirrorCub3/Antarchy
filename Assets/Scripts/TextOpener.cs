using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextOpener : MonoBehaviour // Joyce Mai
{
    [Tooltip("Will immedialty go into one of two secified options, otherwise trigger via useFunction in SetConvo()")]
    [SerializeField] bool twoOpenings = true; // will determine which one to play upon start
    [Tooltip("Will send reference to game script then disable component, will trigger dialogue upon enable")]
    [SerializeField] bool delayedStart = true; // will determine which one to play upon start
    [SerializeField] int peterOpenIndex = 0;
    [SerializeField] int petraOpenIndex = 0;
    [SerializeField] int OpenIndex = 0;

    PlayerScript player;
    Character goingWith; // the pointer for the charcatater that is curently the focus of the date
    private void Awake()
    {
        // send this isntance to the game manager if avaliable
        GameManagerScript.instance.SetCurrOpener(this);
        if (delayedStart)
        {
            this.enabled = false;
        }
    }
    void Start()
    {
        player = GameManagerScript.instance.player;
        goingWith = player.GetHeartSubject();
        if (twoOpenings)
        {
            print(goingWith);
            if (goingWith == player.peter)
                SetConvo(peterOpenIndex);
            else if (goingWith == player.petra)
                SetConvo(petraOpenIndex);
        }
        else
        {
            SetConvo(OpenIndex);
        }
    }

    public void SetConvo(int index) // allows for triggers via convo useFunction()
    {
        GameManagerScript.instance.currDiaManager.StartConversation(index);
    }


}
