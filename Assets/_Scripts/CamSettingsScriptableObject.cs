using UnityEngine;

[CreateAssetMenu(fileName = "CamSettings", menuName = "ScriptableObjects/CamSettings", order = 1)]
public class CamSettingsScriptableObject : ScriptableObject
{
    public float rotationSpeed = 0.5f;
    public float moveSpeed = 0.1f;
    public float zoomSpeed = 0.4f;
}