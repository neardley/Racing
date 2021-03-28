using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class CameraFollow : MonoBehaviourPun
{
    Transform target;
    public float distance = 3.0f;
    public float height = 3.0f;
    public float dampingFactor = 5.0f;

    public static CameraFollow instance;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        if (target != null)
        {
            Vector3 wantedPosition;
            float damping = Vector3.Distance(target.position, transform.position) * dampingFactor;
            wantedPosition = target.TransformPoint(0, height, -distance);
            transform.position = Vector3.Lerp(transform.position, wantedPosition, Time.deltaTime * damping);
            transform.LookAt(target);
        }
        
    }

    public void AssignTarget(GameObject player)
    {
        this.target = player.transform;
    }
}
