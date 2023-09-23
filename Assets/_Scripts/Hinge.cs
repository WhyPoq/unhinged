using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public class Hinge : MonoBehaviour
{
    [SerializeField]
    public List<Rigidbody> connected = new List<Rigidbody>();
    [SerializeField]
    public List<ConfigurableJoint> joints = new List<ConfigurableJoint>();

    [SerializeField]
    public List<Rigidbody> connectedPlanes = new List<Rigidbody>();
    [SerializeField]
    public List<Joint> planeJoints = new List<Joint>();

    public bool refreshConnections = false;
    public bool refreshFixedConnections = false;

    private void Update()
    {
        if(refreshConnections)
        {
            refreshConnections = false;
            RefreshConnections();
        }
        if (refreshFixedConnections)
        {
            refreshFixedConnections = false;
            RefreshFixedConnections();
        }
    }

    void RefreshConnections() //editor
    {
#if UNITY_EDITOR
        Undo.RegisterCompleteObjectUndo(GetComponent<Hinge>(), "Vertex refreshed");
        for (int i = 0; i < connected.Count; i++)
        {
            if (connected[i] == null)
            {
                connected.RemoveAt(i);
                Undo.DestroyObjectImmediate(joints[i]);
                joints.RemoveAt(i);
                i--;
            }
        }
#endif
    }

    void RefreshFixedConnections() //editor
    {
#if UNITY_EDITOR
        Undo.RegisterCompleteObjectUndo(GetComponent<Hinge>(), "Vertex refreshed");
        for (int i = 0; i < connectedPlanes.Count; i++)
        {
            if (connectedPlanes[i] == null)
            {
                connectedPlanes.RemoveAt(i);
                if (planeJoints[i] != null)
                {
                    Undo.DestroyObjectImmediate(planeJoints[i]);
                }
                planeJoints.RemoveAt(i);
                i--;
            }
        }
#endif
    }

    public void AddConnection(Rigidbody to, float connectionLength)
    {
        connected.Add(to);
#if UNITY_EDITOR
        ConfigurableJoint joint = Undo.AddComponent<ConfigurableJoint>(gameObject);
#else
        ConfigurableJoint joint = gameObject.AddComponent<ConfigurableJoint>();
#endif
        joint.xMotion = ConfigurableJointMotion.Locked;
        joint.yMotion = ConfigurableJointMotion.Locked;
        joint.zMotion = ConfigurableJointMotion.Locked;

        joint.connectedBody = to;
        joint.anchor = new Vector3(0, 0, 0);
        joint.autoConfigureConnectedAnchor = false;
        joint.connectedAnchor = new Vector3(0, connectionLength, 0);
        joints.Add(joint);

        to.GetComponent<Connection>().connectedTo.Add(this);
    }

    public void DestroyConnection(Rigidbody to)
    {
#if UNITY_EDITOR
        Undo.RegisterCompleteObjectUndo(GetComponent<Hinge>(), "Vertex updated");
#endif
        for (int i = 0; i < connected.Count; i++)
        {
            if (connected[i] == to)
            {
                connected.RemoveAt(i);
                if (Application.isPlaying)
                {
                    Destroy(joints[i]);
                }
                else
                {
#if UNITY_EDITOR
                    Undo.DestroyObjectImmediate(joints[i]);
#endif
                }

                joints.RemoveAt(i);
                break;
            }
        }    
    }

    public void AddFixedConnection(Rigidbody to, bool fixedConn)
    {
        connectedPlanes.Add(to);
        if (!fixedConn)
        {
#if UNITY_EDITOR
            ConfigurableJoint joint = Undo.AddComponent<ConfigurableJoint>(gameObject);
#else
        ConfigurableJoint joint = gameObject.AddComponent<ConfigurableJoint>();
#endif

            joint.xMotion = ConfigurableJointMotion.Locked;
            joint.yMotion = ConfigurableJointMotion.Locked;
            joint.zMotion = ConfigurableJointMotion.Locked;

            joint.anchor = new Vector3(0, 0, 0);

            joint.connectedBody = to;
            planeJoints.Add(joint);
        }
        else
        {
#if UNITY_EDITOR
            FixedJoint joint = Undo.AddComponent<FixedJoint>(gameObject);
#else
        FixedJoint joint = gameObject.AddComponent<FixedJoint>();
#endif
            joint.connectedBody = to;
            planeJoints.Add(joint);
        }

        FixedPlane plane = to.GetComponent<FixedPlane>();
        for (int i = 0; i < plane.connectedTo.Count; i++)
        {
            if(plane.connectedTo[i] == null)
            {
                plane.connectedTo.RemoveAt(i);
                i--;
            }
        }
        plane.connectedTo.Add(this);
        plane.isFixed.Add(fixedConn);

    }

    public void DestroyFixedConnection(Rigidbody to)
    {
#if UNITY_EDITOR
        Undo.RegisterCompleteObjectUndo(GetComponent<Hinge>(), "Vertex updated");
#endif
        for (int i = 0; i < connectedPlanes.Count; i++)
        {
            if (connectedPlanes[i] == to)
            {
                connectedPlanes.RemoveAt(i);
                if (Application.isPlaying)
                {
                    Destroy(planeJoints[i]);
                }
                else
                {
#if UNITY_EDITOR
                    Undo.DestroyObjectImmediate(planeJoints[i]);
#endif
                }

                planeJoints.RemoveAt(i);
                break;
            }
        }
    }
}
