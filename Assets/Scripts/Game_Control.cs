﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;

public class Game_Control : MonoBehaviourPunCallbacks
{
    [Header("Race Info")]
    public List<GameObject> trackGates;
    public bool gameEnded = false;
    public int numLaps;

    [Header("Player Info")]
    public string playerPrefabLocation;
    public Transform[] spawnPoints;
    public PlayerMovement[] players;
    public int[] playerPositions;
    private int playersInGame;
    private int playersSpawned = 0;

    public static Game_Control instance;

    public static double StartTime;

    [Header("Components")]
    public PhotonView photonView;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        players = new PlayerMovement[PhotonNetwork.PlayerList.Length];
        playerPositions = new int[PhotonNetwork.PlayerList.Length];
        photonView.RPC("NotifyJoin", RpcTarget.AllBuffered);
    }

    [PunRPC]
    void NotifyJoin()
    {
        playersInGame++;

        if(playersInGame == PhotonNetwork.PlayerList.Length)
        {
            SpawnPlayer();
        }
    }
    
    void SpawnPlayer()
    {
        GameObject playerObj;

        if (!PhotonNetwork.IsMasterClient)
        {
            playerObj = PhotonNetwork.Instantiate(playerPrefabLocation, spawnPoints[0].position, Quaternion.identity);
            StartTime = PhotonNetwork.Time;
        }
        else
        {
            playerObj = PhotonNetwork.Instantiate(playerPrefabLocation, spawnPoints[1].position, Quaternion.identity);
        }

        PlayerMovement playerScript = playerObj.GetComponent<PlayerMovement>();

        playerScript.photonView.RPC("Init", RpcTarget.All, PhotonNetwork.LocalPlayer);
        if (playerObj.GetPhotonView().IsMine)
        {
            CameraFollow.instance.AssignTarget(playerObj);
            gameObject.GetComponent<UI_Control>().AssignTarget(playerObj);
            playerScript.SetStartTime(StartTime);
        }
    }

    [PunRPC]
    void WinGame(int playerid)
    {
        if(PhotonNetwork.IsMasterClient)
        {
            if (playerid == 1)
                Debug.Log("YOU WIN");
            else
                Debug.Log("YOU LOSE");
        }
        else
        {
            if (playerid == 1)
                Debug.Log("YOU LOSE");
            else
                Debug.Log("YOU WIN");
            
        }



        gameEnded = true;
        Invoke("GoBackToMenu", 3.0f);
    }

    void GoBackToMenu()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel("Mainscreen");
    }

    public int GetGateIndex(GameObject gate)
    {
        return trackGates.FindIndex(x => x.GetInstanceID() == gate.GetInstanceID());
    }

    public PlayerMovement GetPlayer(int playerId)
    {
        return players.First(x => x.id == playerId);
    }
}
