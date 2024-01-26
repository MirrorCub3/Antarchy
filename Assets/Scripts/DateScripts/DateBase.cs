using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DateBase : MonoBehaviour //Joyce Mai // have the minigames derive from this
{
    [SerializeField] protected int outingVal = 1; // the amount of outing this date counts as
    [SerializeField] protected float locBoost = 1;
    [SerializeField] Response.Subject target; // only use if thel location has a certain boost for the given charcter 
    [SerializeField] protected float generalBoost = 1;

    protected PlayerScript player;

    private void Awake()
    {
        player = GameManagerScript.instance.player;
        print("Dating " + player.heartSubject.charaName);
        print("relationship " + player.heartSubject.relationship);
        if(player.heartSubject == player.petra && target == Response.Subject.PETRA) // if the location boost is for petra, set it
        {
            player.SetLocBoost(locBoost * generalBoost);
        }
        else if (player.heartSubject == player.peter && target == Response.Subject.PETER) // if the level is peter's fav, set the boost
        {
            player.SetLocBoost(locBoost * generalBoost);
        }
        else
        {
            player.SetLocBoost(generalBoost); // set no location multiplier
        }
    }
    public void DateOver()
    {
        bool overOutings = GameManagerScript.instance.AddOuting(outingVal);
        int returnIndex = GameManagerScript.instance.dayCount - 1;
        if (!overOutings)
        {
            GameManagerScript.instance.returning = true;
            SceneManagerScript.LoadScene(GameManagerScript.instance.settings.Days[returnIndex]);
        }
    }
}
