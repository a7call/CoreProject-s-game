using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
                    NeiborsToLight(roomInstance);
                    FogOfWar.Instance?.RevealRoomAndNeighbors(roomInstance);
                }
             
            }
        }

        void NeiborsToLight(RoomInstance room)
        {
            var roomsToLight = new List<RoomInstance>()
            {
              room
            };

            foreach (var roomToExplore in room.Doors.Select(x => x.ConnectedRoomInstance))
            {
                roomsToLight.Add(roomToExplore);
            }

            LightNeibors(roomsToLight);
        }

      

        void LightNeibors(List<RoomInstance> roomsToLight)
        {
            foreach (RoomInstance room in roomsToLight)
            {
                if (room.RoomTemplateInstance.transform.Find("LightContainer") != null)
                {
                    Transform LigthContainerObject = room.RoomTemplateInstance.transform.Find("LightContainer");

                    foreach (Transform child in LigthContainerObject.transform)
                    {
                        if (!child.gameObject.activeSelf)
                        {
                            child.gameObject.SetActive(true);
                        }
                    }
                }
            }
        }
        public void OnTriggerExit2D(Collider2D otherCollider)
        {
            if (otherCollider.gameObject.tag == "Player")
            {
                roomManager?.OnRoomLeave(otherCollider.gameObject);
                if (roomInstance.IsCorridor)
                {
                }
            }
        }
    }
}

