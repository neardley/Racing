using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class QuickStartLobbyController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject quickStartButton; // button used for creating and joining a game
    [SerializeField]
    private GameObject quickCanelButton; // button used to stop searching for a game to join
    [SerializeField]
    private int Roomsize; // set the number of players in the room at anyy one time
    // Start is called before the first frame update

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        quickStartButton.SetActive(true);
    }


    public void QuickStart()
    {
        quickStartButton.SetActive(false);
        quickCanelButton.SetActive(true);
        PhotonNetwork.JoinRandomRoom();
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to join a room");
        CreateRoom();
    }
    void CreateRoom()
    {
        Debug.Log("Creating room now");
        int RandomRoomNumber = Random.Range(0, 10000);
        RoomOptions roomOps = new RoomOptions() 
            { 
            IsVisible = true, 
            IsOpen = true, 
            MaxPlayers = (byte)Roomsize 
            };
        PhotonNetwork.CreateRoom("Room" + RandomRoomNumber, roomOps);
        Debug.Log(RandomRoomNumber);
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to create room. . . trying again");
        CreateRoom();
    }

    public void QuickCanel()
    {
        quickCanelButton.SetActive(false);
        quickStartButton.SetActive(true);
        PhotonNetwork.LeaveRoom();
    }
}
