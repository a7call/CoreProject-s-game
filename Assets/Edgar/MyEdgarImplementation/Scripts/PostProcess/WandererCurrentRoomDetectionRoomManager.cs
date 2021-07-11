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
        public List<WandererCurrentRoomDetectionRoomManager> connectedRoomDetectionManagers = new List<WandererCurrentRoomDetectionRoomManager>();
        public GameObject WandererObject;

        public GameObject doors;
        


        private bool isActive;

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

        public void OnRoomStateSwitch()
        {
            switch (room.roomState)
            {
                case RoomState.Cleared:
                    
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




        public void Start()
        {     
            if(roomInstance.IsCorridor)
                doors = gameObject.transform.Find("Tilemaps").Find("Additionnal Layer 1").Find("Doors").gameObject;

            GetConnectedRoom();
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
                room.doors.GetComponent<DoorManagement>().CloseDoors();
            }
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
            if (room.Type != RoomType.Corridor && room.Type != RoomType.Spawn && room.roomState != RoomState.Cleared)
                room.SethRoomState(RoomState.CurrentlyUsed);
               
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
            if (roomInstance.IsCorridor) previousCorridor = roomInstance;

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

