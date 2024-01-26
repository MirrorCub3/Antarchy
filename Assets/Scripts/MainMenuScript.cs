using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour // Joyce Mai
{
    [SerializeField] GameSettings settings;

    [SerializeField] Slider volSlider;
    [SerializeField] Text volNumText;

    [SerializeField] Slider speedSlider;
    [SerializeField] Text speedNumText;

    [SerializeField] GameObject options;
    [SerializeField] Button loadSave;

    [SerializeField] GameObject inputWindow;
    [SerializeField] InputField nameInput;

    #region Tokens
    [SerializeField] GameObject petraPhoto, peterPhoto;
    #endregion

    void Awake()
    {
        volSlider.maxValue = settings.maxVol;
        volSlider.minValue = settings.minVol;
        volSlider.value = GameManagerScript.instance.volume;//TO DO: implement a player pref here instead
        OnChangeVolume();

        speedSlider.maxValue = settings.maxMult;
        speedSlider.minValue = settings.minMult;
        speedSlider.value = GameManagerScript.instance.textSpeedMult; //TO DO: implement a player pref here instead
        OnChangeTextMult();

        options.SetActive(false);
        inputWindow.SetActive(false);
        nameInput = inputWindow.GetComponentInChildren<InputField>();
        nameInput.characterLimit = GameManagerScript.instance.settings.maxCharacters;

        if(GameManagerScript.instance.dayCount == 0)
        {
            loadSave.interactable = false;
        }
        else
        {
            loadSave.interactable = true;
        }

        
        petraPhoto.SetActive(false);
        peterPhoto.SetActive(false);
        

        ShowEndTokens();
    }

    void ShowEndTokens()
    {
        if (GameManagerScript.instance.Peter)
        {
            peterPhoto.SetActive(true);
        }
        if (GameManagerScript.instance.Petra)
        {
            petraPhoto.SetActive(true);
        }
    }
    public void OpenSettings()
    {
        options.SetActive(true);
    }
    public void SaveSettings() // save changes
    {
        GameManagerScript.instance.textSpeedMult = speedSlider.value;
        GameManagerScript.instance.ChangeVolume(volSlider.value);
        print(GameManagerScript.instance.volume + " " + GameManagerScript.instance.textSpeedMult);
        options.SetActive(false);
    }
    public void CloseSettings() // just close
    {
        speedSlider.value = GameManagerScript.instance.textSpeedMult;
        volSlider.value = GameManagerScript.instance.volume;
        AudioListener.volume = volSlider.normalizedValue;

        options.SetActive(false);
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

    public void OpenNameInput()
    {
        inputWindow.SetActive(true);
    }
    public void CloseNameInput()
    {
        inputWindow.SetActive(false);
    }
    public void SaveNameInput(string intro) // new game
    {
        GameManagerScript.instance.player.playerName = nameInput.text;
        GameManagerScript.instance.NewGame();
        SceneManagerScript.LoadScene(intro);
    }
    public void OnEndNameInput()
    {
        GameManagerScript.instance.player.playerName = nameInput.text;
    }
    public void LoadSave()
    {
        GameManagerScript.instance.LoadSavedGame();
    }
}
