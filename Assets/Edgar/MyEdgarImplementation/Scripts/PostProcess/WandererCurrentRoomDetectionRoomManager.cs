using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;
using System;


namespace Edgar.Unity.Examples
{
    public class WandererCurrentRoomDetectionRoomManager : MonoBehaviour
    {
        /// <summary>
        /// Whether the room was cleared from all the enemies.
        /// </summary>
        public bool Cleared = false;

        /// <summary>
        /// Whether the room was visited by the player.
        /// </summary>
        public bool Visited;

        public bool explored;

        /// <summary>
        /// Doors of neighboring corridors.
        /// </summary>
        public List<GameObject> Doors = new List<GameObject>();

        /// <summary>
        /// Enemies that can spawn inside the room.
        /// </summary>
        public GameObject[] Enemies;

        /// <summary>
        /// Whether enemies were spawned.
        /// </summary>
        public bool EnemiesSpawned;

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
        /// <summary>
        /// Gets called when a player enters the room.
        /// </summary>
        /// <param name="player"></param>
        
        public void OnRoomEnter(GameObject player)
        {
            Debug.Log($"Room enter. Room name: {RoomInstance.Room.GetDisplayName()}, Room template: {RoomInstance.RoomTemplatePrefab.name}");
            WandererGameManager.Instance.OnRoomEnter(RoomInstance);

           

            if (!Visited && roomInstance != null)
            {
                Visited = true;
                UnlockDoors();
            }

            if (ShouldSpawnEnemies())
            {
                // Close all neighboring doors
                CloseDoors();

                // Spawn enemies
                SpawnEnemies();

            }
           StartCoroutine(ExploreRoom());
        }
        /// <summary>
        /// Gets called when a player leaves the room.
        /// </summary>
        /// <param name="player"></param>
        public void OnRoomLeave(GameObject player)
        {
            //Debug.Log($"Room leave {RoomInstance.Room.GetDisplayName()}");
            WandererGameManager.Instance.OnRoomLeave(RoomInstance);
        }
     

        public Transform TileMap;
        private IEnumerator ExploreRoom()
        {
            List<Vector3Int> direction = new List<Vector3Int>();
            direction.Add(new Vector3Int(1, 0, 0));
            direction.Add(new Vector3Int(-1, 0, 0));
            direction.Add(new Vector3Int(0, 1, 0));
            direction.Add(new Vector3Int(0, -1, 0));
           

            if (!roomInstance.isExplored)
            {
                roomInstance.isExplored = true;
                roomInstance.RoomTemplateInstance.layer = 18;

                GameObject TilMapObj = TileMap.gameObject;
                var tilemapGoblal = RoomTemplateUtils.GetTilemaps(TilMapObj);
               
                var tilemapsRoom = RoomTemplateUtils.GetTilemaps(roomInstance.RoomTemplateInstance);
                foreach (Tilemap tilemapG in tilemapGoblal)
                {
                    foreach (Tilemap tilemapR in tilemapsRoom)
                    {
                        if (tilemapG.ToString() != tilemapR.ToString()) continue;
                        yield return new WaitForSeconds(0.001f);

                        foreach (Vector3 tileGlobal in getTheTiles(tilemapG))
                        {
                            foreach (Vector3 tileRoom in getTheTiles(tilemapR))
                            {
                                if (tileRoom == tileGlobal)
                                {
                                    tilemapG.SetTileFlags(tilemapG.WorldToCell(tileGlobal), TileFlags.None);
                                    tilemapG.SetColor(tilemapG.WorldToCell(tileGlobal), Color.white);
                                   
                                    MiniMapGestion(tilemapG, tilemapG.WorldToCell(tileGlobal));


                                    foreach (Vector3Int dir in direction)
                                    {
                                        if (!tilemapR.HasTile(tilemapR.WorldToCell(tileGlobal) + dir) && tilemapG.GetColor(tilemapG.WorldToCell(tileGlobal) + dir) != Color.white)
                                        {
                                            tilemapG.SetTileFlags(tilemapG.WorldToCell(tileGlobal + dir), TileFlags.None);
                                            tilemapG.SetColor(tilemapG.WorldToCell(tileGlobal + dir),new Color(0.35f,0.35f,0.35f,1) );
                                            
                                               
                                            
                                           
                                        }
                                         
                                    }
                                    
                                }


                            }
                        }

                    }
                }

            }
        }
        public string WallsTilemaps = "Walls";

        public string FloorTilemaps = "Floor";
        [Range(0, 1)]
        public float WallSize = 0.5f;
        public Color WallsColor = new Color(0.72f, 0.72f, 0.72f);

        public Color FloorColor = new Color(0.18f, 0.2f, 0.34f);
        Tilemap MinimapInit()
        {
            var tilemapsRoot = TileMap;
            var tilemapObject = new GameObject("Minimap");
            tilemapObject.transform.SetParent(tilemapsRoot);
            tilemapObject.transform.localPosition = Vector3.zero;
            var tilemap = tilemapObject.AddComponent<Tilemap>();
            var tilemapRenderer = tilemapObject.AddComponent<TilemapRenderer>();
            tilemapRenderer.sortingOrder = 20;

            // TODO: check that the layer exists
            // Assign special layer


            tilemapObject.layer = 17;
            return tilemap;
        }

        void MiniMapGestion(Tilemap sourceTilemap, Vector3Int tilemapPosition)
        {
            
          
                print("test");
                CopyTilesToLevelMap(sourceTilemap, tilemapPosition, MinimapInit(), CreateTileFromColor(WallsColor));
            
           
                var floorPpu = 1 / (1 + (1 - WallSize) * 2);
                CopyTilesToLevelMap(sourceTilemap, tilemapPosition, MinimapInit(), CreateTileFromColor(FloorColor, floorPpu));
           
        }

        private TileBase CreateTileFromColor(Color color, float pixelsPerUnit = 1)
        {
            var tile = ScriptableObject.CreateInstance<Tile>();

            var texture = new Texture2D(1, 1);
            texture.SetPixel(0, 0, color);
            texture.Apply();

            var sprite = Sprite.Create(texture, new Rect(0, 0, 1, 1), new Vector2(0.5f, 0.5f), pixelsPerUnit);
            tile.sprite = sprite;

            return tile;
        }

        /// <summary>
        /// Copy tiles from given source tilemaps to the level map tilemap.
        /// Instead of using the original tiles, we use a given level map tile (which is usually only a single color).
        /// If we want to copy only some of the tiles, we can provide a tile filter function.
        /// </summary>
        private void CopyTilesToLevelMap( Tilemap sourceTilemap, Vector3Int tilemapPosition ,  Tilemap levelMapTilemap, TileBase levelMapTile, Predicate<TileBase> tileFilter = null)
        {
            // Go through the tilemaps with the correct name
           
        

                    // Check if there is a tile at a given position
                    var originalTile = sourceTilemap.GetTile(tilemapPosition);
                     
                    if (originalTile != null)
                    {
                        // If a tile filter is provided, use it to check if the predicate holds
                        if (tileFilter != null)
                        {
                            if (tileFilter(originalTile))
                            {
                                levelMapTilemap.SetTile(tilemapPosition, levelMapTile);
                            }
                        }
                        // Otherwise set the levelMapTile to the correct position
                        else
                        {
                            levelMapTilemap.SetTile(tilemapPosition, levelMapTile);
                        }
                    }
        }



        public List<Vector3> getTheTiles(Tilemap tileMap)
        {

           List<Vector3> availablePlaces = new List<Vector3>();
            for (int n = tileMap.cellBounds.xMin; n < tileMap.cellBounds.xMax; n++)
            {
                for (int p = tileMap.cellBounds.yMin; p < tileMap.cellBounds.yMax; p++)
                {
                    Vector3Int localPlace = (new Vector3Int(n, p, 0));
                    Vector3 place = tileMap.CellToWorld(localPlace);
                    if (tileMap.HasTile(localPlace))
                    {
                        //Tile at "place"
                        availablePlaces.Add(place);
                    }
                    else
                    {
                        //No tile at "place"
                    }
                }
            }

            return availablePlaces;
        }

        public void Start()
        {
            roomInstance = GetComponent<RoomInfo>()?.RoomInstance;
            room = roomInstance?.Room as WandererRoom;
            if(room.Type == RoomType.Spawn && TileMap != null)
            {
                StartCoroutine(ExploreRoom());
            }
            
        }

       
        public void Update()
        {
           
        }

        public List<GameObject> enemies;
        private void SpawnEnemies()
        {
            EnemiesSpawned = true;

             enemies = new List<GameObject>();

            var totalEnemiesCount = WandererGameManager.Instance.Random.Next(4, 8);
            

            while (enemies.Count < totalEnemiesCount)
            {
                // Find random position inside floor collider bounds
                var position = RandomPointInBounds(FloorCollider.bounds, 1f);

                // Check if the point is actually inside the collider as there may be holes in the floor, etc.
                if (!IsPointWithinCollider(FloorCollider, position))
                {
                    continue;
                }

                // We want to make sure that there is no other collider in the radius of 1
                if (Physics2D.OverlapCircleAll(position, 0.5f).Any(x => !x.isTrigger))
                {
                    continue;
                }

                // Pick random enemy prefab
                var enemyPrefab = Enemies[UnityEngine.Random.Range(0, Enemies.Length)];

                // Create an instance of the enemy and set position and parent
                var enemy = Instantiate(enemyPrefab);
                enemy.transform.position = position;
                enemy.transform.parent = roomInstance.RoomTemplateInstance.transform;
                enemies.Add(enemy);
            }
            StartCoroutine(OpeningDoors(enemies));
        }
        /// <summary>
        /// Wait some time before before opening doors.
        /// </summary>
        private IEnumerator OpeningDoors(List<GameObject> ennemies)
        {
            do
            {
                yield return new WaitForEndOfFrame();
                int count = 0;
                foreach(GameObject enemy in ennemies)
                {
                    if (enemy == null) count++;  
                }
                if (count >= ennemies.Count)
                {
                    Cleared = true;
                    OpenDoors();
                }   
            }while ( !Cleared) ;

          
        }

        /// <summary>
        /// Close doors before we spawn enemies.
        /// </summary>
        private void CloseDoors()
        {
            foreach (var door in Doors)
            {
                if (door.GetComponent<WandererDoors>().State == WandererDoors.DoorState.EnemyLocked)
                {
                    door.SetActive(true);
                }
            }
        }

        /// <summary>
        /// Open doors that were closed because of enemies.
        /// </summary>
        private void OpenDoors()
        {
            foreach (var door in Doors)
            {
                if (door.GetComponent<WandererDoors>().State == WandererDoors.DoorState.EnemyLocked)
                {
                    door.SetActive(false);
                }
            }
        }

        /// <summary>
        /// Unlock doors that were locked because there is a shop or reward room on the other end.
        /// </summary>
        private void UnlockDoors()
        {
            if (room.Type == RoomType.Reward || room.Type == RoomType.Shop)
            {
                foreach (var door in Doors)
                {
                    if (door.GetComponent<WandererDoors>().State == WandererDoors.DoorState.Locked)
                    {
                        door.GetComponent<WandererDoors>().State = WandererDoors.DoorState.Unlocked;
                    }
                }
            }
        }

        private static bool IsPointWithinCollider(Collider2D collider, Vector2 point)
        {
            return collider.OverlapPoint(point);
        }

        private static Vector3 RandomPointInBounds(Bounds bounds, float margin = 0)
        {
            var random = WandererGameManager.Instance.Random;

            return new Vector3(
               UnityEngine.Random.Range(bounds.min.x + margin, bounds.max.x - margin),
                UnityEngine.Random.Range(bounds.min.y + margin, bounds.max.y - margin),
                UnityEngine.Random.Range(bounds.min.z + margin, bounds.max.z - margin)
            );
        }

        /// <summary>
        /// Check if we should spawn enemies based on the current state of the room and the type of the room.
        /// </summary>
        /// <returns></returns>
        private bool ShouldSpawnEnemies()
        {
            return Cleared == false && EnemiesSpawned == false && (room.Type == RoomType.Normal || room.Type == RoomType.Hub || room.Type == RoomType.Boss);
        }
    }
}

