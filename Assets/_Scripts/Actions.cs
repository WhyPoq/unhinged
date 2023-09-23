using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Actions : MonoBehaviour
{
    public static Actions instance;

    public Camera cam;

    Destroyable selected;

    bool firstMove = false;

    public event Action BrokePart;

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (!GameManager.instance.allowActions || GameManager.instance.gamePaused)
        {
            if (selected != null) selected.Deselect();
            selected = null;
            return;                                                                 //!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        }

        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Selectable selectable = hit.collider.GetComponent<Selectable>();
            if (selectable != null)
            {
                Destroyable prevSelected = selected;

                if (selected != null) selected.Deselect();

                if (selectable.isChild)
                {
                    selected = hit.collider.transform.parent.gameObject.GetComponent<Destroyable>();
                }
                else
                {
                    selected = hit.collider.GetComponent<Destroyable>();
                }
                selected.Select();

                if(prevSelected != selected)
                {
                    AudioManager.instance.Play("Pick");
                }
            }
            else
            {
                if (selected != null) selected.Deselect();
                selected = null;
            }
        }
        else
        {
            if (selected != null) selected.Deselect();
            selected = null;
        }

        if (Input.GetMouseButtonDown(0) && selected != null)
        {
            if (!firstMove)
            {
                firstMove = true;
                Ball.instance.GetComponent<Rigidbody>().drag = 0.05f;
                Ball.instance.GetComponent<Rigidbody>().angularDrag = 0.05f;
            }

            AudioManager.instance.Play("Break");
            Destroy(selected.gameObject);
            selected = null;

            BrokePart?.Invoke();
        }
    }
}
