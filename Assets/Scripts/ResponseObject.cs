using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResponseObject : MonoBehaviour // Joyce Mai
{
    Button myButton;
    Text text;
    GameObject canvas;
    Animator anim;
    [SerializeField] Response myResponse;

    PlayerScript player;

    void Awake()
    {
        myButton = GetComponent<Button>();
        myButton.onClick.AddListener(OnClick);
        text = GetComponentInChildren<Text>();
        canvas = GetComponentInParent<Canvas>().gameObject;
        anim = GetComponentInParent<Animator>();

        text.text = myResponse.option;

        player = GameManagerScript.instance.player;
    }
    private void OnEnable()
    {
        anim.SetBool("isOpen", true);
    }
    void OnClick()
    {
        Response.Subject target = myResponse.target;
        if (myResponse.responseType == Response.ResponseType.TARGET) // if the button is to target // target triggers points and potentially a conversation
        {
            if (myResponse.targetType == Response.TargetType.DATE) // use this type to set up date selection
            {
                if (target == Response.Subject.PETER) // set peter's status to love
                {
                    player.heartSubject = player.peter;
                    player.peter.relationship = Character.Status.LOVE;
                    player.petra.relationship = Character.Status.FRIEND;
                }
                else if (target == Response.Subject.PETRA) // set petra to love status
                {
                    player.heartSubject = player.petra;
                    player.peter.relationship = Character.Status.FRIEND;
                    player.petra.relationship = Character.Status.LOVE;
                }
            }
            else if(myResponse.targetType == Response.TargetType.FRIEND) // use this to set up hang out selection
            {
                if (target == Response.Subject.PETER) // set peter's status to love
                {
                    player.heartSubject = player.peter;
                    player.peter.relationship = Character.Status.FRIEND;
                    player.petra.relationship = Character.Status.FRIEND;
                }
                else if (target == Response.Subject.PETRA) // set petra to love status
                {
                    player.heartSubject = player.petra;
                    player.peter.relationship = Character.Status.FRIEND;
                    player.petra.relationship = Character.Status.FRIEND;
                }
            }

            if(target == Response.Subject.NULL && myResponse.pointDelta != 0) // if the button was meant to change hearts but doesnt have a target, change both
            {
                player.UntargetedHearts(myResponse.pointDelta);
            }
            else if(myResponse.pointDelta != 0)
            {
                player.ChangeHearts(myResponse.pointDelta);
            }

            if(myResponse.convoIndex >= 0) // if valid index of conversation, trigger it
            {
                GameManagerScript.instance.currDiaManager.StartConversation(myResponse.convoIndex);
            }
        }
        else if (myResponse.responseType == Response.ResponseType.CONVO)
        {
            if (myResponse.convoIndex >= 0)
            {
                GameManagerScript.instance.currDiaManager.StartConversation(myResponse.convoIndex);
            }
        }
        else
        {
            if (target != Response.Subject.NULL) // if you set a target to the scene
            {
                if (myResponse.targetType == Response.TargetType.DATE) // use this type to set up date selection
                {
                    if (target == Response.Subject.PETER) // set peter's status to love
                    {
                        player.heartSubject = player.peter;
                        player.peter.relationship = Character.Status.LOVE;
                        player.petra.relationship = Character.Status.FRIEND;
                    }
                    else if (target == Response.Subject.PETRA) // set petra to love status
                    {
                        player.heartSubject = player.petra;
                        player.peter.relationship = Character.Status.FRIEND;
                        player.petra.relationship = Character.Status.LOVE;
                    }
                }
                else if (myResponse.targetType == Response.TargetType.FRIEND) // use this to set up hang out selection
                {
                    if (target == Response.Subject.PETER) // set peter's status to love
                    {
                        player.heartSubject = player.peter;
                        player.peter.relationship = Character.Status.FRIEND;
                        player.petra.relationship = Character.Status.FRIEND;
                    }
                    else if (target == Response.Subject.PETRA) // set petra to love status
                    {
                        player.heartSubject = player.petra;
                        player.peter.relationship = Character.Status.FRIEND;
                        player.petra.relationship = Character.Status.FRIEND;
                    }
                }
            }
            
            SceneManagerScript.LoadScene(myResponse.toScene);
        }
        anim.SetBool("isOpen", false);
        canvas.SetActive(false);
    }
    
}
