using System;
using System.Collections;
using UnityEngine;


public class RoomManager : MonoBehaviour
{
    public RoomState State { get; set; }

    public Room Room { get; set; }

    public void OnEnable()
    {
        Room.onSwitchRoomState += OnSwitchRoomState;
    }

    public void OnDisable()
    {
        Room.onSwitchRoomState -= OnSwitchRoomState;
    }

    internal void OnRoomEnter(GameObject gameObject)
    {
        throw new NotImplementedException();
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

    internal void OnRoomExit(GameObject gameObject)
    {
        throw new NotImplementedException();
    }
}
