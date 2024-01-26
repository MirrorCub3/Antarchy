using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public string currDayScene;
    public int dayCount;

    public float textSpeedMult;
    public float volume;

    // ending booleans for main menu trophies
    public bool Peter;
    public bool Petra;

    public GameData(GameManagerScript game)
    {
        if(game.dayCount > 0)
            currDayScene = game.settings.Days[game.dayCount - 1]; // game saves by the day not by scene
        else
            currDayScene = game.settings.Days[game.dayCount];

        dayCount = game.dayCount;

        textSpeedMult = game.textSpeedMult;
        volume = game.volume;

        Peter = game.Peter;
        Petra = game.Petra;
    }
}
