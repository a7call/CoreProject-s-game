using System.Linq;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


namespace Edgar.Unity.Examples
{
    [Serializable]
    public struct enemyStruct
    {
        public GameObject enemy;
        public int EnemyPoint;
    }
    public struct RoomStruct
    {
        public List<GameObject> ennemies;
        public int enemyPointsAvailable;
        public bool isEnemyAlreadySpawned;
    }
    [CreateAssetMenu(menuName = "Edgar/Wanderer/Current room detection/Post-process", fileName = "CurrentRoomDetectionPostProcess")]
    public class WandererRoomDetectionPostProcess : DungeonGeneratorPostProcessBase
    {
        public static Dictionary<RoomInstance, RoomStruct> roomDictionary = new Dictionary<RoomInstance, RoomStruct>();
        public enemyStruct[] Enemies;
        Tilemap tilemapMiniMap;
     
        public override void Run(GeneratedLevel level, LevelDescription levelDescription)
        {
            
            var tilemapss = level.RootGameObject.transform.Find("Tilemaps");
            var Rooms = level.RootGameObject.transform.Find("Rooms");

            var walls = tilemapss.transform.Find("Walls").gameObject;
            var floors = tilemapss.transform.Find("Floor").gameObject;
            UpdateLayer(walls, 10);
            UpdateLayer(floors , 14);
            Tilemap tilemapMiniMap = MinimapInit(level);
            foreach (var roomInstance in level.GetRoomInstances())
            {

                RoomStruct roomStruct = new RoomStruct();
                RoomStructInitializer(ref roomStruct);
                // Set the Random instance of the GameManager to be the same instance as we use in the generator
                if (WandererGameManager.Instance != null) 
                    WandererGameManager.Instance.Random = Random;

                var roomManager = AssignRoomComponents(roomInstance);
                AssignPointToRooms(roomInstance, roomManager, ref roomStruct);

                if (ShouldSpawnEnemy(roomInstance))
                    SpawnEnemy(roomInstance, roomManager, ref roomStruct);

                roomDictionary.Add(roomInstance, roomStruct);

                roomManager.roomStruct = roomStruct;
                roomManager.Enemies = Enemies;
            }
            AstarPath.active.Scan();
            MovePlayerToSpawn(level);
        }

        protected void AssignPointToRooms(RoomInstance roomInstance, WandererCurrentRoomDetectionRoomManager roomManager,ref RoomStruct roomStruct)
        {
            roomManager.RoomInstance = roomInstance;
            
            var room = (WandererRoom)roomInstance.Room;

            if (room.Type == RoomType.Large)
            {
                roomStruct.enemyPointsAvailable = 15;
            }
            else if (room.Type == RoomType.Medium)
            {
                roomStruct.enemyPointsAvailable = 10;
            }
            else if (room.Type == RoomType.Small)
            {
                roomStruct.enemyPointsAvailable = 5;
            }
        }

        protected void RoomStructInitializer(ref RoomStruct roomStruct)
        {
            roomStruct.isEnemyAlreadySpawned = false;
            roomStruct.ennemies = new List<GameObject>();
            roomStruct.enemyPointsAvailable = 0;
        }

        protected WandererCurrentRoomDetectionRoomManager AssignRoomComponents(RoomInstance roomInstance)
        {
            var roomTemplateInstance = roomInstance.RoomTemplateInstance;
            // Find floor tilemap layer
            var room = (WandererRoom)roomInstance.Room;

            var tilemaps = RoomTemplateUtils.GetTilemaps(roomTemplateInstance);
            roomInstance.RoomTemplateInstance.layer = 19;

            var floor = tilemaps.Single(x => x.name == "Floor").gameObject;
            // Add floor collider

            // Add the room manager component
            var roomManager = roomTemplateInstance.AddComponent<WandererCurrentRoomDetectionRoomManager>();

            roomManager.FloorCollider = floor.GetComponent<CompositeCollider2D>();

            floor.AddComponent<WandererCurrentRoomDetectionTriggerhandler>();

            return roomManager;
        }
       
        Tilemap MinimapInit(GeneratedLevel level)
        {       
                var tilemapsRoot = level.RootGameObject.transform.Find(GeneratorConstants.TilemapsRootName);
                var tilemapObject = new GameObject("Minimap");
                tilemapObject.transform.SetParent(tilemapsRoot);
                tilemapObject.transform.localPosition = Vector3.zero;
                var tilemap = tilemapObject.AddComponent<Tilemap>();
                var tilemapRenderer = tilemapObject.AddComponent<TilemapRenderer>();
                tilemapRenderer.sortingOrder = 20;
                tilemapMiniMap = tilemap;


                // TODO: check that the layer exists
                // Assign special layer


                tilemapObject.layer = 17;
                return tilemap; 
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
                    var spawnPosition = GameObject.FindGameObjectWithTag("SpawnPosition");
                    var player = GameObject.FindGameObjectWithTag("Player");
                    Debug.Log(player.transform.position);
                    player.transform.position = spawnPosition.transform.position;
                    Debug.Log(player.transform.position);
                }
            }
        }
        
        private bool ShouldSpawnEnemy(RoomInstance roomInstance)
        {
            var room = (WandererRoom)roomInstance.Room;
            if (roomInstance.IsCorridor)
            {
                foreach (var roomInstanceNextToAcualRoom in roomInstance.Doors.Select(x => x.ConnectedRoomInstance))
                {
                   var roomNextToAcualRoom = (WandererRoom)roomInstanceNextToAcualRoom.Room;
                   if(roomNextToAcualRoom.Type == RoomType.Spawn)
                        return false;   
                }
            }
               
            return roomInstance.isEnemyAlreadySpawned == false && room.Type != RoomType.Spawn;
        }

        private void SpawnEnemy(RoomInstance roomInstance, WandererCurrentRoomDetectionRoomManager roomManager, ref RoomStruct roomStruct)
        {
            // Enemy are set to already spawned
            roomStruct.isEnemyAlreadySpawned = true;
            // sécurité si boucle infini
            int iteration = 0;
            var room = roomInstance.RoomTemplateInstance;
            // PointsInTheCurrentRoom
            int currentRoomPoint = 0;
            roomManager = room.GetComponent<WandererCurrentRoomDetectionRoomManager>();
            do
            {
                iteration++;
                // check if enemyPos is in bounds
                var position = WandererCurrentRoomDetectionRoomManager.RandomPointInBounds(roomManager.FloorCollider.bounds, 1f);
                
                if (!WandererCurrentRoomDetectionRoomManager.IsPointWithinCollider(roomManager.FloorCollider, position))
                {
                    continue;
                }

                if (Physics2D.OverlapCircleAll(position, 0.5f).Any(x => !x.isTrigger))
                {
                    continue;
                }
                
                // Get Random Enemy in Array
                var enemyStruct= Enemies[UnityEngine.Random.Range(0, Enemies.Length)];
                
                // spawn enemy if points are available 
                if (currentRoomPoint + enemyStruct.EnemyPoint <= roomStruct.enemyPointsAvailable)
                {
                  
                    var enemyGO = Instantiate(enemyStruct.enemy);
                    enemyGO.transform.position = position;
                    enemyGO.transform.parent = roomInstance.RoomTemplateInstance.transform;
                    roomStruct.ennemies.Add(enemyGO);
                    currentRoomPoint += enemyStruct.EnemyPoint;
                }

            } while (iteration < 100 || currentRoomPoint < roomStruct.enemyPointsAvailable ) ;
        }

        protected void UpdateLayer(GameObject map, int layer)
        {
            map.layer = layer;
        }
    }
}

