using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordGameManager : DateBase // Samantha Zavaleta
{
    [SerializeField] List<string> peterFavs;
    [SerializeField] List<string> petraFavs;

    [SerializeField] GameObject game;
    [SerializeField] Text pointCounter;
    int correct = 0;

    int currIndex = 0;
    [SerializeField] List<GameObject> rounds;
    [SerializeField] float heartPerCorrect = 2;

    [SerializeField] int PetraEndGood;
    [SerializeField] int PeterEndGood;
    [SerializeField] int PetraEndBad;
    [SerializeField] int PeterEndBad;

    [SerializeField] int goodBoudary = 3;
    bool gameOver = false;
    // vars for how many hearts to add based on correct/5
    void Start()
    {
        currIndex = 0;
        correct = 0;
        pointCounter.text = correct + "";
        gameOver = false;
        rounds[currIndex].SetActive(true);
    }

    public void SendName(string name)
    {
        if (gameOver) return;

        if(player.heartSubject == player.peter)
        {
            if (CheckPeter(name)) // if it matches
            {
                player.ChangeHearts(heartPerCorrect);
                correct++;
                pointCounter.text = correct + "";
            }
        }
        else if (player.heartSubject == player.petra)
        {
            if (CheckPetra(name)) // if it matches
            {
                player.ChangeHearts(heartPerCorrect);
                correct++;
                pointCounter.text = correct + "";
            }
        }
        rounds[currIndex].SetActive(false);
        currIndex++;
        if(currIndex >= rounds.Count)
        {
            gameOver = true;

            game.SetActive(false);
            if (player.heartSubject == player.peter)
            {
                if(correct >= goodBoudary)
                    GameManagerScript.instance.currDiaManager.StartConversation(PeterEndGood);
                else
                    GameManagerScript.instance.currDiaManager.StartConversation(PeterEndBad);
            }
            else if (player.heartSubject == player.petra)
            {
                if (correct >= goodBoudary)
                    GameManagerScript.instance.currDiaManager.StartConversation(PetraEndGood);
                else
                    GameManagerScript.instance.currDiaManager.StartConversation(PetraEndBad);
            }
        }
        else
        {
            rounds[currIndex].SetActive(true);
        }
    }

    bool CheckPeter(string name)
    {
        foreach (string title in peterFavs)
        {
            if (title.Equals(name))
            {
                return true;
            }
        }
        return false;
    }
    bool CheckPetra(string name)
    {
        foreach(string title in petraFavs)
        {
            if (title.Equals(name))
            {
                return true;
            }
        }
        return false;
    }
}
