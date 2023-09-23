using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public static class ConnectPointsStatic
{
    [MenuItem("My Commands/Connect _g")]
    static void ConnectSelected()
    {
        if(ConnectPoints.instance == null)
        {
            ConnectPoints.instance = GameObject.FindObjectOfType<ConnectPoints>();
        }
        ConnectPoints.instance.ConnectSelected(false);
    }

    [MenuItem("My Commands/Connect fixed %g")]
    static void ConnectFixedSelected()
    {
        if (ConnectPoints.instance == null)
        {
            ConnectPoints.instance = GameObject.FindObjectOfType<ConnectPoints>();
        }
        ConnectPoints.instance.ConnectSelected(true);
    }

    [MenuItem("My Commands/Clear all connections")]
    static void ClearAllConnection()
    {
        ConnectPoints.instance.ClearAllConnection();
    }
}