using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;
using System;
using Wanderer.Utils;
using Assets.Scripts.Game;

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
        public Tile animatedDoor;
        public Tile closedDoor;
        public List<Vector3Int> listOfDoorsPos = new List<Vector3Int>();
        private Tilemap doorTilemap;


        private bool isActive;
        

        public void Start()
        {
            roomInstance = GetComponent<RoomInfo>()?.RoomInstance;
            room = roomInstance?.Room as WandererRoom;
            Transform Doors = gameObject.transform.Find("Tilemaps").Find("Doors");
            if (Doors != null)
            {
                doorTilemap = Doors.GetComponent<Tilemap>();

                listOfDoorsPos = new List<Vector3Int>();

                foreach (var pos in doorTilemap.cellBounds.allPositionsWithin)
                {
                    Vector3Int localPlace = new Vector3Int(pos.x, pos.y, pos.z);
                    Vector3 place = doorTilemap.CellToWorld(localPlace);
                    if (doorTilemap.HasTile(localPlace))
                    {
                        listOfDoorsPos.Add(localPlace);
                    }
                }
                print(listOfDoorsPos.Count);
            }
           
        }

        void ChangeToAnimTiles()
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
            isActive = true;
            Debug.Log($"Room enter. Room name: {RoomInstance.Room.GetDisplayName()}, Room template: {RoomInstance.RoomTemplatePrefab.name}");
            WandererGameManager.Instance.OnRoomEnter(RoomInstance);
            StartCoroutine(RoomSpawn());
        }
        
        /// <summary>
        /// Gets called when a player leaves the room.
        /// </summary>
        /// <param name="player"></param>
        public void OnRoomLeave(GameObject player)
        {
            isActive = false;
            WandererGameManager.Instance.OnRoomLeave(RoomInstance);
            if (!roomInstance.IsCorridor) previousRoom = roomInstance;
            if (roomInstance.IsCorridor)
            {
                Debug.LogWarning("Leaving Corridor");
                previousCorridor = roomInstance;
                foreach(var pos in listOfDoorsPos)
                {
                   
                    doorTilemap.SetTile(pos, closedDoor);
                }

            }

        }
        #endregion

        #region Ennemies
       
        IEnumerator RoomSpawn()
        {

            foreach (var monsterObj in room.ActiveMonsters)
            {
                monsterObj.Item1.GetComponent<Enemy>().onEnemyDeath += room.ClearDeadMonsters;
                var spawnedMonster = room.spawner.Spawn(monsterObj.Item1, monsterObj.Item2, this.gameObject.transform);
            }
            yield return null;
        }

        #endregion


    }
    
}

