using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class ConnectPoints : MonoBehaviour
{
    public static ConnectPoints instance;
    public GameObject unityConnection;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void OnValidate()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void ConnectSelected(bool fixedConn)
    {
#if UNITY_EDITOR
        List<GameObject> selectedObjs = new List<GameObject>();
        for (int i = 0; i < Selection.objects.Length; i++)
        {
            if (Selection.objects[i] is GameObject)
            {
                GameObject g = (GameObject)Selection.objects[i];
                selectedObjs.Add(g);
            }
        }
        if(selectedObjs.Count == 1)
        {
            GameObject a = selectedObjs[0];
            if (a.GetComponent<Hinge>() != null)
            {
                GameObject clone = Instantiate(a, a.transform.parent);
                Undo.RegisterCreatedObjectUndo(clone, "Clone vertex");

                clone.GetComponent<Hinge>().connected.Clear();
                clone.GetComponent<Hinge>().joints.Clear();

                clone.GetComponent<Hinge>().connectedPlanes.Clear();
                clone.GetComponent<Hinge>().planeJoints.Clear();

                ConfigurableJoint[] toRemove = clone.GetComponents<ConfigurableJoint>();
                for (int k = 0; k < toRemove.Length; k++)
                {
                    Undo.DestroyObjectImmediate(toRemove[k]);
                }
                FixedJoint[] toRemoveFixed = clone.GetComponents<FixedJoint>();
                for (int k = 0; k < toRemoveFixed.Length; k++)
                {
                    Undo.DestroyObjectImmediate(toRemoveFixed[k]);
                }
                Selection.activeGameObject = clone;
            }
            else if (a.GetComponent<FixedPlane>() != null)
            {
                GameObject clone = Instantiate(a, a.transform.parent);
                Undo.RegisterCreatedObjectUndo(clone, "Clone plane");

                clone.GetComponent<FixedPlane>().Clear();

                Selection.activeGameObject = clone;
            }
        }
        else if (selectedObjs.Count == 2)
        {
            GameObject a = selectedObjs[0];
            GameObject b = selectedObjs[1];

            if(a.GetComponent<Hinge>() == null)
            {
                GameObject tmp = a;
                a = b;
                b = tmp;
            }

            if (a.GetComponent<Hinge>() != null && b.GetComponent<Hinge>() == null && b.GetComponent<FixedPlane>() != null)
            {
                Undo.RegisterCompleteObjectUndo(a.GetComponent<Hinge>(), "Connect points");
                Undo.RegisterCompleteObjectUndo(b.GetComponent<FixedPlane>(), "Connect points");

                /*Vector3 to = a.transform.position - b.transform.position;
                to.x = Mathf.Sign(to.x) / 2;
                to.y = 0;
                to.z = Mathf.Sign(to.z) / 2;*/

                a.GetComponent<Hinge>().AddFixedConnection(b.GetComponent<Rigidbody>(), fixedConn);
                Undo.FlushUndoRecordObjects();
            }
            else if (a.GetComponent<Hinge>() != null && b.GetComponent<Hinge>() != null)
            {
                Undo.RegisterCompleteObjectUndo(a.GetComponent<Hinge>(), "Connect points");
                Undo.RegisterCompleteObjectUndo(b.GetComponent<Hinge>(), "Connect points");

                Vector3 pos = (a.transform.position + b.transform.position) / 2;


                GameObject connection = Instantiate(unityConnection, a.transform.parent);
                Undo.RegisterCreatedObjectUndo(connection, "New Connection");

                connection.transform.position = pos;

                Vector3 vec = (b.transform.position - a.transform.position);
                Vector3 dir = vec.normalized;
                float dist = vec.magnitude;
                float r = a.transform.localScale.x / 2;
                float conR = connection.transform.localScale.x / 2;

                float remDist = Mathf.Sqrt(r * r - conR * conR) * 2 - 0.1f;

                Vector3 scale = connection.transform.localScale;
                scale.y = (dist - remDist) / 2;
                connection.transform.localScale = scale;

                connection.GetComponent<CapsuleCollider>().height *= (dist - 2 * r) / (dist - remDist);

                connection.transform.rotation = Quaternion.LookRotation(dir) *
                    Quaternion.FromToRotation(Vector3.up, Vector3.forward);

                a.GetComponent<Hinge>().AddConnection(connection.GetComponent<Rigidbody>(), -dist / (dist - remDist));
                b.GetComponent<Hinge>().AddConnection(connection.GetComponent<Rigidbody>(), dist / (dist - remDist));

                Undo.FlushUndoRecordObjects();
            }
        }
#endif
    }

    public void ClearAllConnection()
    {
#if UNITY_EDITOR
        Hinge[] spheres = GameObject.FindObjectsOfType<Hinge>();
        for (int i = 0; i < spheres.Length; i++)
        {
            Undo.RegisterCompleteObjectUndo(spheres[i], "Clear hinge");
            spheres[i].connected.Clear();
            spheres[i].joints.Clear();

            spheres[i].connectedPlanes.Clear();
            spheres[i].planeJoints.Clear();

            ConfigurableJoint[] toRemove = spheres[i].gameObject.GetComponents<ConfigurableJoint>();
            for (int k = 0; k < toRemove.Length; k++)
            {
                Undo.DestroyObjectImmediate(toRemove[k]);
            }
            FixedJoint[] toRemoveFixed = spheres[i].gameObject.GetComponents<FixedJoint>();
            for (int k = 0; k < toRemoveFixed.Length; k++)
            {
                Undo.DestroyObjectImmediate(toRemoveFixed[k]);
            }
        }

        Connection[] connections = GameObject.FindObjectsOfType<Connection>();
        for (int i = 0; i < connections.Length; i++)
        {
            Undo.DestroyObjectImmediate(connections[i].gameObject);
        }

        FixedPlane[] planes = GameObject.FindObjectsOfType<FixedPlane>();
        for (int i = 0; i < planes.Length; i++)
        {
            Undo.RegisterCompleteObjectUndo(planes[i], "Clear plane");
            planes[i].connectedTo.Clear();
        }
#endif
    }
}
