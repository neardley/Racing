using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UI_Control : MonoBehaviour
{
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

    // Start is called before the first frame update
    void Start()
    {
        speedDisplay = speedDisplaytf.GetComponent<TMP_Text>();
        placeDisplay = placeDisplaytf.GetComponent<TMP_Text>();
        lapDisplay = lapdisplaytf.GetComponent<TMP_Text>();
        timeDisplay = timeDisplaytf.GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            //convert meters per sec to mph
            speedDisplay.text = Math.Round(playerRB.velocity.magnitude * 2.23694).ToString() + " mph";
        }
    }

    public void AssignTarget(GameObject player)
    {
        this.player = player.transform;
        playerRB = player.GetComponent<Rigidbody>();
    }
}
