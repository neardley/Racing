using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float maxTorqe;
    [SerializeField] bool Rearwheeldrive = true;
    Rigidbody rb;

    WheelCollider FLW, FRW, RLW, RRW;

    float wheelPos, currentTorqe;
    float maxSteerAngle = 30f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        FLW = transform.Find("Wheel_Front_Left").GetComponent<WheelCollider>();
        FRW = transform.Find("Wheel_Front_Right").GetComponent<WheelCollider>();
        RLW = transform.Find("Wheel_Rear_Left").GetComponent<WheelCollider>();
        RRW = transform.Find("Wheel_Rear_Right").GetComponent<WheelCollider>();
    }

    // I put all physics related things in here
    private void FixedUpdate()
    {
        currentTorqe = Input.GetAxis("Vertical")*maxTorqe;
        if (Rearwheeldrive)
        {
            RLW.motorTorque = currentTorqe;
            RRW.motorTorque = currentTorqe;
        }
        else
        {
            FLW.motorTorque = currentTorqe;
            FRW.motorTorque = currentTorqe;
        }

        
        wheelPos = Input.GetAxis("Horizontal")*maxSteerAngle;
        FLW.steerAngle = wheelPos;
        FRW.steerAngle = wheelPos;


    }

}
