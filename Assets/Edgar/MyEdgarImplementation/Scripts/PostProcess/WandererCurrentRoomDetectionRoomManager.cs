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

        public List<WandererCurrentRoomDetectionRoomManager> connectedRoomDetectionManagers = new List<WandererCurrentRoomDetectionRoomManager>();

        List<GameObject> instansiatedMonster = new List<GameObject>();


        public Transform doorsHandler;

        #region Unity Mono
        private void OnEnable()
        {
            roomInstance = GetComponent<RoomInfo>()?.RoomInstance;
            room = roomInstance?.Room as WandererRoom;
            room.onSwitchRoomState += OnRoomStateSwitch;
        }


        private void OnDisable()
        {
            room.onSwitchRoomState -= OnRoomStateSwitch;
        }



        public void Start()
        {
            GetDoorHandlerInCorridors();
            GetConnectedRoom();
        }
        #endregion

        #region State Management
        public void OnRoomStateSwitch()
        {
            switch (room.roomState)
            {
                case RoomState.Cleared:
                    OpenRoom();
                    break;

                case RoomState.UnCleared:
                    room.isAllowedToSpawnMonsters(room.Type);
                    room.SetRoomDifficulty(room.Type);
                    room.SetChanceToSpawn(room.Type);

                    if (room.ShouldSpawnMonsters)
                    {
                        //isAllowedToSpawnMonstersTwice();
                        room.RoomRandomSpawnSetUp();
                    }
                    break;

                case RoomState.CurrentlyUsed:
                    //Init Combat phase
                    StartCoroutine(RoomSpawn());
                    CloseRoom();
                    break;
            }
        }
        #endregion

        #region Doors Handling
        void GetDoorHandlerInCorridors()
        {
            if (roomInstance.IsCorridor)
            {
                doorsHandler = gameObject.transform.Find("Tilemaps").Find("Additionnal Layer 1").Find("Doors");
                if (doorsHandler == null)
                    Debug.LogError("One corridor miss some doors");
            }     
        }

        void GetConnectedRoom()
        {
            foreach (var door in roomInstance.Doors)
            {
              connectedRoomDetectionManagers.Add(door.ConnectedRoomInstance.RoomTemplateInstance.GetComponent<WandererCurrentRoomDetectionRoomManager>());
            }
        }
        void CloseRoom()
        {
            foreach(var room in connectedRoomDetectionManagers)
            {
                room.doorsHandler.GetComponent<DoorManagement>().CloseDoors();
            }
        }
        void OpenRoom()
        {
            foreach (var room in connectedRoomDetectionManagers)
            {
                room.doorsHandler.GetComponent<DoorManagement>().OpenDoors();
            }
        }
        #endregion;

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
            if (room.Type != RoomType.Corridor && room.Type != RoomType.Spawn && room.roomState != RoomState.Cleared)
                room.SetRoomState(RoomState.CurrentlyUsed);     
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

        #region Ennemies Room Management
        
        IEnumerator RoomSpawn()
        {

            foreach (var monsterObj in room.ActiveMonsters)
            {
                var spawnedMonster = room.spawner.Spawn(monsterObj.Item1, monsterObj.Item2, this.gameObject.transform);
                instansiatedMonster.Add(spawnedMonster);
                spawnedMonster.GetComponent<Enemy>().onEnemyDeath += ClearDeadMonsters;
            }
            yield return null;
        }
        public void ClearDeadMonsters(GameObject monster)
        {
            foreach (var monsterObj in instansiatedMonster.ToArray())
            {
                if (monsterObj == monster)
                {
                    instansiatedMonster.Remove(monsterObj);
                    monster.GetComponent<Enemy>().onEnemyDeath -= ClearDeadMonsters;
                }
            }

            if (instansiatedMonster.Count <= 0 &&  room.roomState != RoomState.Cleared)
                room.SetRoomState(RoomState.Cleared);
        }
        #endregion


    }
    
}

