using UnityEngine;

namespace Edgar.Unity.Examples.EnterTheGungeon
{
    public class GungeonCurrentRoomHandler : MonoBehaviour
    {
        private GungeonRoomManager roomManager;
        private RoomInstance roomInstance;

        public void Start()
        {
            var parent = transform.parent.parent;
            roomManager = parent.gameObject.GetComponent<GungeonRoomManager>();
            roomInstance = parent.gameObject.GetComponent<RoomInfo>().RoomInstance;
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