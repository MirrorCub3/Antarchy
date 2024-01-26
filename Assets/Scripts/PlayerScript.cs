using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour // Joyce Mai
{
    public static PlayerScript instance;

    [SerializeField] float maxHearts = 50;

    [SerializeField] GameObject sliderCanvas;

    public Character petra;
    public Slider PetraHearts;
    [SerializeField] Image petraFill;

    public Character peter;
    public Slider PeterHearts;
    [SerializeField] Image peterFill;

    public Character heartSubject; // will be the pointer to the current character adding/taking hearts
    public string playerName;

    float locBoost = 1;

    void Awake() // called once for when the obj is made // load player info
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(this);
            return; // tp prevent other awakes from running
        }

        // peter's heart setup
        PeterHearts.maxValue = maxHearts; // TO DO: also load these up from saves
        PeterHearts.value = maxHearts /2; // start in the middle b/c they're all friends
        peterFill.color = peter.gradient.Evaluate(PeterHearts.normalizedValue);

        // petra's heart setup
        PetraHearts.maxValue = maxHearts;
        PetraHearts.value = maxHearts /2;
        petraFill.color = petra.gradient.Evaluate(PetraHearts.normalizedValue);
    }
    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        PeterHearts.value = data.peterHearts;
        PetraHearts.value = data.petraHearts;

        playerName = data.playerName;
    }
    public void SavePlayer()
    {
        SaveSystem.SavePlayer(instance);
    }
    public void Restart()
    {
        PeterHearts.value = maxHearts / 2; // start in the middle b/c they're all friends
        peterFill.color = peter.gradient.Evaluate(PeterHearts.normalizedValue);

        PetraHearts.value = maxHearts / 2;
        petraFill.color = petra.gradient.Evaluate(PetraHearts.normalizedValue);
        heartSubject = null;
        petra.relationship = Character.Status.FRIEND;
        peter.relationship = Character.Status.FRIEND;
        SavePlayer();
    }
    public void ResetStatus()
    {
        petra.relationship = Character.Status.FRIEND;
        peter.relationship = Character.Status.FRIEND;
        heartSubject = null;
    }
    public void ChangeHearts(float heartDelta) // location boost for certain characters
    {
        if (heartSubject != null && heartSubject.relationship == Character.Status.LOVE)
        {
            heartDelta = GameManagerScript.instance.settings.dateMult * heartDelta * locBoost; // more heart loss and gain on dates float heart;
            print("using loc boost " + locBoost);
        }

        print(heartDelta);

        if (heartSubject == peter)
        {
            SetPeterHearts(heartDelta);
        }
        else if (heartSubject == petra)
        {
            SetPetraHearts(heartDelta);
        }
    }
    public void UntargetedHearts(float heartDelta) // doesn't use the pointer, just raises both
    {
        print(heartDelta);

        SetPeterHearts(heartDelta);
        SetPetraHearts(heartDelta);
    }

    public void SetSliderActive(bool active)
    {
        sliderCanvas.SetActive(active);
    }

    public void DailyDrop(float drop)
    {
        SetPeterHearts(drop);
        SetPetraHearts(drop);
    }

    public void SetPeterHearts(float heartDelta)
    {
        float heart;
        heart = PeterHearts.value;
        heart += heartDelta;

        if (heartDelta > 0) // good response
        {
            peter.GoodResponse();
        }
        else if (heartDelta < 0) // bad response
        {
            peter.BadResponse();
        }

        if (heart <= 0)
        {
            heart = 0;
            PeterHearts.value = heart;
            peterFill.color = peter.gradient.Evaluate(PeterHearts.normalizedValue);
            GameManagerScript.instance.GameOver();
        }
        else if (heart >= PeterHearts.maxValue)
        {
            heart = maxHearts;
            PeterHearts.value = heart;
            peterFill.color = peter.gradient.Evaluate(PeterHearts.normalizedValue);
            GameManagerScript.instance.Ending(true , peter);
        }
        PeterHearts.value = heart;
        peterFill.color = peter.gradient.Evaluate(PeterHearts.normalizedValue);
    }
    public void SetPetraHearts(float heartDelta)
    {
        float heart;
        heart = PetraHearts.value;
        heart += heartDelta;

        if (heartDelta > 0) // good response
        {
            petra.GoodResponse();
        }
        else if (heartDelta < 0) // bad response
        {
            petra.BadResponse();
        }

        if (heart <= 0)
        {
            heart = 0;
            PetraHearts.value = heart;
            petraFill.color = petra.gradient.Evaluate(PetraHearts.normalizedValue);
            GameManagerScript.instance.GameOver();
        }
        else if (heart >= PetraHearts.maxValue)
        {
            heart = maxHearts;
            PetraHearts.value = heart;
            petraFill.color = petra.gradient.Evaluate(PetraHearts.normalizedValue);
            GameManagerScript.instance.Ending(true, petra);
        }
        PetraHearts.value = heart;
        petraFill.color = petra.gradient.Evaluate(PetraHearts.normalizedValue);
    }
    public Character GetHeartSubject()
    {
        return heartSubject;
    }
    public bool FullHearts()
    {
        if (PeterHearts.value >= maxHearts)
            return true;
        if (PetraHearts.value >= maxHearts)
            return true;
        return false;
    }
    public void SetLocBoost(float sceneBoost)
    {
        locBoost = sceneBoost;
    }
    private void OnDestroy()
    {
        Destroy(this.gameObject);
    }
}
