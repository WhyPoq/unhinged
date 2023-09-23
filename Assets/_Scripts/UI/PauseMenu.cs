using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject menuObj;

    bool open = false;
    bool settingsOpen = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && GameManager.instance.curLevel != 0
            && GameManager.instance.curLevel != GameManager.instance.levels.Count - 1 && !GameManager.instance.loading)
        {
            if (!open)
                Open();
            else if (!settingsOpen)
                Close();
        }
    }

    public void LoadMainMenu()
    {
        open = false;
        menuObj.SetActive(false);
        GameManager.instance.LoadLevel(0, false);
        Tutorial.instance.Close();
    }

    public void Open()
    {
        open = true;
        GameManager.instance.PauseGame();
        menuObj.SetActive(true);
    }

    public void Close()
    {
        open = false;
        if (!Tutorial.instance.active)
            GameManager.instance.UnpauseGame();
        menuObj.SetActive(false);
    }

    public void OpenSettings()
    {
        settingsOpen = true;
    }

    public void CloseSettings()
    {
        settingsOpen = false;
    }
}
