using System;
using System.Collections;
using System.Collections.Generic;
using Characters.Scripts;
using UnityEngine;

public class RoomManager : MonoBehaviour
{

    public GameObject roomRespawnPoint;
    private int initialRoomHp;
    private GameObject player;
    public Vector2 respawnPointVector;

    //get Player instance in scene
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Sets a temporary int to the value of the health of the player as they enter a room. 
    private void SetInitialRoomHp()
    {
        initialRoomHp = player.GetComponentInChildren<Health>().currentHp;
        Debug.Log("The initial PlayerHP for: " + this.gameObject.name + " Has been to: " + initialRoomHp);
        
    }

    //Sets new respawnPoint for the newly entered room based on child object
    private void OnTriggerEnter2D(Collider2D other)
    {
        respawnPointVector = roomRespawnPoint.transform.position;
        Debug.Log("New Respawn Point Set: " + respawnPointVector);
        SetPlayerRespawnPoint();
    }
    
    //Sends vector cords to Health script of Player
    private void SetPlayerRespawnPoint()
    {
        player.GetComponentInChildren<Health>().updatedRespawnPoint = respawnPointVector;
    }
}