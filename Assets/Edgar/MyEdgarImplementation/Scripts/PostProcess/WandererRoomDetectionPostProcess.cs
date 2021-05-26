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
        public List<GameObject> monsters;
        #region MainBoucle
        public override void Run(GeneratedLevel level, LevelDescription levelDescription)
        {

            var tilemaps = level.RootGameObject.transform.Find("Tilemaps");
            var Rooms = level.RootGameObject.transform.Find("Rooms");
            #region SetLayers
            var foreGroundWall = tilemaps.transform.Find("ForeGroundWall").gameObject;
            var BackGroundWall = tilemaps.transform.Find("BackGroundWall").gameObject;
            var floors = tilemaps.transform.Find("Floor").gameObject;
            UpdateLayer(foreGroundWall, 10);
            UpdateLayer(BackGroundWall, 10);
            UpdateLayer(floors, 14);
            #endregion
            Tilemap tilemapMiniMap = MinimapInit(level);
            foreach (var roomInstance in level.GetRoomInstances())
            {
                // Set the Random instance of the GameManager to be the same instance as we use in the generator
                if (WandererGameManager.Instance != null)
                    WandererGameManager.Instance.Random = Random;

                var roomManager = AssignRoomComponents(roomInstance);
                roomManager.RoomInstance = roomInstance;

                roomManager.shouldSpawnMonsters = ShouldSpawnMonsters(roomInstance);
                
                if (roomManager.shouldSpawnMonsters)
                {
                   roomManager.monsters = monsters;
                }
              
            }
            // AstarPath.active.Scan();
            FindObjectOfType<NodeGrid>().CreateGrid();
            MovePlayerToSpawn(level);
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

        //protected void RoomStructInitializer(ref RoomStruct roomStruct)
        //{
        //    roomStruct.isEnemyAlreadySpawned = false;
        //    roomStruct.ennemies = new List<GameObject>();
        //    roomStruct.enemyPointsAvailable = 0;
        //    roomStruct.shouldHaveSecondSpawn = false;
        //}
        //#endregion


    
        private bool ShouldSpawnMonsters(RoomInstance roomInstance)
        {
            var room = (WandererRoom)roomInstance.Room;
            return (room.Type == RoomType.Large || room.Type == RoomType.Medium || room.Type == RoomType.Small);
        }

        //private void SetSecondSpawn(ref RoomStruct roomStruct)
        //{
        //    roomStruct.shouldHaveSecondSpawn = UnityEngine.Random.Range(0f, 1f) >= 0.9f? true : false; 
        //}

        //public static void SpawnEnemy(RoomInstance roomInstance, WandererCurrentRoomDetectionRoomManager roomManager, ref RoomStruct roomStruct, EnemyStruct[] Enemies, bool isSecondSpawn = false)
        //{
        //    if (isSecondSpawn)
        //    {
        //        roomStruct.shouldHaveSecondSpawn = false;
        //    }
        //    else
        //    {
        //        roomStruct.isEnemyAlreadySpawned = true;
        //    }
        //    // s�curit� si boucle infini
        //    int iteration = 0;
        //    var room = roomInstance.RoomTemplateInstance;
        //    // PointsInTheCurrentRoom
        //    int currentRoomPoint = 0;
        //    roomManager = room.GetComponent<WandererCurrentRoomDetectionRoomManager>();
        //    do
        //    {
        //        iteration++;
        //        // check if enemyPos is in bounds
        //        var position = WandererCurrentRoomDetectionRoomManager.RandomPointInBounds(roomManager.FloorCollider.bounds, 1f);

        //        if (!WandererCurrentRoomDetectionRoomManager.IsPointWithinCollider(roomManager.FloorCollider, position))
        //        {
        //            continue;
        //        }

        //        if (Physics2D.OverlapCircleAll(position, 0.5f).Any(x => !x.isTrigger))
        //        {
        //            continue;
        //        }

        //        // Get Random Enemy in Array
        //        var enemyStruct = Enemies[UnityEngine.Random.Range(0, Enemies.Length)];

        //        // spawn enemy if points are available 
        //        if (currentRoomPoint + enemyStruct.EnemyPoint <= roomStruct.enemyPointsAvailable)
        //        {

        //            var enemyGO = Instantiate(enemyStruct.enemy);
        //            enemyGO.transform.position = position;
        //            enemyGO.transform.parent = roomInstance.RoomTemplateInstance.transform;
        //            roomStruct.ennemies.Add(enemyGO);
        //            currentRoomPoint += enemyStruct.EnemyPoint;
        //        }

        //    } while (iteration < 100 || currentRoomPoint < roomStruct.enemyPointsAvailable);
        //}

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
