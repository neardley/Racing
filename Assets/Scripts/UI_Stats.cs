using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UI_Stats : MonoBehaviour
{
    TMP_Text speedDisplay;

    [SerializeField] Transform player;
    Rigidbody playerRB;

    // Start is called before the first frame update
    void Start()
    {
        speedDisplay = transform.Find("Speed Display").GetComponent<TMP_Text>();
        playerRB = player.GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //convert meters per sec to mph
        speedDisplay.text = Math.Round(playerRB.velocity.magnitude* 2.23694).ToString() + " mph";
    }
}
