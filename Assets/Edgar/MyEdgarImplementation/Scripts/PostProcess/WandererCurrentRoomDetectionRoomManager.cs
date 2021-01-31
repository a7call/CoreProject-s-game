using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Edgar.Unity.Examples
{
    public class WandererCurrentRoomDetectionRoomManager : MonoBehaviour
    {
        public RoomInstance RoomInstance;
        /// <summary>
        /// Gets called when a player enters the room.
        /// </summary>
        /// <param name="player"></param>
        public void OnRoomEnter(GameObject player)
        {
            Debug.Log($"Room enter. Room name: {RoomInstance.Room.GetDisplayName()}, Room template: {RoomInstance.RoomTemplatePrefab.name}");
            WandererGameManager.Instance.OnRoomEnter(RoomInstance);
        }
        /// <summary>
        /// Gets called when a player leaves the room.
        /// </summary>
        /// <param name="player"></param>
        public void OnRoomLeave(GameObject player)
        {
            Debug.Log($"Room leave {RoomInstance.Room.GetDisplayName()}");
            WandererGameManager.Instance.OnRoomLeave(RoomInstance);
        }
    }
}

