using System.Linq;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


namespace Edgar.Unity.Examples
{
    [Serializable]
    public struct EnemyStruct
    {
        public GameObject enemy;
        public int EnemyPoint;
    }
    public struct RoomStruct
    {
        public List<GameObject> ennemies;
        public int enemyPointsAvailable;
        public bool isEnemyAlreadySpawned;
        public bool shouldHaveSecondSpawn;
    }
    [CreateAssetMenu(menuName = "Edgar/Wanderer/Current room detection/Post-process", fileName = "CurrentRoomDetectionPostProcess")]
    public class WandererRoomDetectionPostProcess : DungeonGeneratorPostProcessBase
    {

        #region MainBoucle

        public EnemyStruct[] Enemies;

        public static Dictionary<RoomInstance, RoomStruct> roomDictionary = new Dictionary<RoomInstance, RoomStruct>();
        public override void Run(GeneratedLevel level, LevelDescription levelDescription)
        {

            var tilemapss = level.RootGameObject.transform.Find("Tilemaps");
            var Rooms = level.RootGameObject.transform.Find("Rooms");
            #region SetLayers
            var foreGroundWall = tilemapss.transform.Find("ForeGroundWall").gameObject;
            var BackGroundWall = tilemapss.transform.Find("BackGroundWall").gameObject;
            var floors = tilemapss.transform.Find("Floor").gameObject;
            UpdateLayer(foreGroundWall, 10);
            UpdateLayer(BackGroundWall, 10);
            UpdateLayer(floors, 14);
            #endregion
            Tilemap tilemapMiniMap = MinimapInit(level);
            foreach (var roomInstance in level.GetRoomInstances())
            {

                RoomStruct roomStruct = new RoomStruct();
                RoomStructInitializer(ref roomStruct);
                SetSecondSpawn(ref roomStruct);
                // Set the Random instance of the GameManager to be the same instance as we use in the generator
                if (WandererGameManager.Instance != null)
                    WandererGameManager.Instance.Random = Random;

                var roomManager = AssignRoomComponents(roomInstance);
                AssignPointToRooms(roomInstance, roomManager, ref roomStruct);

                //if (ShouldSpawnEnemy(roomInstance, roomStruct))
                //    SpawnEnemy(roomInstance, roomManager, ref roomStruct, Enemies);

                roomDictionary.Add(roomInstance, roomStruct);
                roomManager.roomStruct = roomStruct;
                roomManager.Enemies = Enemies;
            }
           // AstarPath.active.Scan();
            FindObjectOfType<NodeGrid>().CreateGrid();
            MovePlayerToSpawn(level);
        }
        

        protected void AssignPointToRooms(RoomInstance roomInstance, WandererCurrentRoomDetectionRoomManager roomManager, ref RoomStruct roomStruct)
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

        protected WandererCurrentRoomDetectionRoomManager AssignRoomComponents(RoomInstance roomInstance)
        {
            var roomTemplateInstance = roomInstance.RoomTemplateInstance;
            // Find floor tilemap layer
            var room = (WandererRoom)roomInstance.Room;

            var tilemaps = RoomTemplateUtils.GetTilemaps(roomTemplateInstance);
            UpdateLayer(roomInstance.RoomTemplateInstance, 19);

            var floor = tilemaps.Single(x => x.name == "Floor").gameObject;
            // Add floor collider

            // Add the room manager component
            var roomManager = roomTemplateInstance.AddComponent<WandererCurrentRoomDetectionRoomManager>();
            

            roomManager.FloorCollider = floor.GetComponent<CompositeCollider2D>();

            floor.AddComponent<WandererCurrentRoomDetectionTriggerhandler>();

            return roomManager;
        }

        protected void RoomStructInitializer(ref RoomStruct roomStruct)
        {
            roomStruct.isEnemyAlreadySpawned = false;
            roomStruct.ennemies = new List<GameObject>();
            roomStruct.enemyPointsAvailable = 0;
            roomStruct.shouldHaveSecondSpawn = false;
        }
        #endregion


        #region EnemySpawn
        private bool ShouldSpawnEnemy(RoomInstance roomInstance, RoomStruct roomStruct)
        {
            var room = (WandererRoom)roomInstance.Room;
            if (roomInstance.IsCorridor)
            {
                foreach (var roomInstanceNextToAcualRoom in roomInstance.Doors.Select(x => x.ConnectedRoomInstance))
                {
                    var roomNextToAcualRoom = (WandererRoom)roomInstanceNextToAcualRoom.Room;
                    if (roomNextToAcualRoom.Type == RoomType.Spawn)
                        return false;
                }
            }

            return roomStruct.isEnemyAlreadySpawned == false && room.Type != RoomType.Spawn;
        }

        private void SetSecondSpawn(ref RoomStruct roomStruct)
        {
            roomStruct.shouldHaveSecondSpawn = UnityEngine.Random.Range(0f, 1f) >= 0.9f? true : false; 
        }

        public static void SpawnEnemy(RoomInstance roomInstance, WandererCurrentRoomDetectionRoomManager roomManager, ref RoomStruct roomStruct, EnemyStruct[] Enemies, bool isSecondSpawn = false)
        {
            if (isSecondSpawn)
            {
                roomStruct.shouldHaveSecondSpawn = false;
            }
            else
            {
                roomStruct.isEnemyAlreadySpawned = true;
            }
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
                var enemyStruct = Enemies[UnityEngine.Random.Range(0, Enemies.Length)];

                // spawn enemy if points are available 
                if (currentRoomPoint + enemyStruct.EnemyPoint <= roomStruct.enemyPointsAvailable)
                {

                    var enemyGO = Instantiate(enemyStruct.enemy);
                    enemyGO.transform.position = position;
                    enemyGO.transform.parent = roomInstance.RoomTemplateInstance.transform;
                    roomStruct.ennemies.Add(enemyGO);
                    currentRoomPoint += enemyStruct.EnemyPoint;
                }

            } while (iteration < 100 || currentRoomPoint < roomStruct.enemyPointsAvailable);
        }

        #endregion


        #region Player
        private void MovePlayerToSpawn(GeneratedLevel level)
        {
            
            foreach (var roomInstance in level.GetRoomInstances())
            {
                var room = (WandererRoom)roomInstance.Room;
                // Get spawn position if Entrance
                if (room.Type == RoomType.Spawn)
                {
                    var spawnPosition = GameObject.FindGameObjectWithTag("SpawnPosition");
                    var player = GameObject.FindGameObjectWithTag("Player");
                    player.transform.position = spawnPosition.transform.position;
                }
            }
        }
        #endregion


        #region Utiles
        protected void UpdateLayer(GameObject map, int layer)
        {
            map.layer = layer;
        }
        #endregion


        #region MiniMap
        Tilemap tilemapMiniMap;
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
        #endregion
    }
}
