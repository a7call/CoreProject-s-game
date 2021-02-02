using System.Linq;
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
            Debug.Log(level.RootGameObject);
            var tilemapss = level.RootGameObject.transform.Find("Tilemaps");
            Debug.Log(tilemapss);
            var walls = tilemapss.transform.Find("Walls").gameObject;
            var wallsDown = tilemapss.transform.Find("WallsDown").gameObject;
            AddLayerToWall(walls);
            AddLayerToWall(wallsDown);
            foreach (var roomInstance in level.GetRoomInstances())
            {
                var roomTemplateInstance = roomInstance.RoomTemplateInstance;
                // Find floor tilemap layer
                var room = (WandererRoom)roomInstance.Room;
                var tilemaps = RoomTemplateUtils.GetTilemaps(roomTemplateInstance);
               
                
                
                var floor = tilemaps.Single(x => x.name == "Floor").gameObject;
                // Add floor collider
                AddFloorCollider(floor);
                // Add the room manager component
                var roomManager = roomTemplateInstance.AddComponent<WandererCurrentRoomDetectionRoomManager>();
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
        }
        protected void AddFloorCollider(GameObject floor)
        {
            var tilemapCollider2D = floor.AddComponent<TilemapCollider2D>();
            tilemapCollider2D.usedByComposite = true;
            var compositeCollider2d = floor.AddComponent<CompositeCollider2D>();
            compositeCollider2d.geometryType = CompositeCollider2D.GeometryType.Polygons;
            compositeCollider2d.isTrigger = true;
            compositeCollider2d.generationType = CompositeCollider2D.GenerationType.Manual;
            floor.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
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
    }
}

