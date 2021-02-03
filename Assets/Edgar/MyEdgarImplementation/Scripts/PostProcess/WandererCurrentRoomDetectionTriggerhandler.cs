using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Edgar.Unity.Examples
{
    public class WandererCurrentRoomDetectionTriggerhandler : MonoBehaviour
    {
        private WandererCurrentRoomDetectionRoomManager roomManager;
        private RoomInstance roomInstance;
        public void Start()
        {
            var parent = transform.parent.parent;
            roomInstance = parent.gameObject.GetComponent<RoomInfo>().RoomInstance;
            roomManager = parent.gameObject.GetComponent<WandererCurrentRoomDetectionRoomManager>();
        }
        public void OnTriggerEnter2D(Collider2D otherCollider)
        {
            if (otherCollider.gameObject.tag == "Player")
            {
                roomManager?.OnRoomEnter(otherCollider.gameObject);

                // Handle Fog of War
                if (roomInstance.IsCorridor)
                {
                    FogOfWar.Instance?.RevealRoomAndNeighbors(roomInstance);
                }
            }
        }
        public void OnTriggerExit2D(Collider2D otherCollider)
        {
            if (otherCollider.gameObject.tag == "Player")
            {
                roomManager?.OnRoomLeave(otherCollider.gameObject);
            }
        }
    }
}

