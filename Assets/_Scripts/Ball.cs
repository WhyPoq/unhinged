using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float velChangeThreshold = 1f;
    public float velMoveThreshold = 0.05f;


    Rigidbody rb;

    Vector3 lastVel;

    public bool isTouching = false;
    public bool playingRoll = false;

    public static Ball instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        AudioManager.instance.Stop("Roll");
        rb = GetComponent<Rigidbody>();

        rb.drag = 5f;
        rb.angularDrag = 5f;
    }

    private void OnCollisionStay(Collision collision)
    {
        isTouching = true;
    }

    private void Update()
    {
        if(GameManager.instance.gamePaused && playingRoll)
        {
            AudioManager.instance.Stop("Roll");
            playingRoll = false;
        }
    }

    private void FixedUpdate()
    {
        if((rb.velocity - lastVel).magnitude > velChangeThreshold)
        {
            AudioManager.instance.Play("Hit");
        }

        lastVel = rb.velocity;

        if(isTouching && rb.angularVelocity.magnitude > velMoveThreshold && !GameManager.instance.gamePaused)
        {
            float normVel = 7.5f;
            float pitch = Mathf.Max(0.5f, Mathf.Min(1.5f, rb.angularVelocity.magnitude / normVel)) + 0.5f;
            float volume = Mathf.Max(0f, Mathf.Min(1f, Mathf.Log(rb.angularVelocity.magnitude / normVel + 1, 2f)));
            AudioManager.instance.SetSettings("Roll", volume, pitch);

            if (!playingRoll)
            {
                AudioManager.instance.Play("Roll");
                playingRoll = true;
            }
        }
        else if(playingRoll)
        {
            AudioManager.instance.Stop("Roll");
            playingRoll = false;
        }

        isTouching = false;
    }
}
