using System;
using System.Collections;
using UnityEngine;


public class RoomManager : MonoBehaviour
{
    public RoomState State { get; set; }

    public Room Room { get; set; }

    public void OnEnable()
    {
        //Room.onSwitchRoomState += OnSwitchRoomState;
    }

    public void OnDisable()
    {
       // Room.onSwitchRoomState -= OnSwitchRoomState;
    }

    public void OnRoomEnter(GameObject gameObject)
    {
        DungeonManager.GetInstance().currentRoom = Room.roomObject;
    }

    private void OnSwitchRoomState()
    {
        switch (Room.currentState)
        {
            case RoomState.Cleared:
                break;
            case RoomState.UnCleared:
                break;
            case RoomState.Active:
                break;
        }
    }

    public void OnRoomExit(GameObject gameObject)
    {
        throw new NotImplementedException();
    }
}
