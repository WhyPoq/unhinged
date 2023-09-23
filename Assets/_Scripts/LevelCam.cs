using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCam : MonoBehaviour
{
    private void Awake()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.levelCam = gameObject;
        }
    }
}
