using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float distance = 3.0f;
    public float height = 3.0f;
    public float dampingFactor = 5.0f;

    void Update()
    {
        Vector3 wantedPosition;
        float damping = Vector3.Distance(target.position, transform.position)* dampingFactor;
        wantedPosition = target.TransformPoint(0, height, -distance);
        transform.position = Vector3.Lerp(transform.position, wantedPosition, Time.deltaTime * damping);
        transform.LookAt(target, target.up);
    }
}
