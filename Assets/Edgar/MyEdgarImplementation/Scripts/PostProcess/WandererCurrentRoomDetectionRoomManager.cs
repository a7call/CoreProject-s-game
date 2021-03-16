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
        public enemyStruct[] Enemies;

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
        public static RoomInstance previousRoom;
        public static RoomInstance previousCorridor;
        /// <summary>
        /// Gets called when a player enters the room.
        /// </summary>
        /// <param name="player"></param>
        /// 

        //EnemySpawn Variable

        bool shouldSpawnEnemy = false;
        bool alReadyChecked = false;
        bool EnemyBaseSpawn;


        public void OnRoomEnter(GameObject player)
        {
            Debug.Log($"Room enter. Room name: {RoomInstance.Room.GetDisplayName()}, Room template: {RoomInstance.RoomTemplatePrefab.name}");
            WandererGameManager.Instance.OnRoomEnter(RoomInstance);
            lightGestion();
            SetEnemyPoints();
            // Calcule probability of 2nd Spawn
            if (!alReadyChecked)
            {
                alReadyChecked = true;
                shouldSpawnEnemy = ShouldSpawnEnemies();

                if (shouldSpawnEnemy)
                {
                    float chanceToEnemyBase = UnityEngine.Random.Range(0.0f,1.0f);

                    EnemyBaseSpawn = chanceToEnemyBase >= 0.4f ? EnemyBaseSpawn = true : EnemyBaseSpawn = false;
                }
              
            }


            // Time Base Spawn
            if (shouldSpawnEnemy && !EnemyBaseSpawn)
            {
                StartCoroutine(TimeBaseSpawn());
            }


           
            if (!Visited && roomInstance != null)
            {
                Visited = true;
                UnlockDoors();
            }
            



            else if (room.Type != RoomType.Spawn)
            {
                CloseDoors();
               // OpeningDoors(enemies);
            }
          //  StartCoroutine(ExploreRoom());
        }
        /// <summary>
        /// Gets called when a player leaves the room.
        /// </summary>
        /// <param name="player"></param>
        public void OnRoomLeave(GameObject player)
        {
            //Debug.Log($"Room leave {RoomInstance.Room.GetDisplayName()}");
            WandererGameManager.Instance.OnRoomLeave(RoomInstance);
            if(!roomInstance.IsCorridor) previousRoom = roomInstance;
            if(roomInstance.IsCorridor) previousCorridor = roomInstance;

        }

      

        void lightGestion()
        {
            if (!roomInstance.IsCorridor && previousRoom != null)
            {
                if (previousRoom == roomInstance)
                {
                    foreach (RoomInstance room in roomInstance.Doors.Select(x => x.ConnectedRoomInstance))
                    {
                        foreach (RoomInstance roomInstance in room.Doors.Select(x => x.ConnectedRoomInstance))
                        {
                            if (roomInstance != previousRoom)
                            {
                                GetLigthSwitchOFF(roomInstance);
                            }

                        }
                    }

                }
                else
                {
                    GetLigthSwitchOFF(previousRoom);
                }

            }
            if (roomInstance.IsCorridor && previousCorridor != null && previousCorridor != roomInstance)
            {
                GetLigthSwitchOFF(previousCorridor);
            }
        }
        
        private void GetLigthSwitchOFF(RoomInstance roomInstance)
        {
           
            if(roomInstance.RoomTemplateInstance.transform.Find("LightContainer") != null)
            {
                Transform LigthContainerObject = roomInstance.RoomTemplateInstance.transform.Find("LightContainer");

                foreach (Transform child in LigthContainerObject.transform)
                {
                    if (child.gameObject.activeSelf)
                    {
                        child.gameObject.SetActive(false);
                    }
                }
            } 
           
        }
       
        public void Start()
        {
            roomInstance = GetComponent<RoomInfo>()?.RoomInstance;
            room = roomInstance?.Room as WandererRoom; 
        }

        float numberOfEnemyLeftInRoom = 2f;
        public void Update()
        {
            // clean Enemy array when enely is killed;
            CleanEnemyArray();
            // If proba == EnemyBase Spawn
            EnemyBasedSpawn();
            // Check if all enemy are killed if true clear = true; (room is safe)
            CheckIsRoomSafe();


        }

       private float timeBeforeBackUps =5f;
        void CheckIsRoomSafe()
        {
            if ((!shouldSpawnEnemy && roomInstance.Enemies.Count <= 0 && !Cleared && roomInstance.IsEnemyAlreadySpawned) || (shouldSpawnEnemy && EnemiesSpawned && roomInstance.Enemies.Count <= 0 && !Cleared && roomInstance.IsEnemyAlreadySpawned))
            {
                Cleared = true;
            }
        }
       // TimeBase Spawn
       void CleanEnemyArray()
        {

            if (!Cleared)
            {
                foreach (GameObject enemy in roomInstance.Enemies.ToArray())
                {

                    if (enemy == null)
                    {
                        roomInstance.Enemies.Remove(enemy);
                    }
                }
            }
        }
       private IEnumerator TimeBaseSpawn()
        {
            yield return new WaitForSeconds(timeBeforeBackUps);
            StartCoroutine(SpawnEnemies());
        }

        //EnemyBase Spawn
        void EnemyBasedSpawn()
        {
            if (shouldSpawnEnemy && !Cleared && !EnemiesSpawned && EnemyBaseSpawn)
            {
                if (roomInstance.Enemies.Count <= numberOfEnemyLeftInRoom)
                {
                    StartCoroutine(SpawnEnemies());
                }
            }
        }

        private void SetEnemyPoints()
        {
           if(room.Type == RoomType.Large)
            {
                roomInstance.enemyPointsAvailable = 15;
            }
                else if (room.Type == RoomType.Medium)
            {
                roomInstance.enemyPointsAvailable = 10;
            }
            else if (room.Type == RoomType.Small)
            {
                roomInstance.enemyPointsAvailable = 5;
            }
        }

        // Spawn Enemy function voir DetectionPostProcess pour comment 
        private IEnumerator SpawnEnemies()
        {
            EnemiesSpawned = true;
            // PointsInTheCurrentRoom
            int currentRoomPoint = 0;

            var totalEnemiesCount = WandererGameManager.Instance.Random.Next(4, 8);
            yield return new WaitForSeconds(1f);

            do
            {
                yield return new WaitForSeconds (0.001f);
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
                if (currentRoomPoint + enemyPrefab.EnemyPoint <= roomInstance.EnemyPointsAvailable)
                {
                    var enemyGO = Instantiate(enemyPrefab.enemy);
                    enemyGO.transform.position = position;
                    enemyGO.transform.parent = roomInstance.RoomTemplateInstance.transform;
                    roomInstance.Enemies.Add(enemyGO);
                    currentRoomPoint += enemyPrefab.EnemyPoint;
                }
            } while (currentRoomPoint < roomInstance.EnemyPointsAvailable);
            // StartCoroutine(OpeningDoors(roomInstance.Enemies));
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

        public static bool IsPointWithinCollider(Collider2D collider, Vector2 point)
        {
            return collider.OverlapPoint(point);
        }

        public static Vector3 RandomPointInBounds(Bounds bounds, float margin = 0)
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
            return Cleared == false && EnemiesSpawned == false &&( room.Type == RoomType.Large ||room.Type == RoomType.Medium )&& UnityEngine.Random.Range(0.0f,1.0f) >= 0.5f;
        }
    }
}

