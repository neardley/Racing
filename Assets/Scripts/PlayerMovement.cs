using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float horsePower;
    [SerializeField] List<WheelCollider> steeringWheels;
    [SerializeField] List<WheelCollider> drivenWheels;

    Rigidbody rb;

    float wheelPos, currentTorqe, drivenWheelRadius;
    float maxSteerAngle = 30f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        drivenWheelRadius = drivenWheels[0].radius;
    }

    // I put all physics related things in here
    private void FixedUpdate()
    {
        float c = horsePower * 1136.8f * drivenWheelRadius / drivenWheels.Count;
        currentTorqe = Input.GetAxis("Vertical") * c;
        if(rb.velocity.magnitude > 1)
        {
            currentTorqe /= rb.velocity.magnitude;
        }
        wheelPos = Input.GetAxis("Horizontal") * maxSteerAngle;

        foreach (WheelCollider wheel in drivenWheels)
        {
            wheel.motorTorque = currentTorqe;
        }

        foreach(WheelCollider wheel in steeringWheels)
        {
            wheel.steerAngle = wheelPos;
        }

    }

}
