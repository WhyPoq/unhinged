using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public static Tutorial instance;
    public GameObject startPanel;
    public GameObject middlePanel;
    public GameObject breakPanel;

    public bool active = false;

    private void Awake()
    {
        instance = this;
    }

    public void StartTutorial()
    {
        startPanel.SetActive(true);
        GameManager.instance.PauseGame();
        active = true;
    }

    public void BreakPart()
    {
        breakPanel.SetActive(true);
        GameManager.instance.UnpauseGame();
        Actions.instance.BrokePart += Broke;
    }

    public void Broke()
    {
        breakPanel.SetActive(false);
        Actions.instance.BrokePart -= Broke;
        PlayerPrefs.SetInt("TutorialComplete", 1);
        active = false;
    }

    public void Close()
    {
        startPanel.SetActive(false);
        middlePanel.SetActive(false);
        breakPanel.SetActive(false);
        active = false;
    }
}
