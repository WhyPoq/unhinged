using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
public class FixedPlane : MonoBehaviour
{
    public List<Hinge> connectedTo = new List<Hinge>();
    public List<bool> isFixed = new List<bool>();


    void OnDestroy()
    {
        Clear();
    }

    public void Clear()
    {
        if (!Application.isPlaying && Application.isEditor)
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            for (int i = 0; i < connectedTo.Count; i++)
            {
                if (connectedTo[i] != null)
                {
                    connectedTo[i].refreshFixedConnections = true;
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
                    connectedTo[i].DestroyFixedConnection(rb);
                }
            }
        }
        connectedTo.Clear();
        isFixed.Clear();
    }

    private void OnDrawGizmos()
    {
        while(isFixed.Count < connectedTo.Count)
        {
            isFixed.Add(false);
        }

        Gizmos.color = new Color(0, 0, 100);
        for (int i = 0; i < connectedTo.Count; i++)
        {
            if (connectedTo[i] != null)
            {
                if (isFixed[i])
                {
                    Gizmos.color = new Color(0, 100, 100);
                }
                else
                {
                    Gizmos.color = new Color(0, 0, 100);
                }
                Gizmos.DrawLine(transform.position, connectedTo[i].transform.position);
            }
        }
    }
}
