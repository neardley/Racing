using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonLobby : MonoBehaviourPunCallbacks
{

    public static PhotonLobby lobby;

    public GameObject startbutton;
    public GameObject cancelbutton;


    private void Awake()
    {
        lobby = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("Player Has connected to the Photon master server");
        PhotonNetwork.AutomaticallySyncScene = true;
        startbutton.SetActive(true);

    }

    public void OnStartClick()
    {
        PhotonNetwork.JoinRandomRoom();
        startbutton.SetActive(false);
        cancelbutton.SetActive(true);
    }

    public void onCanelClick()
    {
        PhotonNetwork.LeaveRoom();
        startbutton.SetActive(true);
        cancelbutton.SetActive(false);
    }


    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        Debug.Log("Failed to Join");
        CreateRoom();
    }
    void CreateRoom()
    {
        int RandomRoomName = Random.Range(0, 100);
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 10 };
        PhotonNetwork.CreateRoom("Room" + RandomRoomName, roomOps);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("failed to make a room, trying again");
        CreateRoom();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
