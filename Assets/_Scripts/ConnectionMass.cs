using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Rigidbody))]
public class ConnectionMass : MonoBehaviour
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
        float volume = transform.lossyScale.x * transform.lossyScale.y * transform.lossyScale.z;
        GetComponent<Rigidbody>().mass = volume * density;
    }
}
