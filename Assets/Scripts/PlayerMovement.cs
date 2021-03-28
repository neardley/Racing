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

    public int id;
    Player photonPlayer;

    [Header("Components")]
    public PhotonView photonView;

    public int lastGateIndex = -1;

    public static double StartingTime;

    public void SetStartTime(double time)
    {
        StartingTime = time;
    }

    public double ShowStartTime()
    {
        return StartingTime;
    }

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
        if (Input.GetKeyDown(KeyCode.R) && lastGateIndex != -1)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            transform.position = Game_Control.instance.trackGates[lastGateIndex].transform.position;
            transform.rotation = Game_Control.instance.trackGates[lastGateIndex].transform.rotation;



        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Gate")
        {
            int newIndex = Game_Control.instance.GetGateIndex(other.gameObject);

            Debug.Log("Last Gate:" + lastGateIndex.ToString() + "  new Gate:" + newIndex.ToString());
            //is next gate?
            if(newIndex == lastGateIndex + 1)
            {
                lastGateIndex = newIndex;
            }

            //is last gate?
            if(newIndex == 0 && lastGateIndex == Game_Control.instance.trackGates.Count - 1)
            {
                Game_Control.instance.photonView.RPC("WinGame", RpcTarget.All, id);
            }
        }
    }

}
