using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;
using System;
using Wanderer.Utils;


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

        public bool shouldSpawnMonsters;
        

        public void Start()
        {
            roomInstance = GetComponent<RoomInfo>()?.RoomInstance;
            room = roomInstance?.Room as WandererRoom;
            
        }

        
        public void Update()
        {
            
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
            StartCoroutine(RoomRandomSpawn());
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

        public List<GameObject> monsters = new List<GameObject>();
        private List<GameObject> activeMonsters = new List<GameObject>();
        //EnemyBase Spawn
        IEnumerator RoomRandomSpawn()
        {
            if (!shouldSpawnMonsters)
                yield break;

            var currentDifficulty = 0;
            var spawner = new Spawner(monsters);

            while(currentDifficulty <= room.DifficultyAllowed)
            {
                int index = Wanderer.Utils.Utils.RandomObjectInCollection(monsters.Count);
               
                var position = RandomPointInBounds(FloorCollider.bounds, 1f);

                if (!IsPointWithinCollider(FloorCollider, position))
                {
                    continue;
                }

                if (Physics2D.OverlapCircleAll(position, 0.5f).Any(x => !x.isTrigger))
                {
                    continue;
                }

                
                var spawnedMonster = spawner.Spawn(monsters[index], position, this.gameObject.transform);
                currentDifficulty += spawnedMonster.GetComponent<Enemy>().Difficulty;

                activeMonsters.Add(spawnedMonster);
            }
            yield return null;
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

