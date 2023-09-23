using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : MonoBehaviour
{
    public Material normal;
    public Material selected;

    public void Select()
    {
        gameObject.GetComponent<Renderer>().material = selected;
    }

    public void Deselect()
    {
        gameObject.GetComponent<Renderer>().material = normal;
    }
}
