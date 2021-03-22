﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Launcher : MonoBehaviourPunCallbacks
{
    string gameVersion = "1";


    #region MonoBehaviorPunCallsbacks Callbacks
    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMster() was called by PUN");
        PhotonNetwork.JoinRandomRoom();
    }

    #endregion

    void awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void Start()
    {
        Connect();
    }


    public void Connect()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = gameVersion;
        }
    }
  
}
