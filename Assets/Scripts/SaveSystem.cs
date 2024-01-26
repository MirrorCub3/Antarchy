using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public static class SaveSystem // Joyce Mai
{
    static string playerPath = "/player.cube";
    static string gamePath = "/game.cube";
    static string songPath = "/song.cube";
    public static void SavePlayer (PlayerScript player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + playerPath;
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player);

        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + playerPath;
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return (data);
        }
        else
        {
            Debug.LogError("no player save file in " + path);
            return null;
        }
    }
    public static void SaveGame(GameManagerScript game)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + gamePath;
        FileStream stream = new FileStream(path, FileMode.Create);

        GameData data = new GameData(game);

        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static GameData LoadGame()
    {
        string path = Application.persistentDataPath + gamePath;
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GameData data = formatter.Deserialize(stream) as GameData;
            stream.Close();

            return (data);
        }
        else
        {
            Debug.LogError("no game save file in " + path);
            return null;
        }
    }
    public static void SaveSong(SongwriteManager song)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + songPath;
        FileStream stream = new FileStream(path, FileMode.Create);
        Debug.Log(path);
        SongData data = new SongData(song);

        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static SongData LoadSong()
    {
        string path = Application.persistentDataPath + songPath;
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SongData data = formatter.Deserialize(stream) as SongData;
            stream.Close();

            return (data);
        }
        else
        {
            Debug.LogError("no song save file in " + path);
            return null;
        }
    }
    public static void ClearSong()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + songPath;
        File.Delete(path);
    }


}
