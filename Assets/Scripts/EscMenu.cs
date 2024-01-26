using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EscMenu : MonoBehaviour // Joyce Mai
{
    [SerializeField] bool isESC = true;
    public static bool EscOpen = false; // for blocking purposed

    [SerializeField] GameObject buttonMenu, settings, exitWarning; // all the screens that need to be made visible or not

    [SerializeField] Slider volSlider;
    [SerializeField] Text volNumText;

    [SerializeField] Slider speedSlider;
    [SerializeField] Text speedNumText;
    //public GameObject loader;
    private string toScene;
    void Start()
    {
        volSlider.maxValue = GameManagerScript.instance.settings.maxVol;
        volSlider.minValue = GameManagerScript.instance.settings.minVol;
        volSlider.value = GameManagerScript.instance.volume;//TO DO: implement a player pref here instead
        OnChangeVolume();

        speedSlider.maxValue = GameManagerScript.instance.settings.maxMult;
        speedSlider.minValue = GameManagerScript.instance.settings.minMult;
        speedSlider.value = GameManagerScript.instance.textSpeedMult; //TO DO: implement a player pref here instead
        OnChangeTextMult();

        EscOpen = false;

        buttonMenu.SetActive(false);
        settings.SetActive(false);
        exitWarning.SetActive(false);

        //loader = GameObject.FindGameObjectWithTag("Loader");
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isESC)
        {
            EscOpen = !EscOpen;
            if (EscOpen)
            {
                buttonMenu.SetActive(true);
                settings.SetActive(false);
                exitWarning.SetActive(false);

                Time.timeScale = 0;
            }
            else
            {
                buttonMenu.SetActive(false);
                settings.SetActive(false);
                exitWarning.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }
    public void OpenExit(string goTo)
    {
        toScene = goTo;
        exitWarning.SetActive(true);
    }
    public void CloseExit()
    {
        exitWarning.SetActive(false);
    }
    public void OpenSettings()
    {
        settings.SetActive(true);
    }
    public void SaveSettings() // save changes
    {
        GameManagerScript.instance.textSpeedMult = speedSlider.value;
        GameManagerScript.instance.ChangeVolume(volSlider.value);
        settings.SetActive(false);
    }
    public void CloseSettings() // just close
    {
        speedSlider.value = GameManagerScript.instance.textSpeedMult;
        volSlider.value = GameManagerScript.instance.volume;
        AudioListener.volume = volSlider.normalizedValue;

        settings.SetActive(false);
    }
    public void Leave()
    {
        Time.timeScale = 1;
        SceneManagerScript.LoadScene(toScene);
    }

    public void OnChangeVolume()
    {
        volNumText.text = (volSlider.value + "");
        AudioListener.volume = volSlider.normalizedValue;
    }

    public void OnChangeTextMult()
    {
        speedNumText.text = (speedSlider.value + "x");
    }
}
