﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowingPlayer : MonoBehaviour
{
    GameObject player;
    float startingYposition;
    bool reset;

    /// <summary>
    /// Start moving the camera if you are this far from the player
    /// </summary>
    [SerializeField]
    float maxDistanceFromCamera = 10f;

    /// <summary>
    /// Start moving the camera if you are this far from the player
    /// </summary>
    [SerializeField]
    float resetDistanceFromCamera = 5f;

    /// <summary>
    /// Adjust the speed of camera movement
    /// </summary>
    [SerializeField]
    float speed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        // Get player object
        if(player == null)
        {
            player = GameObject.Find("Player");
        }
        // Get initial y position for the camera so that vertical movement is ignored for now 
        startingYposition = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        var pos = transform.position;
        var playerPos = player.transform.position;
        
        // Get distance from player
        var distance = Vector3.Distance(pos, playerPos);
        // Start moving camera if too far
        if (distance > maxDistanceFromCamera)
        {
            reset = true;       
            FollowPlayer(pos, playerPos, distance);
        }
        // Keep moving to reset point
        else if(reset && distance > resetDistanceFromCamera)
        {
            FollowPlayer(pos, playerPos, distance);
        }
        // Clear reset
        else if (reset)
        {
            reset = !reset;
        }
    }

    /// <summary>
    /// Move the camera target closer to the player
    /// </summary>
    /// <param name="pos">Camera Target position</param>
    /// <param name="playerPos">Player position</param>
    /// <param name="distance">Distance between the two</param>
    void FollowPlayer(Vector3 pos, Vector3 playerPos, float distance)
    {
        // Create new position to go toward
        Vector3 newPos = new Vector3(playerPos.x, startingYposition, playerPos.z);
        // Move closer to the player, speed up if farther away
        transform.position = Vector3.Lerp(pos, newPos, distance * speed / 2000);
    }
    
}