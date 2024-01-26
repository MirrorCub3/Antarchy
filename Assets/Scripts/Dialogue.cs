using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue // Joyce Mai
{
    public bool narration = false;
    public string name;
    public GameObject character;

    public DialogueSequence myDS; // pointer to the loaded dialogue squence

    public Response.Subject target; // use this to set a target for the dialogue if heartDelta is applicable
}

