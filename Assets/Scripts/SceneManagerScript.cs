using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagerScript : MonoBehaviour // Joyce Mai
{
    [SerializeField] Animator transition;
    [SerializeField] float transitionTime;
    [SerializeField] float displayTextTime;

    [SerializeField] Text transitionText;
    [SerializeField] Text dayCounter;
    [SerializeField] string daysText = "DAYS UNTIL CONCERT  ";
    [SerializeField] string dayText = "DAY UNTIL CONCERT    ";
    int changeDayText = 1;
    [SerializeField] GameObject savingIcon;

    static SceneManagerScript instance;
    private void Awake()
    {
        instance = this;
        GameManagerScript.instance.OnNewScene(); // one scene manager prefab per scene, this will call game manager to know it's a new scene 
        instance.transitionText.enabled = false;
        instance.dayCounter.enabled = false;
        savingIcon.SetActive(false);
    }
    public static void LoadScene(string goTo) // basic scene loading
    {
        if (goTo.Equals("Quit"))
        {
            Application.Quit();
        }
        else
        {
            instance.StartCoroutine(LoadNextScene(goTo));
        }
    }

    static IEnumerator LoadNextScene(string goTo)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(goTo);
        operation.allowSceneActivation = false;
        instance.transition.SetTrigger("Close");
        yield return new WaitForSeconds(instance.transitionTime);
        if (goTo.Contains(GameManagerScript.instance.settings.dayDetect) && !GameManagerScript.instance.returning) // if loading a Day scene and is not currently in return mode 
        {
            instance.StartCoroutine(DisplayText(goTo, operation));
        }
        else
        {
            operation.allowSceneActivation = true;
        }

    }
    static IEnumerator DisplayText(string goTo , AsyncOperation currOperation)
    {
        instance.transitionText.enabled = true;
        instance.dayCounter.enabled = true;
        instance.savingIcon.SetActive(true);
        float days = GameManagerScript.instance.settings.Days.Count - int.Parse(goTo.Substring(GameManagerScript.instance.settings.dayDetect.Length, 1));
        if (days != instance.changeDayText)
        {
            instance.transitionText.text = instance.daysText;
        }
        else
        {
            instance.transitionText.text = instance.dayText;
        }
        instance.dayCounter.text = "" + (days); ; // 7  days - the current amount of days completed
        yield return new WaitForSeconds(instance.displayTextTime);
        currOperation.allowSceneActivation = true;
    }
}
