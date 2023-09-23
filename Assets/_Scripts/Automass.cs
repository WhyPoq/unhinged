using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class Automass : MonoBehaviour
{
    public float density = 1;

    void Update()
    {
        if (transform.hasChanged)
        {
            transform.hasChanged = false;

            UpdateMass();
        }
    }

    private void OnValidate()
    {
        UpdateMass();
    }

    void UpdateMass()
    {
        BoxCollider col = GetComponent<BoxCollider>();
        Vector3 size = col.bounds.size;
        float volume = size.x * size.y * size.z;
        GetComponent<Rigidbody>().mass = volume * density;
    }
}
