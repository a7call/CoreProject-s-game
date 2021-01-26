using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Edgar.Unity.Examples.EnterTheGungeon
{
    public class GungeonRoomManager : MonoBehaviour
    {
        /// <summary>
        /// Whether the room was cleared from all the enemies.
        /// </summary>
        public bool Cleared;

        /// <summary>
        /// Whether the room was visited by the player.
        /// </summary>
        public bool Visited;

        /// <summary>
        /// Doors of neighboring corridors.
        /// </summary>
        public List<GameObject> Doors = new List<GameObject>();

        /// <summary>
        /// Enemies that can spawn inside the room.
        /// </summary>
        public GameObject[] Enemies;

        /// <summary>
        /// Whether enemies were spawned.
        /// </summary>
        public bool EnemiesSpawned;

        /// <summary>
        /// Collider of the floor tilemap layer.
        /// </summary>
        public Collider2D FloorCollider;

        /// <summary>
        /// Room instance of the corresponding room.
        /// </summary>
        private RoomInstance roomInstance;

        /// <summary>
        /// Room info.
        /// </summary>
        private GungeonRoom room;

        public void Start()
        {
            roomInstance = GetComponent<RoomInfo>()?.RoomInstance;
            room = roomInstance?.Room as GungeonRoom;
        }

        public void OnRoomEnter(GameObject player)
        {
            GungeonGameManager.Instance.OnRoomEnter(roomInstance);

            if (!Visited && roomInstance != null)
            {
                Visited = true;
                UnlockDoors();
            }

            if (ShouldSpawnEnemies())
            {
                // Close all neighboring doors
                CloseDoors();

                // Spawn enemies
                SpawnEnemies();

                // Wait 3 seconds before opening doors
                // We would normally open doors after all the enemies are defeated but combat is not implemented yet
                StartCoroutine(WaitBeforeOpeningDoors(3));
            }
        }

        public void OnRoomLeave(GameObject player)
        {
            GungeonGameManager.Instance.OnRoomLeave(roomInstance);
        }

        private void SpawnEnemies()
        {
            EnemiesSpawned = true;

            var enemies = new List<GameObject>();
            var totalEnemiesCount = GungeonGameManager.Instance.Random.Next(4, 8);

            while(enemies.Count < totalEnemiesCount)
            {
                // Find random position inside floor collider bounds
                var position = RandomPointInBounds(FloorCollider.bounds, 1f);

                // Check if the point is actually inside the collider as there may be holes in the floor, etc.
                if (!IsPointWithinCollider(FloorCollider, position))
                {
                    continue;
                }

                // We want to make sure that there is no other collider in the radius of 1
                if (Physics2D.OverlapCircleAll(position, 0.5f).Any(x => !x.isTrigger))
                {
                    continue;
                }

                // Pick random enemy prefab
                var enemyPrefab = Enemies[Random.Range(0, Enemies.Length)];

                // Create an instance of the enemy and set position and parent
                var enemy = Instantiate(enemyPrefab);
                enemy.transform.position = position;
                enemy.transform.parent = roomInstance.RoomTemplateInstance.transform;
                enemies.Add(enemy);
            }
        }
        /// <summary>
        /// Wait some time before before opening doors.
        /// </summary>
        private IEnumerator WaitBeforeOpeningDoors(int seconds)
        {
            yield return new WaitForSeconds(seconds);

            Cleared = true;
            OpenDoors();
        }

        /// <summary>
        /// Close doors before we spawn enemies.
        /// </summary>
        private void CloseDoors()
        {
            foreach (var door in Doors)
            {
                if (door.GetComponent<GungeonDoor>().State == GungeonDoor.DoorState.EnemyLocked)
                {
                    door.SetActive(true);
                }
            }
        }

        /// <summary>
        /// Open doors that were closed because of enemies.
        /// </summary>
        private void OpenDoors()
        {
            foreach (var door in Doors)
            {
                if (door.GetComponent<GungeonDoor>().State == GungeonDoor.DoorState.EnemyLocked)
                {
                    door.SetActive(false);
                }
            }
        }

        /// <summary>
        /// Unlock doors that were locked because there is a shop or reward room on the other end.
        /// </summary>
        private void UnlockDoors()
        {
            if (room.Type == GungeonRoomType.Reward || room.Type == GungeonRoomType.Shop)
            {
                foreach (var door in Doors)
                {
                    if (door.GetComponent<GungeonDoor>().State == GungeonDoor.DoorState.Locked)
                    {
                        door.GetComponent<GungeonDoor>().State = GungeonDoor.DoorState.Unlocked;
                    }
                }
            }
        }

        private static bool IsPointWithinCollider(Collider2D collider, Vector2 point)
        {
            return collider.OverlapPoint(point); 
        }

        private static Vector3 RandomPointInBounds(Bounds bounds, float margin = 0)
        {
            var random = GungeonGameManager.Instance.Random;

            return new Vector3(
                Random.Range(bounds.min.x + margin, bounds.max.x - margin),
                Random.Range(bounds.min.y + margin, bounds.max.y - margin),
                Random.Range(bounds.min.z + margin, bounds.max.z - margin)
            );
        }

        /// <summary>
        /// Check if we should spawn enemies based on the current state of the room and the type of the room.
        /// </summary>
        /// <returns></returns>
        private bool ShouldSpawnEnemies()
        {
            return Cleared == false && EnemiesSpawned == false && (room.Type == GungeonRoomType.Normal || room.Type == GungeonRoomType.Hub || room.Type == GungeonRoomType.Boss);
        }
    }
}