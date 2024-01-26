using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMScript : MonoBehaviour // Joyce Mai
{
    public static BGMScript instance;
    [SerializeField] Clip[] BGMs;
    [SerializeField] AudioSource source;
    Scene currScene;

    private void Awake()
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
        currScene = SceneManager.GetActiveScene();
    }
    private void Start()
    {
        source = GetComponent<AudioSource>();
        StartBGM();
    }
    private void Update()
    {
        if(SceneManager.GetActiveScene() != currScene)
        {
            StartBGM();
            currScene = SceneManager.GetActiveScene();
        }
    }
    public void StartBGM()
    {
        foreach (Clip clip in BGMs) {
            if (SceneManager.GetActiveScene().name.Contains(clip.sceneName) && source.clip != clip.clip)
            {
                source.clip = clip.clip;
                source.Play();
            }
        }
    }
}

[System.Serializable]
public class Clip
{
    public AudioClip clip;
    public string sceneName;
}
