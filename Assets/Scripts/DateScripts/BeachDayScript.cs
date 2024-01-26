using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeachDayScript : DateBase // Lyndsey Narvaez
{
    [SerializeField] string trigger = "Up";
    [SerializeField] float waitForSeconds = 1f;
    [SerializeField] float startTime = 30; //  in seconds
    [SerializeField] Text counter;

    public float heartsForItem = 1;

    bool isPlaying;
    float currTime;

    public string food = "Food";
    public string drink = "Drink";
    public string petra = "Petra";
    public string peter = "Peter";

    void Start()
    {
        currTime = startTime;
        counter.text = currTime + "";
        isPlaying = true;
        StartCoroutine(CountDown());
    }
    public void TriggerAnim(Animator anim)
    {
        anim.SetTrigger(trigger);
    }
    IEnumerator CountDown()
    {
        while (isPlaying)
        {
            currTime--;
            counter.text = currTime + "";
            if (currTime <= 0)
            {
                isPlaying = false;
            }
            yield return new WaitForSecondsRealtime(waitForSeconds);
        }
        DateOver();
    }
}
