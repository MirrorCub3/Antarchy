using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Response : ScriptableObject // Joyce Mai
{
    // more so for us than the game // TEMP remove this is final build
    [SerializeField] string day;
    [SerializeField] string choiceOption;

    /* there's 3 types of responses:
     Target: pick a character to set as the heart subject 
     Convo: Trigger a conversation
     Scene: Load a new Scene
    */
    public enum ResponseType { TARGET, CONVO, SCENE }
    [Tooltip("TARGET: use for setting date/ hangout option" +
            " CONVO: will trigger a branched conversation use for text heart damage. Will set the heartSubject to the dialogue targeted player." +
            " SCENE: will load a new scene")]
    public ResponseType responseType;

    // target variables
    public enum Subject { NULL, PETER, PETRA}
    [Tooltip("Date or Hang Out charcter selection")]
    public Subject target; // set who the target is
    public enum TargetType {DATE, FRIEND} // it the type is target, put in the type: either date or friend
    [Tooltip("Date or Hang Out selection")]
    public TargetType targetType = TargetType.DATE;

    [Tooltip("index of the convo to load. if valid can be triggered by target as well")]
    //convo variable
    public int convoIndex = -1; // selects a certain array index from the current dialogue manager
    [Tooltip("how much the response changes the hearts (always added)")]
    public int pointDelta; // how much the the points change based on the dialogue option selected // will be ADDED to the current amount

    [Tooltip("scene name to load")]
    //scene variable
    public string toScene; // string to access and set up in scenemanager

    [Tooltip("text shown for the option")]
    [TextArea] public string option;
}
