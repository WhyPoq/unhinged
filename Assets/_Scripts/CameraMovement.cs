using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class SavedCamState
{
    public float curAngle;
    public float curH;
    public float curDistance;
    public float curZoom;

    public SavedCamState(float curAngle, float curH, float curDistance, float curZoom)
    {
        this.curAngle = curAngle;
        this.curH = curH;
        this.curDistance = curDistance;
        this.curZoom = curZoom;
    }
}

[ExecuteInEditMode]
public class CameraMovement : MonoBehaviour
{
    public CamSettingsScriptableObject camSettings;

    public Transform center;
    public float height = 2f;

    public float curAngle = 0f;
    public float curH = 0;
    public float curDistance = 7f;
    public float curZoom = 5;

    public float maxDistance = 20f;
    public float minDistance = 1f;

    Camera cam;

    public static CameraMovement instance;
    public static SavedCamState savedCamState;

    private void Awake()
    {
        cam = GetComponent<Camera>();
        CameraMovement.instance = this;

        if(CameraMovement.savedCamState != null)
        {
            SetSavedCam();
            CameraMovement.savedCamState = null;
            PositionCamera();
        }
    }

    private void Update()
    {
        if (Application.isPlaying)
        {
            curAngle = (curAngle + (Input.GetAxis("Horizontal") * camSettings.rotationSpeed * Time.deltaTime)) % 360;
            curH += Input.GetAxis("Vertical") * camSettings.moveSpeed * Time.deltaTime;
            curZoom += -Input.mouseScrollDelta.y * camSettings.zoomSpeed * Time.deltaTime;
            if (curZoom > maxDistance) curZoom = maxDistance;
            if (curZoom < minDistance) curZoom = minDistance;
        }
        else
        {
            PositionCamera();
        }
    }

    private void FixedUpdate()
    {
        PositionCamera();
    }

    void PositionCamera()
    {
        if (center != null)
        {
            transform.position = center.transform.position +
                new Vector3(Mathf.Cos(curAngle / 180f * Mathf.PI), 0, Mathf.Sin(curAngle / 180f * Mathf.PI)) * curDistance
                + new Vector3(0, height + curH, 0);
            transform.LookAt(center.transform.position + new Vector3(0, curH, 0));

            if (cam == null)
            {
                cam = GetComponent<Camera>();
            }
            cam.orthographicSize = curZoom;
        }
    }

    public static SavedCamState GetSavedCam()
    {
        return new SavedCamState(instance.curAngle, instance.curH, instance.curDistance, instance.curZoom);
    }

    public static void SetSavedCam()
    {
        instance.curAngle = savedCamState.curAngle;
        instance.curH = savedCamState.curH;
        instance.curDistance = savedCamState.curDistance;
        instance.curZoom = savedCamState.curZoom;
    }
}
