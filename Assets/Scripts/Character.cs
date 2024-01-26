using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Character // Joyce Mai
{
    [Tooltip("Just for us to know")]
    public string charaName;
    [Tooltip("The color gradient per character's heart bar")]
    public Gradient gradient;

    public enum Status { LOVE, FRIEND } // the characters can either bein in friend or date mode
    [Tooltip("Set's the character's relationship with the character")]
    public Status relationship = Status.FRIEND;

    public ParticleSystem goodResponse;
    public ParticleSystem badResponse;

    public void GoodResponse()
    {
        goodResponse.Play();
    }
    public void BadResponse()
    {
        badResponse.Play();
    }
}
