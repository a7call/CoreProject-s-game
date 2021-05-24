using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;
using System;


namespace Edgar.Unity.Examples

{
    public class WandererCurrentRoomDetectionRoomManager : MonoBehaviour
    {
        /// <summary>
        /// Whether the room was cleared from all the enemies.
        /// </summary>
        public bool Cleared = false;

        /// <summary>
        /// Doors of neighboring corridors.
        /// </summary>
        public List<GameObject> Doors = new List<GameObject>();

        /// <summary>
        /// Enemies that can spawn inside the room.
        /// </summary>
        public EnemyStruct[] Enemies;

        /// <summary>
        /// room structure
        /// </summary>
        public RoomStruct roomStruct;

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
        private WandererRoom room;

        public RoomInstance RoomInstance;
        public static RoomInstance previousRoom;
        public static RoomInstance previousCorridor;
        public GameObject WandererObject;
        

        public void Start()
        {
            roomInstance = GetComponent<RoomInfo>()?.RoomInstance;
            room = roomInstance?.Room as WandererRoom;
            
        }

        
        public void Update()
        {
            // clean Enemy array when enemy is killed;
            CleanEnemyArray();
            // If proba == EnemyBase Spawn
            SecondSpawn();
            // Check if all enemy are killed if true clear = true; (room is safe)
            CheckIsRoomSafe();
        }

        #region RoomEnter && RoomLeave
        /// <summary>
        /// Gets called when a player enters the room.
        /// </summary>
        /// <param name="player"></param>
        /// 
        public void OnRoomEnter(GameObject player)
        {
            Debug.Log($"Room enter. Room name: {RoomInstance.Room.GetDisplayName()}, Room template: {RoomInstance.RoomTemplatePrefab.name}");
            WandererGameManager.Instance.OnRoomEnter(RoomInstance);
            WandererRoomDetectionPostProcess.SpawnEnemy(RoomInstance, this, ref roomStruct, Enemies, true);
            
        }
        
        /// <summary>
        /// Gets called when a player leaves the room.
        /// </summary>
        /// <param name="player"></param>
        public void OnRoomLeave(GameObject player)
        {
            WandererGameManager.Instance.OnRoomLeave(RoomInstance);
            if (!roomInstance.IsCorridor) previousRoom = roomInstance;
            if (roomInstance.IsCorridor) previousCorridor = roomInstance;

        }
        #endregion

        #region Ennemies

        float numberOfEnemyLeftInRoom = 2f;
        //EnemyBase Spawn
        void SecondSpawn()
        {
            //if (roomStruct.shouldHaveSecondSpawn && !Cleared)
            //{
            //    if (roomStruct.ennemies.Count <= numberOfEnemyLeftInRoom)
            //    {
            //        WandererRoomDetectionPostProcess.SpawnEnemy(RoomInstance, this, ref roomStruct, Enemies, true);
            //    }
            //}
        }

        void CleanEnemyArray()
        {
            if (!Cleared)
            {
                foreach (GameObject enemy in roomStruct.ennemies.ToArray())
                {
                    if (enemy == null)
                    {
                        roomStruct.ennemies.Remove(enemy);
                    }
                }
            }
        }

        void CheckIsRoomSafe()
        {
            if (!roomStruct.shouldHaveSecondSpawn && roomStruct.ennemies.Count <= 0 && !Cleared && roomStruct.isEnemyAlreadySpawned)
            {
                Cleared = true;
            }
        }
        #endregion

        #region Utiles

        public static bool IsPointWithinCollider(Collider2D collider, Vector2 point)
        {
            return collider.OverlapPoint(point);
        }

        public static Vector3 RandomPointInBounds(Bounds bounds, float margin = 0)
        {
            var random = WandererGameManager.Instance.Random;

            return new Vector3(
               UnityEngine.Random.Range(bounds.min.x + margin, bounds.max.x - margin),
                UnityEngine.Random.Range(bounds.min.y + margin, bounds.max.y - margin),
                UnityEngine.Random.Range(bounds.min.z + margin, bounds.max.z - margin)
            );
        }
        #endregion
    }
}

