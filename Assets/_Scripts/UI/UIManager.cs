using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameManager gameManager;

    public GameObject canvas;
    public GameObject transformationButton;
    public GameObject pauseButton;
    public TextMeshProUGUI levelNumber;
    public GameObject levelNumberPanel;

    private void Awake()
    {
        if (instance == null || instance == this)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadLevel();
        }
    }

    public void ReloadLevel()
    {
        gameManager.ReloadLevel();
    }

    public void ShowEndScreen()
    {
        transformationButton.SetActive(false);
        canvas.GetComponent<Animator>().SetTrigger("TurnOnEndScreen");
    }

    public void PlayTransitionAnim(bool withNext)
    {
        if (withNext)
        {
            canvas.GetComponent<Animator>().SetTrigger("LevelTransitionNext");
        }
        else
        {
            canvas.GetComponent<Animator>().SetTrigger("LevelTransition");
        }
    }

    public void PlayLoadedAnim()
    {
        canvas.GetComponent<Animator>().SetTrigger("LevelLoaded");
    }

    public void LoadedAnimEnded()
    {
        GameManager.instance.Loaded();
        if (GameManager.instance.curLevel != 0 && GameManager.instance.curLevel != GameManager.instance.levels.Count - 1)
        {
            GameManager.instance.SetAllowActions(true);
        }
    }
    public void ReadyForLoading()
    {
        GameManager.instance.ReadyForLoading();
    }

    public void PlayWoosh()
    {
        AudioManager.instance.Play("Woosh");
    }

    public void PlayReverseWoosh()
    {
        AudioManager.instance.Play("Reverse woosh");
    }

    public void ActiveRestartButton(bool isActive)
    {
        transformationButton.SetActive(isActive);
    }

    public void ActivePauseButton(bool isActive)
    {
        pauseButton.SetActive(isActive);
    }

    public void SetLevelNumber(int curLevel, int maxLevels)
    {
        levelNumber.text = curLevel.ToString() + " / " + maxLevels.ToString();
    }

    public void ShowLevelNumber(bool value)
    {
        levelNumberPanel.SetActive(value);
    }

}
