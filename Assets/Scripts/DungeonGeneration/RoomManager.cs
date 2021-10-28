using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RoomManager : MonoBehaviour
{
    public RoomState State { get; set; }

    public Room Room { get; set; }

    
    public void Start()
    {
        Room.onSwitchRoomState += OnSwitchRoomState;
        if (Room.Type != RoomType.Spawn)
        {
            Room.SetRoomState(RoomState.UnCleared);
        }
            

    }

    public void OnDisable()
    {
        Room.onSwitchRoomState -= OnSwitchRoomState;
    }

    public void OnRoomEnter(GameObject gameObject)
    {
        DungeonManager.GetInstance().currentRoom = Room.roomObject;
        if(Room.currentState == RoomState.UnCleared)
        {
            StartCoroutine(FightSimulation());
        }
    }

    private void OnSwitchRoomState()
    {
        switch (Room.currentState)
        {
            case RoomState.Cleared:
                Room.roomObject.ExitTimeLines.ForEach(t => t.gameObject.SetActive(true));
                Room.roomObject.doors.ForEach(d => d.gameObject.GetComponent<Collider2D>().enabled = true);
                break;
            case RoomState.UnCleared:
                Room.roomObject.ExitTimeLines.ForEach(t => t.gameObject.SetActive(false));
                Room.roomObject.doors.ForEach(d => d.gameObject.GetComponent<Collider2D>().enabled = false);
                break;
            case RoomState.Active:
                break;
        }
    }

    IEnumerator FightSimulation()
    {
        Room.SetRoomState(RoomState.Active);      
        yield return new WaitForSeconds(4f);
        Room.SetRoomState(RoomState.Cleared);
    }

    public void OnRoomExit(GameObject gameObject)
    {
        
    }
}
