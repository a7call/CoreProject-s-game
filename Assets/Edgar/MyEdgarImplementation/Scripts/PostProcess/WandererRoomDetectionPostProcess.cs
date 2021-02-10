using System.Linq;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


namespace Edgar.Unity.Examples
{
    [CreateAssetMenu(menuName = "Edgar/Wanderer/Current room detection/Post-process", fileName = "CurrentRoomDetectionPostProcess")]
    public class WandererRoomDetectionPostProcess : DungeonGeneratorPostProcessBase
    {


        public GameObject[] Enemies;

        public override void Run(GeneratedLevel level, LevelDescription levelDescription)
        {
            
            MovePlayerToSpawn(level);
            var tilemapss = level.RootGameObject.transform.Find("Tilemaps");
            
            var walls = tilemapss.transform.Find("Walls").gameObject;
            var floors = tilemapss.transform.Find("Floor").gameObject;
            AddLayerToWall(walls);
            AddLayerToFloor(floors);
           
            foreach (var roomInstance in level.GetRoomInstances())
            {
                var roomTemplateInstance = roomInstance.RoomTemplateInstance;
                // Find floor tilemap layer
                var room = (WandererRoom)roomInstance.Room;
                var tilemaps = RoomTemplateUtils.GetTilemaps(roomTemplateInstance);
               
                
                
                var floor = tilemaps.Single(x => x.name == "Floor").gameObject;
                // Add floor collider
              
                // Add the room manager component
                var roomManager = roomTemplateInstance.AddComponent<WandererCurrentRoomDetectionRoomManager>();
                roomManager.TileMap = tilemapss;
                
                BlackenMap(tilemapss, roomInstance);
                roomManager.RoomInstance = roomInstance;
                
               
                     
                    // Add current room detection handler
                    floor.AddComponent<WandererCurrentRoomDetectionTriggerhandler>();

                if (WandererGameManager.Instance != null)
                {
                    // Set the Random instance of the GameManager to be the same instance as we use in the generator
                    WandererGameManager.Instance.Random = Random;
                }
               
                
               

                if (room.Type != RoomType.Corridor)
                {
                    // Set enemies and floor collider to the room manager
                    roomManager.Enemies = Enemies;
                    roomManager.FloorCollider = floor.GetComponent<CompositeCollider2D>();

                    // Find all the doors of neighboring corridors and save them in the room manager
                    // The term "door" has two different meanings here:
                    //   1. it represents the connection point between two rooms in the level
                    //   2. it represents the door game object that we have inside each corridor
                    foreach (var door in roomInstance.Doors)
                    {
                        // Get the room instance of the room that is connected via this door
                        var corridorRoom = door.ConnectedRoomInstance;

                        // Get the room template instance of the corridor room
                        var corridorGameObject = corridorRoom.RoomTemplateInstance;

                        // Find the door game object by its name
                        var doorsGameObject = corridorGameObject.transform.Find("Door")?.gameObject;

                        // Each corridor room instance has a connection that represents the edge in the level graph
                        // We use the connection object to check if the corridor should be locked or not
                        var connection = (WandererConnection)corridorRoom.Connection;

                        if (doorsGameObject != null)
                        {
                            // If the connection is locked, we set the Locked state and keep the game object active
                            // Otherwise we set the EnemyLocked state and deactivate the door. That means that the door is active and locked
                            // only when there are enemies in the room.
                            if (connection.IsLocked)
                            {
                                doorsGameObject.GetComponent<WandererDoors>().State = WandererDoors.DoorState.Locked;
                            }
                            else
                            {
                                doorsGameObject.GetComponent<WandererDoors>().State = WandererDoors.DoorState.EnemyLocked;
                                doorsGameObject.SetActive(false);
                            }

                            roomManager.Doors.Add(doorsGameObject);
                        }
                    }
                }
            }
            AstarPath.active.Scan();
            SetupFogOfWar(level);
           
        }
       

        void BlackenMap(Transform tilemaps, RoomInstance roomInstance)
        {
            var room = (WandererRoom)roomInstance.Room;
            var roomTemplateInstance = roomInstance.RoomTemplateInstance;
            var roomManager = roomTemplateInstance.AddComponent<WandererCurrentRoomDetectionRoomManager>();
          

            var tilemapGoblal = RoomTemplateUtils.GetTilemaps(tilemaps.gameObject);
            foreach (Tilemap tilemap in tilemapGoblal)
            {

                foreach (Vector3 tileGlobal in roomManager.getTheTiles(tilemap))
                {

                    tilemap.SetTileFlags(tilemap.WorldToCell(tileGlobal), TileFlags.None);
                    tilemap.SetColor(tilemap.WorldToCell(tileGlobal), Color.black);

                }
            }
        }

        private void SetupFogOfWar(GeneratedLevel level)
        {
            // To setup the FogOfWar component, we need to get the root game object that holds the level.
            var generatedLevelRoot = level.RootGameObject;

            // If we use the Wave mode, we must specify the point from which the wave spreads as we reveal a room.
            // The easiest way to do so is to get the player game object and use its transform as the wave origin.
            // Change this line if your player game object does not have the "Player" tag.
            var player = GameObject.FindGameObjectWithTag("Player");

            // Now we can setup the FogOfWar component.
            // To make it easier to work with the component, the class is a singleton and provides the Instance property.
            FogOfWar.Instance?.Setup(generatedLevelRoot, player.transform);

            // After the level is generated, we usually want to reveal the spawn room.
            // To do that, we have to find the room instance that corresponds to the Spawn room.
            // In this example, the spawn room has the GungeonRoomType.Entrance type.
            var spawnRoom = level
                .GetRoomInstances()
                .SingleOrDefault(x => ((WandererRoom)x.Room).Type == RoomType.Spawn);

            if (spawnRoom == null)
            {
                throw new InvalidOperationException("There must be exactly one room with the name 'Spawn' for this example to work.");
            }
            // When we have the spawn room instance, we can reveal the room from the fog.
            // We use revealImmediately: true so that the first room is revealed instantly,
            // but it is optional.
            FogOfWar.Instance?.RevealRoom(spawnRoom, revealImmediately: true);
        }

        private void MovePlayerToSpawn(GeneratedLevel level)
        {
            foreach (var roomInstance in level.GetRoomInstances())
            {
                var room = (WandererRoom)roomInstance.Room;
                var roomTemplateInstance = roomInstance.RoomTemplateInstance;

                // Get spawn position if Entrance
                if (room.Type == RoomType.Spawn)
                {

                    var spawnPosition = roomTemplateInstance.transform.Find("SpawnPosition");
                    var player = GameObject.FindWithTag("Player");
                    player.transform.position = spawnPosition.position;
                }
            }
        }

        protected void AddLayerToWall(GameObject wall)
        {
            wall.layer = 10;
            Debug.Log(wall.layer);
        }
        protected void AddLayerToFloor(GameObject floor)
        {
            floor.layer = 14;
        }
    }
}

