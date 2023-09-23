using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[ExecuteInEditMode]
public class Connection : MonoBehaviour
{
    public List<Hinge> connectedTo = new List<Hinge>();

    void OnDestroy()
    {
        if (!Application.isPlaying && Application.isEditor)
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            for (int i = 0; i < connectedTo.Count; i++)
            {
                if (connectedTo[i] != null)
                {
                    connectedTo[i].refreshConnections = true;
                }
            }
        }
        else
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            for (int i = 0; i < connectedTo.Count; i++)
            {
                if (connectedTo[i] != null)
                {
                    connectedTo[i].DestroyConnection(rb);
                }
            }
        }
    }
}
