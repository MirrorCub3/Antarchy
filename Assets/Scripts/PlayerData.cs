using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float petraHearts;
    public float peterHearts;

    public string playerName;
    public PlayerData(PlayerScript player)
    {
        petraHearts = player.PetraHearts.value;
        peterHearts = player.PeterHearts.value; 
        playerName = player.playerName;
    }
}
