using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    Vector2 worldSize = new Vector2(30, 30);
    [SerializeField] List<Vector2> takenPositions = new List<Vector2>();
    int gridSizeX, gridSizeY, numberOfRooms = 40;
    [SerializeField]  List<Room> roomsList = new List<Room>();
    [SerializeField]  List<GameObject> SpriteList = new List<GameObject>();
    public Room[,] rooms;
    public GameObject spU, spD, spR, spL,
            spUD, spRL, spUR, spUL, spDR, spDL,
            spULD, spRUL, spDRU, spLDR, spUDRL, BigRoom;
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
            SpriteList.Insert(0,PickSprite(room, drawPos));
            index++;

        }
        BigRooms();
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

    public GameObject sampleRoom;
    GameObject PickSprite(Room room, Vector2 _drawPos)
    { //picks correct sprite based on the four door bools
        _drawPos.x = _drawPos.x * (sampleRoom.GetComponent<Renderer>().bounds.size.x);
        _drawPos.y = _drawPos.y * (sampleRoom.GetComponent<Renderer>().bounds.size.y);
       
        if (room.up)
        {
            if (room.down)
            {
                if (room.right)
                {
                    if (room.left)
                    {
                       
                         GameObject roomObj =  Instantiate(spUDRL, _drawPos, Quaternion.identity);
                        return roomObj;
                       
                    }
                    else
                    {


                        GameObject roomObj = Instantiate(spDRU, _drawPos, Quaternion.identity);
                        return roomObj;

                    }
                }
                else if (room.left)
                {

                    GameObject roomObj =Instantiate(spULD, _drawPos, Quaternion.identity);
                    return roomObj;

                }
                else
                {

                    GameObject roomObj= Instantiate(spUD, _drawPos, Quaternion.identity);
                    return roomObj;

                }
            }
            else
            {
                if (room.right)
                {
                    if (room.left)
                    {

                        GameObject roomObj = Instantiate(spRUL, _drawPos, Quaternion.identity);
                        return roomObj;

                    }
                    else
                    {

                        GameObject roomObj = Instantiate(spUR, _drawPos, Quaternion.identity);
                        return roomObj;

                    }
                }
                else if (room.left)
                {

                    GameObject roomObj = Instantiate(spUL, _drawPos, Quaternion.identity);
                    return roomObj;

                }
                else
                {
                    if (ChanceToSpawnBoss >= Random.value && !BossAlreadySpawned)
                    {
                       
                        BossAlreadySpawned = true;
                        GameObject roomObj = Instantiate(specificRoom, _drawPos, Quaternion.identity);
                        return roomObj;
                    }
                    else
                    {

                        GameObject roomObj = Instantiate(spU, _drawPos, Quaternion.identity);
                        return roomObj;

                    }



                }
            }
        }
        if (room.down)
        {
            if (room.right)
            {
                if (room.left)
                {

                    GameObject roomObj  = Instantiate(spLDR, _drawPos, Quaternion.identity);
                    return roomObj;

                }
                else
                {

                    GameObject roomObj  = Instantiate(spDR, _drawPos, Quaternion.identity);
                    return roomObj;

                }
            }
            else if (room.left)
            {

                GameObject roomObj = Instantiate(spDL, _drawPos, Quaternion.identity);
                return roomObj;

            }
            else
            {
                if (ChanceToSpawnBoss >= Random.value && !BossAlreadySpawned)
                {
                  
                    BossAlreadySpawned = true;
                    GameObject roomObj =  Instantiate(specificRoom, _drawPos, Quaternion.identity);
                    return roomObj;
                }
                else
                {

                    GameObject roomObj =Instantiate(spD, _drawPos, Quaternion.identity);
                    return roomObj;

                }

            }
            
        }
        if (room.right)
        {
            if (room.left)
            {

                GameObject roomObj =  Instantiate(spRL, _drawPos, Quaternion.identity);
                return roomObj;

            }
            else
            {
                if (ChanceToSpawnBoss >= Random.value && !BossAlreadySpawned)
                {
                   
                    BossAlreadySpawned = true;
                    GameObject roomObj =  Instantiate(specificRoom, _drawPos, Quaternion.identity);
                    return roomObj;
                }
                else
                {

                    GameObject roomObj =  Instantiate(spR, _drawPos, Quaternion.identity);
                    return roomObj;

                }
            }
        }
        else
        {
            if (ChanceToSpawnBoss >= Random.value && !BossAlreadySpawned)
            {
              
                BossAlreadySpawned = true;
                GameObject roomObj = Instantiate(specificRoom, _drawPos, Quaternion.identity);
                return roomObj;
            }
            else
            {

                GameObject roomObj = Instantiate(spL, _drawPos, Quaternion.identity);
                return roomObj;

            }


        }
    }

    bool isDone = false;
   void BigRooms()
    {
        for (int x = 0; x < ((gridSizeX)); x++)
        {
            if (isDone) break;
            for (int y = 0; y < ((gridSizeY)); y++)
            {
                if (isDone) break;
                if (rooms[x, y] != null)
                {
                    
                    if ((y - 1 > 0) && (x - 1 > 0))
                    {
                        
                        if (rooms[x, y - 1] != null && rooms[x - 1, y] != null && rooms[x - 1, y - 1] != null)
                        {
                            int index = 0;
                            foreach(Room room in roomsList.ToArray())
                            {
                                
                                if (room.gridPos == rooms[x, y - 1].gridPos || room.gridPos == rooms[x - 1, y].gridPos || room.gridPos == rooms[x - 1, y - 1].gridPos || room.gridPos == rooms[x , y ].gridPos)
                                {
                                    print(room.gridPos);
                                    Destroy(SpriteList[index].gameObject);
                                }
                                index++;

                            }
                            Vector2 pos = new Vector2(rooms[x, y].gridPos.x + rooms[x-1, y].gridPos.x, rooms[x, y].gridPos.y + rooms[x , y-1].gridPos.y);
                            pos.x = pos.x * (sampleRoom.GetComponent<Renderer>().bounds.size.x)/2;
                            pos.y = pos.y * (sampleRoom.GetComponent<Renderer>().bounds.size.y)/2;
                            GameObject roomObj = Instantiate(BigRoom, pos  , Quaternion.identity);
                            SpriteList.Insert(0, roomObj);
                            isDone = true;
                        }
                        
                    }

                }
            }
        }
    }
}
