using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Runtime.InteropServices;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject levelCam;

    [SerializeField]
    UIManager uIManager;

    [SerializeField]
    Camera cam;

    [Scene]
    public List<string> levels = new List<string>();

    public int curLevel = 0;

    [SerializeField]
    private float endScreenDelay = 3f;

    bool levelCompleted = false;
    int savedId;

    public bool canReload = false;
    public bool gamePaused = false;
    public bool allowActions = false;
    public bool loading = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if(UIManager.instance == null)
        {
            UIManager.instance = uIManager;
        }

        if (curLevel != -1)
        {
            SceneManager.LoadScene(levels[curLevel], LoadSceneMode.Additive);
            if (curLevel != 0 && curLevel != levels.Count - 1)
            {
                canReload = true;
                SetAllowActions(true);
                UIManager.instance.ActiveRestartButton(true);
                UIManager.instance.ActivePauseButton(true);
            }
        }
        else
        {
            SetAllowActions(true);
        }
    }

    public void EndLevel()
    {
        if (!levelCompleted)
        {
            PlayerPrefs.SetInt("CompletedLevel", curLevel);
            SetAllowActions(false);
            canReload = false;
            levelCompleted = true;
            StartCoroutine(ShowEndScreen());
        }
    }

    IEnumerator ShowEndScreen()
    {
        yield return new WaitForSeconds(endScreenDelay);
        UIManager.instance.ShowEndScreen();
    }

    public void LoadNextLevel()
    {
        if (curLevel + 1 < levels.Count)
        {
            LoadLevel(curLevel + 1, true);
        }
    }

    public void LoadLevel(int id, bool withNext)
    {
        loading = true;
        UIManager.instance.ActiveRestartButton(false);
        canReload = false;
        levelCompleted = false;
        UIManager.instance.PlayTransitionAnim(withNext);
        savedId = id;
    }

    public void ReloadLevel()
    {
        if (!canReload) 
            return;

        AudioManager.instance.Play("Restart");

        SavedCamState savedCamState = CameraMovement.GetSavedCam();
        levelCompleted = false;
        SceneManager.UnloadSceneAsync(levels[curLevel]);
        SceneManager.LoadScene(levels[curLevel], LoadSceneMode.Additive);
        CameraMovement.savedCamState = savedCamState;
    }

    public void ReadyForLoading()
    {
        cam.gameObject.SetActive(true);
        if (levelCam != null)
        {
            levelCam.SetActive(false);
        }
        StartCoroutine(SceneSwitch(levels[curLevel], levels[savedId]));
        curLevel = savedId;
    }

    IEnumerator SceneSwitch(string from, string to)
    {
        if (SceneManager.GetSceneByName(from).isLoaded)// && from != to)
        {
            AsyncOperation unload = SceneManager.UnloadSceneAsync(from);
            yield return unload;
        }
        yield return null;
        SceneManager.LoadScene(to, LoadSceneMode.Additive);
        yield return null;

        if (curLevel == 0)
        {
            UIManager.instance.ActivePauseButton(false);
        }
        else
        {
            UIManager.instance.ActivePauseButton(true);
        }

        UIManager.instance.PlayLoadedAnim();
        cam.gameObject.SetActive(false);
        UnpauseGame();

        if (curLevel != 0 && curLevel != levels.Count - 1)
        {
            canReload = true;
            UIManager.instance.transformationButton.SetActive(true);

            if (!PlayerPrefs.HasKey("TutorialComplete") || PlayerPrefs.GetInt("TutorialComplete") != 1)
            {
                Tutorial.instance.StartTutorial();
            }

            UIManager.instance.SetLevelNumber(curLevel, levels.Count - 2);
            UIManager.instance.ShowLevelNumber(true);
        }
        else
        {
            UIManager.instance.ShowLevelNumber(false);
        }
    }

    public void Loaded()
    {
        loading = false;
    }

    public void SetAllowActions(bool value)
    {
        allowActions = value;
    }

    public void PauseGame()
    {
        canReload = false;
        gamePaused = true;
        Time.timeScale = 0;
        SetAllowActions(false);
    }

    public void UnpauseGame()
    {
        canReload = true;
        gamePaused = false;
        Time.timeScale = 1;
        SetAllowActions(true);
    }

}
