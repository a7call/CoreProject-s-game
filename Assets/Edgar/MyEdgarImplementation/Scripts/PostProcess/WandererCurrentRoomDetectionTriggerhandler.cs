using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Edgar.Unity.Examples
{
    public class WandererCurrentRoomDetectionTriggerhandler : MonoBehaviour
    {
        private WandererCurrentRoomDetectionRoomManager roomManager;
        public void Start()
        {
            roomManager = transform.parent.parent.gameObject.GetComponent<WandererCurrentRoomDetectionRoomManager>();
        }
        public void OnTriggerEnter2D(Collider2D otherCollider)
        {
            if (otherCollider.gameObject.tag == "Player")
            {
                roomManager?.OnRoomEnter(otherCollider.gameObject);
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

