using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public CanvasGroup group;

    public void Play()
    {
        group.interactable = false;
        int nextLevel = 1;
        if (PlayerPrefs.HasKey("CompletedLevel"))
        {
            nextLevel = PlayerPrefs.GetInt("CompletedLevel") + 1;
        }
        GameManager.instance.LoadLevel(nextLevel, false);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting");
        Application.Quit();
    }

    public void PlayAgain()
    {
        PlayerPrefs.SetInt("TutorialComplete", 0);
        PlayerPrefs.SetInt("CompletedLevel", 0);
        Play();
    }
}
