using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManagerScript : MonoBehaviour // Joyce Mai
{
    public static GameManagerScript instance;

    public DialogueManager currDiaManager;
    public GameObject currTextOpener;
    public PlayerScript player;

    public GameSettings settings;
    public float textSpeedMult = 1; // text speed multiplier
    public float volume = 100;

    public int dayCount = 0;
    int outings;
    int dailyOutings = 2;
    public bool returning = false;
    public bool dayOver = false;

    public bool Peter { get; private set; }
    public bool Petra { get; private set; }

    public int endingNum;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(this);
            return;
        }
        player = FindObjectOfType<PlayerScript>();
        if (player == null)
        {
            player = Instantiate(settings.playerPrefab).GetComponent<PlayerScript>();
        }

        if (SaveSystem.LoadGame() != null) // if there is saved game data somewhere
        {
            LoadGame();
        }
        else
        {
            SaveGame();
        }
        OnNewScene();
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && !EscMenu.EscOpen && currDiaManager) // handles the continuation of text // don't count clicks when Esc is open
        {
            currDiaManager.DisplayNextSentence();
        }
    }
    public void NewGame() // reset function
    {
        dayCount = 0;
        SaveGame();
        player.Restart();
        SaveSystem.ClearSong();
    }
    public void LoadGame() // for player saves, not holding the game data
    {
        GameData data = SaveSystem.LoadGame();
        textSpeedMult = data.textSpeedMult;
        volume = data.volume;

        dayCount = data.dayCount;

        Peter = data.Peter;
        Petra = data.Petra;
    }
    public void LoadSavedGame()
    {
        GameData data = SaveSystem.LoadGame();
        dayCount = data.dayCount;

        player.LoadPlayer();

        SceneManagerScript.LoadScene(data.currDayScene);
    }
    void SaveGame()
    {
        SaveSystem.SaveGame(instance);
    }
    public void SetCurrDialogue(DialogueManager manager)
    {
        currDiaManager = manager;
        player.SetSliderActive(true); // if there's dialogue, show the heart status
    }
    public void SetCurrOpener(TextOpener opener)
    {
        currTextOpener = opener.gameObject;
        if (returning)
        {
            currTextOpener.GetComponent<TextOpener>().enabled = true;
        }
    }
    public void OnNewScene()
    {
        string currScene = SceneManager.GetActiveScene().name;

        if (currScene.Contains(settings.textDetect))
        {
            player.ResetStatus(); // reset the realtionship type after every day and date return
            int currday = ReturnSceneIndex(currScene) + 1;
            if (currday > dayCount) // only take away hearts if they player is entering a new day
            {
                outings = 0;
                returning = false;
                if (dayCount > 0) // drop hearts daily on every day following day
                {
                    player.DailyDrop(settings.dailyDrop);
                }
                player.SavePlayer(); // only save the player data at the start of a new day
            }
            else if (currday == dayCount) // if returning to the same day, then prompt the specific conversation
            {
                if (returning) // have the other date mamagers set this to true if the hangout count hasn't been satisfied yet
                {
                    currTextOpener.GetComponent<TextOpener>().enabled = true;
                    returning = false;
                }
                else
                {
                    outings = 0;
                }
            }
            dayCount = currday;// figuring out what day it is via the number in the scene name
        }
        else if (currScene.Contains(settings.gameDetect))
        {
            player.SetSliderActive(false);
        }
        else if (currDiaManager == null)
        {
            returning = false;
            player.SetSliderActive(false);
        }

        if (ReturnSceneIndex(currScene) == settings.LastIndex() && !player.FullHearts())
        {
            print("Friends");
            PlayerPrefs.SetInt("Ending", settings.friends);
        }
        SaveGame();
    }
    int ReturnSceneIndex(string currScene)
    {
        int index = 0;
        foreach(string name in settings.Days)
        {
            if (currScene.Equals(name))
            {
                return (index);
            }
            index++;
        }
        return (-1); // not found
    }

    public void GameOver()
    {
        if (player.heartSubject == player.peter)
        {
            print("Peter break");
            PlayerPrefs.SetInt("Ending", settings.peterBreak);
        }
        else if (player.heartSubject == player.petra)
        {
            print("Petra break");
            PlayerPrefs.SetInt("Ending", settings.petraBreak);
        }
        SceneManagerScript.LoadScene(settings.breakEnd);
        SaveGame();
    }
    public void Ending(bool fullHeart = false, Character character = null)
    {
        if (fullHeart) // if ending is reached by getting to full hearts with a character
        {
            if (character == player.peter)
            {
                print("Peter ending");
                PlayerPrefs.SetInt("Ending", settings.peterLove);
                Peter = true;
            }
            else if (character == player.petra)
            {
                print("Petra ending");
                PlayerPrefs.SetInt("Ending", settings.petraLove);
                Petra = true;
            }
        }
        SceneManagerScript.LoadScene(settings.Days[settings.LastIndex()]);
        SaveGame();
    }
    public bool AddOuting(int time = 1)
    {
        outings += time;
        if (outings >= dailyOutings)
        {
            dayOver = true;
            SceneManagerScript.LoadScene(settings.Days[dayCount]);
            outings = 0;
            return true;
        }
        return false;
    }
    public void ChangeVolume(float volumeDelta)
    {
        AudioListener.volume = volumeDelta/settings.maxVol;
        volume = volumeDelta;
    }
    private void OnDestroy() // destroy this game object while the script is on it's way out
    {
        Destroy(this.gameObject);
    }
}
