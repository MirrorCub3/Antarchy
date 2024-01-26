using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XYMoveScript : MonoBehaviour // Joyce Mai
{
    [SerializeField] float speed = 5f;
    float xtrans;
    float ytrans;
    [SerializeField] Animator anim;

    static BeachDayScript manager;

    bool hasBurger;
    bool hasDrink;

    void Start()
    {
        anim = this.GetComponent<Animator>();
        manager = FindObjectOfType<BeachDayScript>();
    }

    void Update()
    {
        xtrans = Input.GetAxis("Horizontal") * speed;
        ytrans = Input.GetAxis("Vertical") * speed;

        float moveMag = new Vector2(xtrans, ytrans).magnitude; // calculates if there's movement
        anim.SetFloat("Speed", moveMag);// switiching animation states if in motion
    }
    private void FixedUpdate()
    {
        transform.Translate(xtrans * Time.fixedDeltaTime, ytrans * Time.fixedDeltaTime, 0); // makes collsions smoother
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string name = collision.gameObject.name;

        Animator anim = collision.gameObject.GetComponent<Animator>();
        if(anim != null && (GetItem(name) || GiveItem(name) ))
        {
            manager.TriggerAnim(anim);
        }
    }
    bool GetItem(string name)
    {
        if (name.Contains(manager.food))
        {
            hasBurger = true;
            return true;
        }
        else if (name.Contains(manager.drink))
        {
            hasDrink = true;
            return true;
        }
        return false;
    }
    bool GiveItem(string name)
    {
        if (name.Contains(manager.peter))
        {
            if (hasBurger)
            {
                hasBurger = false;
                GameManagerScript.instance.player.SetPeterHearts(manager.heartsForItem);
                return true;
            }
        }
        else if (name.Contains(manager.petra))
        {
            if (hasDrink)
            {
                hasDrink = false;
                GameManagerScript.instance.player.SetPetraHearts(manager.heartsForItem);
                return true;
            }
        }
        return false;
    }
}
