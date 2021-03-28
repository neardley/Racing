using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PhotonView))]
public class PlayerMovement : MonoBehaviourPunCallbacks
{
    [SerializeField] float horsePower;
    [SerializeField] List<WheelCollider> steeringWheels;
    [SerializeField] List<WheelCollider> drivenWheels;

    float wheelPos, currentTorqe, drivenWheelRadius;
    float maxSteerAngle = 30f;

    Rigidbody rb;

    private int id;
    Player photonPlayer;

    [Header("Components")]
    public PhotonView photonView;

    int lastGateIndex = -1;



    [PunRPC]
    public void Init(Player player)
    {
        photonPlayer = player;
        id = player.ActorNumber;
        Game_Control.instance.players[id - 1] = this;

        if (!photonView.IsMine)
            rb.isKinematic = true;
    }

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

    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Gate")
        {
            int newIndex = Game_Control.instance.GetGateIndex(other.gameObject);

             
        }
    }

    void EndRace()
    {

    }
}
