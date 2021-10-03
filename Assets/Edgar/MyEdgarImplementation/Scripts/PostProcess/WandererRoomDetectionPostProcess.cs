using System.Linq;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Assets.Scripts.Game;


namespace Edgar.Unity.Examples
{
    
    [CreateAssetMenu(menuName = "Edgar/Wanderer/Current room detection/Post-process", fileName = "CurrentRoomDetectionPostProcess")]
    public class WandererRoomDetectionPostProcess : DungeonGeneratorPostProcessBase
    {
        public List<GameObject> monsters = new List<GameObject>();
        #region MainBoucle
        public override void Run(GeneratedLevel level, LevelDescription levelDescription)
        {

            var tilemaps = level.RootGameObject.transform.Find("Tilemaps");

            SetFogOFWar(tilemaps);
           // tilemaps.transform.position = Vector3.zero;
            var Rooms = level.RootGameObject.transform.Find("Rooms");
            Rooms.transform.position = new Vector3(Rooms.transform.position.x, Rooms.transform.position.y, 0);

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
                var room = (WandererRoom)roomInstance.Room;

                room.ResetRoom();

                if (room.Type == RoomType.Corridor && room.Type == RoomType.Spawn)
                {
                    room.SetRoomState(RoomState.Cleared);
                }
                else
                {
                    room.monsters = monsters;
                    room.SetRoomState(RoomState.UnCleared); 
                }                
            }           
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
            room.FloorCollider = floor.GetComponent<CompositeCollider2D>();

            floor.AddComponent<WandererCurrentRoomDetectionTriggerhandler>();

            return roomManager;
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

        void SetFogOFWar(Transform tilemaps)
        {
            GameObject fog = GameObject.FindGameObjectWithTag("FOG");
            if (fog != null)
            {
                Tilemap floorTileMap = tilemaps.Find("Floor").GetComponent<Tilemap>();
                Tilemap WallTileMap = tilemaps.Find("BackGroundWall").GetComponent<Tilemap>();
                List<Tilemap> levelTilemaps = new List<Tilemap>();
                levelTilemaps.Add(WallTileMap);
                levelTilemaps.Add(floorTileMap);
                fog.GetComponentInChildren<FogVal>().GetAllLevlesTiles(levelTilemaps);
            }
        }

    }
}
