using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour // Joyce Mai
{
    [SerializeField] GameObject nameBox; // allows us to enable and disable the name for narration
    [SerializeField] Text nameText;
    [SerializeField] Text dialogueText;

    [SerializeField] Animator anim; // used for the popup.drop down of text box

    Queue<string> sentences; // queues up current dialogue
    int currSIndex; //current sprite index
    Dialogue currDialogue; // current Dialogue object
    

    [SerializeField] float secondsBetweenText = .1f;

    [SerializeField] Conversation openConvo;
    Conversation currConvo;
    public List<Conversation> convoList;

    GameObject currCharacter;
    Animator currAnim; // the current speaker's animator
    SpriteRenderer currRender; // the current speaker's sprite renderer


    [SerializeField] string nameInsertDetect = "@";

    void Awake()
    {
        SetDialogueManager();
        currConvo = openConvo;
        currSIndex = 0;
        sentences = new Queue<string>();
        if (!GameManagerScript.instance.returning)
            StartConversation();
    }
    public void StartDialogue(Dialogue dialogue) //starts the dialogue box popup // reqires trigger
    {
        currDialogue = dialogue;
        resetCharacter();
        if (currDialogue.character != null)
        {
            currCharacter = currDialogue.character;
            currAnim = currCharacter.GetComponent<Animator>();
            currRender = currCharacter.GetComponent<SpriteRenderer>();
        }

        anim.SetBool("isOpen", true);

        if (currDialogue.name.Equals(""))
        {
            nameBox.SetActive(false);
        }
        else
        {
            nameBox.SetActive(true);
            nameText.text = CheckNameInsert(currDialogue.name);
        }
        sentences.Clear();

        currSIndex = 0;

        
        for(int i = 0; i < currDialogue.myDS.sentences.Length; i++)
        {
            string sentence = CheckNameInsert(currDialogue.myDS.sentences[i]);
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence() // moves the text along // also calls the sprite switcher
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return; 
        }
        if(!currDialogue.narration) 
            LoadNextVisual();

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        if(currAnim != null)
            currAnim.SetTrigger("Start");
    }
    IEnumerator TypeSentence (string sentence) // creates the typing effect // TO DO: implement a text multiplier via settings to control the speed of text 
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(secondsBetweenText/GameManagerScript.instance.textSpeedMult);
        }
    }
    void LoadNextVisual() // loads the next sprite in the dialogue order if applicable
    {
        if (currCharacter == null) return;
        if (currSIndex >= currDialogue.myDS.spriteList.Count || currSIndex >= currDialogue.myDS.placements.Count) return;

        if(currDialogue.myDS.spriteList[currSIndex] != null)
        {
            currRender.sprite = currDialogue.myDS.spriteList[currSIndex];
        }
        if(currDialogue.myDS.placements[currSIndex] != null)
        {
            currCharacter.transform.position = currDialogue.myDS.placements[currSIndex].postion;
        }
        currSIndex++;
    }
    void EndDialogue() // closes text box
    {
        if (currConvo == null) return;
        anim.SetBool("isOpen", false);
        if (currCharacter) // if theres something to render visually, do it
        {
            if (currDialogue.myDS.postTextPos != null)
            {
                currCharacter.transform.position = currDialogue.myDS.postTextPos.postion;
            }
            if (currDialogue.myDS.postTextSprite != null)
            {
                currRender.sprite = currDialogue.myDS.postTextSprite;
            }
        }
        // check if there's more dialogue to be spoken
        if (currConvo.MoreDialogue())
        {
            currConvo.TriggerDialogue();
        }
        else // if conversation is over, reset
        {
            currConvo.ResetCurrIndex();
            if (currConvo.useFunction != null)
            {
                currConvo.useFunction.Invoke();
            }
            currConvo = null;
            resetCharacter();
        }
    }

    public void StartConversation(int diaIndex = -1)
    {
        resetCharacter();
        if (diaIndex >= 0)
        {
            currConvo = GameManagerScript.instance.currDiaManager.convoList[diaIndex];
        }
        if (currConvo == null) return;
        currConvo.TriggerDialogue();
        currConvo.SetBG();
    }

    public void StartThisConversation(Conversation convo)
    {
        currConvo = convo;
        currConvo.TriggerDialogue();
        currConvo.SetBG();
    }

    void SetDialogueManager()
    {
        GameManagerScript.instance.SetCurrDialogue(this);
    }
    string CheckNameInsert(string sentence) // call this for every sentence to check if a given sentence has a name inupt
    {
        if (sentence.Contains(nameInsertDetect))
        {
            sentence = sentence.Replace(nameInsertDetect, GameManagerScript.instance.player.playerName);
        }
        return sentence;
    }
    void resetCharacter()
    {
        currAnim = null;
        currCharacter = null;
        currRender = null;
    }
}
