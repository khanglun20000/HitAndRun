using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform target;
    protected LevelController lc;
    private float smoothSpeed;
    public float startSmoothSpeed=0.5f;
    private Vector3 velocity = Vector3.zero;
    public Vector3 _offset;
    private Vector3 desiredPosition;
    private void Awake()
    {
        lc = FindObjectOfType<LevelController>();
    }
    void FixedUpdate()
    {
        if (lc.filmingBoss == false)
        {
            smoothSpeed = startSmoothSpeed;
            desiredPosition = target.transform.position + _offset;
        }
        else
        {
            smoothSpeed = 1f;
            desiredPosition = Vector3.zero + _offset;
        }

        Vector3 smoothPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);

        transform.position = smoothPosition;
    }
}
