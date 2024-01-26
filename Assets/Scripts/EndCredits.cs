using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndCredits : MonoBehaviour// Joyce Mai
{
    [SerializeField] Text title;
    [SerializeField] List<Text> lyrics;
    [SerializeField] Transform credits;

    [SerializeField] float creditRollTime;

    [SerializeField] Image display;
    [SerializeField] Sprite Petra;
    [SerializeField] Sprite Peter;

    string detectBlank = "@"; // look for this to fill in the blank


    SongData data;
    int ending;
    int goodEndStartID = 2;
    void Start()
    {
        ending = PlayerPrefs.GetInt("Ending");
        data = SaveSystem.LoadSong();
        title.text = data.title;
        ReplaceLyrics();
        if(ending >= goodEndStartID)
        {
            if (ending == GameManagerScript.instance.settings.petraLove)
            {
                display.sprite = Petra;
            }
            else if (ending == GameManagerScript.instance.settings.peterLove)
            {
                display.sprite = Peter;
            }
        }
        else
        {
            display.enabled = false;
        }
        StartCoroutine(CloseCredit());
    }
    void ReplaceLyrics()
    {
        int index = 0;
        foreach(Text line in lyrics)
        {
            if (line.text.Contains(detectBlank) && index < data.text.Length)
            {
                line.text = line.text.Replace(detectBlank, data.text[index]);
                index++;
            }
        }
        index = 0;
        foreach (Text line in lyrics) // filling in the repeating chorus
        {
            if (line.text.Contains(detectBlank) && index < data.chorus.Length)
            {
                line.text = line.text.Replace(detectBlank, data.chorus[index]);
                index++;
            }
        }
    }
    IEnumerator CloseCredit()
    {
        yield return new WaitForSecondsRealtime(creditRollTime);
        SceneManagerScript.LoadScene("MainMenu");
    }

}
