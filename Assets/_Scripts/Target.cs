using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Target : MonoBehaviour
{
    public Material reachedMat;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            Reached();
        }
    }

    private void Reached()
    {
        AudioManager.instance.Play("Complete");
        gameObject.GetComponent<Renderer>().material = reachedMat;
        GameManager.instance.EndLevel();
    }
}
