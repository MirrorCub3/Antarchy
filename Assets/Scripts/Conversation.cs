using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Conversation : MonoBehaviour // Joyce Mai
{
    [SerializeField] SpriteRenderer background;
    [SerializeField] Sprite backgroundSprite;

    public List<Dialogue> dialogues;
    private int currIndex;

    public UnityEvent useFunction;
    private void Awake()
    {
        currIndex = 0;
    }
    public void TriggerDialogue()
    {
        if (currIndex >= dialogues.Count) return;

        if(dialogues[currIndex].myDS != null)
        {
            GameManagerScript.instance.currDiaManager.StartDialogue(dialogues[currIndex]);

            Response.Subject target = dialogues[currIndex].target;
            // to set the dialogues target to the heart subject
            if (target != Response.Subject.NULL) // if there's a target to set to
            {
                if(target == Response.Subject.PETER)
                {
                    GameManagerScript.instance.player.heartSubject = GameManagerScript.instance.player.peter;
                }
                else // petra
                {
                    GameManagerScript.instance.player.heartSubject = GameManagerScript.instance.player.petra;
                }
            }
        }
        currIndex++;
    }
    public void SetBG()
    {
        if(background != null && backgroundSprite != null)
            background.sprite = backgroundSprite;
    }
    public bool MoreDialogue()
    {
        if (currIndex < dialogues.Count)
        {
            return true;
        }
        return false;
    }
    public void ResetCurrIndex()
    {
        currIndex = 0;
    }
}
