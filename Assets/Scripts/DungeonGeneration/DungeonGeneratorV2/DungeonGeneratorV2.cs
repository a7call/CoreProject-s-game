using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGeneratorV2 : MonoBehaviour
{
    Vector2 worldSize = new Vector2(30, 30);
    [SerializeField] List<Vector2> takenPositions = new List<Vector2>();
    int gridSizeX, gridSizeY, numberOfRooms = 40;
    [SerializeField] List<Room> roomsList = new List<Room>();
    [SerializeField] List<GameObject> SpriteList = new List<GameObject>();
    public Room[,] rooms;
    public GameObject specificRoom;
    public struct walker
    {
        public Vector2 pos;
        public Vector2 dir;
    }
    public List<walker> walkers = new List<walker>();
    float chanceWalkerChangeDir = 0.5f, chanceToSpawnWalker = 0f, chanceWalkersDestroy = 0.05f;
    int maxWalkers = 10;
    public float ChanceToSpawnBoss = 0.3f;
    private bool BossAlreadySpawned;


    private void Start()
    {
        Setup();

        CreateRooms();
        SetRoomDoors();
        SpawnLevel();
    }

    void Setup()
    {

        if (numberOfRooms >= (worldSize.x) * (worldSize.y))
        {
            numberOfRooms = Mathf.RoundToInt((worldSize.x * 2) * (worldSize.y * 2));
        }


        gridSizeX = Mathf.RoundToInt(worldSize.x);
        gridSizeY = Mathf.RoundToInt(worldSize.y);
        walker newWalker = new walker();
        newWalker.dir = RandomDirection();
        //find center of grid

        Vector2 spawnPos = new Vector2(Mathf.RoundToInt(gridSizeX / 2.0f), Mathf.RoundToInt(gridSizeY / 2.0f));

        newWalker.pos = spawnPos;

        //add walker to list
        walkers.Add(newWalker);
    }


    void CreateRooms()
    {
        rooms = new Room[gridSizeX + 1, gridSizeY + 1];

        int iterations = 0;//loop will not run forever
        do
        {
            //create floor at position of every walker
            int index = 0;
            foreach (walker myWalker in walkers)
            {

                if (takenPositions.Contains(myWalker.pos))
                {
                    continue;
                }
                else if (myWalker.pos.x > gridSizeX || myWalker.pos.x < 0 || myWalker.pos.y > gridSizeY || myWalker.pos.y < 0)
                {
                    walkers.RemoveAt(index);
                    // revenir en arriere
                    break;
                }
                else
                {

                    Vector2 newPos = new Vector2((int)myWalker.pos.x, (int)myWalker.pos.y);

                    takenPositions.Insert(0, newPos);
                    rooms[(int)myWalker.pos.x, (int)myWalker.pos.y] = new Room(newPos, 1);

                }
                index++;

            }

            int numberChecks = walkers.Count; //might modify count while in this loop
            if (numberChecks < 1)
            {
                walker newWalker = new walker();
                newWalker.dir = RandomDirection();
                //find center of grid

                Vector2 spawnPos = new Vector2(Mathf.RoundToInt(gridSizeX / 2.0f), Mathf.RoundToInt(gridSizeY / 2.0f));

                newWalker.pos = spawnPos;

                //add walker to list
                walkers.Add(newWalker);
            }

            for (int i = 0; i < walkers.Count; i++)
            {
                if (Random.value < chanceWalkerChangeDir)
                {
                    walker thisWalker = walkers[i];
                    thisWalker.dir = RandomDirection();
                    walkers[i] = thisWalker;
                }
            }

            numberChecks = walkers.Count; //might modify count while in this loop
            for (int i = 0; i < numberChecks; i++)
            {
                //only if # of walkers < max, and at a low chance
                if (Random.value < chanceToSpawnWalker && walkers.Count < maxWalkers)
                {
                    //create a walker 
                    walker newWalker = new walker();
                    newWalker.dir = RandomDirection();
                    newWalker.pos = walkers[i].pos;
                    walkers.Add(newWalker);
                }
            }

            numberChecks = walkers.Count; //might modify count while in this loop
            for (int i = 0; i < numberChecks; i++)
            {
                //only if its not the only one, and at a low chance
                if (Random.value < chanceWalkersDestroy && walkers.Count > 1)
                {
                    walkers.RemoveAt(i);
                    break; //only destroy one per iteration
                }
            }


            for (int i = 0; i < walkers.Count; i++)
            {
                Debug.LogWarning("test" + i);
                walker thisWalker = walkers[i];
                thisWalker.pos += thisWalker.dir;
                walkers[i] = thisWalker;

            }
            if (takenPositions.Count > numberOfRooms - 1)
            {
                break;
            }

            iterations++;

        } while (iterations < 100000000);
    }


    [SerializeField] GameObject startRoom;
    void SpawnLevel()
    {
        int index = 0;
        foreach (Room room in rooms)
        {
            if (room == null)
            {
                continue; //skip where there is no room
            }


            Vector2 drawPos = room.gridPos;
            roomsList.Insert(0, room);
            
            ChanceToSpawnBoss += 0.1f;
            index++;

        }
        GetBool();
    }

    private Vector2 RandomDirection()
    {
        int choice = Mathf.FloorToInt(Random.value * 3.99f);
        //use that int to chose a direction
        switch (choice)
        {
            case 0:
                return Vector2.down;
            case 1:
                return Vector2.left;
            case 2:
                return Vector2.up;
            default:
                return Vector2.right;
        }
    }

    public GameObject sampleRoom;
    void GetBool()
    {
        int i = 0;
        foreach (Room room in roomsList)
        {
            GameObject test = Instantiate(sampleRoom, room.gridPos, Quaternion.identity);
            test.GetComponent<testdung>().left = room.left;
            test.GetComponent<testdung>().right = room.right;
            test.GetComponent<testdung>().up = room.up;
            test.GetComponent<testdung>().down = room.down;
        }
    }

    void SetRoomDoors()
    {
        for (int x = 0; x < ((gridSizeX)); x++)
        {
            for (int y = 0; y < ((gridSizeY)); y++)
            {
                if (rooms[x, y] == null)
                {
                    continue;
                }
                Vector2 gridPosition = new Vector2(x, y);
                if (y - 1 < 0)
                { //check above
                    rooms[x, y].down = false;
                }
                else
                {
                    rooms[x, y].down = (rooms[x, y - 1] != null);
                }
                if (y + 1 >= gridSizeY * 2)
                { //check bellow
                    rooms[x, y].up = false;
                }
                else
                {
                    rooms[x, y].up = (rooms[x, y + 1] != null);
                }
                if (x - 1 < 0)
                { //check left
                    rooms[x, y].left = false;
                }
                else
                {
                    rooms[x, y].left = (rooms[x - 1, y] != null);
                }
                if (x + 1 >= gridSizeX * 2)
                { //check right
                    rooms[x, y].right = false;
                }
                else
                {
                    rooms[x, y].right = (rooms[x + 1, y] != null);
                }
            }
        }
    }

   

   
}
