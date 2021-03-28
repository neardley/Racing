using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Photon.Pun;

public class UI_Control : MonoBehaviour
{
    [Header("End Display")]
    [SerializeField] Transform winDisplaytf;
    [SerializeField] Transform loseDisplaytf;

    [Header("Speed Display")]
    [SerializeField] Transform speedDisplaytf;
    TMP_Text speedDisplay;

    [Header("Place Display")]
    [SerializeField] Transform placeDisplaytf;
    TMP_Text placeDisplay;

    [Header("Lap Display")]
    [SerializeField] Transform lapdisplaytf;
    TMP_Text lapDisplay;

    [Header("Time Display")]
    [SerializeField] Transform timeDisplaytf;
    TMP_Text timeDisplay;

    [Header("Player")]
    Transform player;
    Rigidbody playerRB;
    PlayerMovement playerScript;

    int totalGates;

    // Start is called before the first frame update
    void Start()
    {
        speedDisplay = speedDisplaytf.GetComponent<TMP_Text>();
        placeDisplay = placeDisplaytf.GetComponent<TMP_Text>();
        lapDisplay = lapdisplaytf.GetComponent<TMP_Text>();
        timeDisplay = timeDisplaytf.GetComponent<TMP_Text>();
        totalGates = Game_Control.instance.trackGates.Count;
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            //convert meters per sec to mph
            speedDisplay.text = Math.Round(playerRB.velocity.magnitude * 2.23694).ToString() + " mph";

            lapDisplay.text = "Gate:" + (playerScript.lastGateIndex + 1).ToString() + "/" + totalGates.ToString();
            /*
            Double currentTime = playerScript.ShowStartTime() - PhotonNetwork.Time;
            int minutes = (int)(Math.Floor(currentTime) / 60);
            int seconds = (int)(Math.Floor(currentTime) % 60);
            timeDisplay.text = minutes.ToString() + " : " + seconds.ToString(); 
            */
        }
    }

    public void AssignTarget(GameObject player)
    {
        this.player = player.transform;
        playerRB = player.GetComponent<Rigidbody>();
        playerScript = player.GetComponent<PlayerMovement>();
    }


    public void DisplayEnd(bool isWinner)
    {
        if (isWinner)
        {
            winDisplaytf.gameObject.SetActive(true);
        }
        else
        {
            loseDisplaytf.gameObject.SetActive(true);
        }
    }
}
