using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonGeneratorV5 : MonoBehaviour
{
    [SerializeField] private int numberOfRooms;
    [SerializeField] Vector2 worldSize = new Vector2(50, 50);
    int gridSizeX, gridSizeY;
    int sucess = 0;
    public List<Vector3> roomPoses = new List<Vector3>();

    [SerializeField] private GameObject roomObj;
    enum gridSpace
    {
        empty, room, connections, corridor
    }

    gridSpace[,] grid;
    void Start()
    {
        SetUp();
        CreateLvl();
        Invoke("GeneratePathWay", 5f);
    }

    void CreateLvl()
    {
        int iterations = 0;//loop will not run forever
        do
        {

            RandomSetRoomPosition();
            iterations++;
        if (sucess >= numberOfRooms)
            {
                break;
            }
        } while (iterations < 1000);

    }

    private void SetUp()
    {
        gridSizeX = Mathf.RoundToInt(worldSize.x);
        gridSizeY = Mathf.RoundToInt(worldSize.y);
        grid = new gridSpace[gridSizeX, gridSizeY];

        for (int x = 0; x < gridSizeX - 1; x++)
        {
            for (int y = 0; y < gridSizeY - 1; y++)
            {
                //make every cell "empty"
                grid[x, y] = gridSpace.empty;
            }
        }
    }


    Vector2 GenerateRandomCoord()
    {
        int posX = Mathf.RoundToInt(Random.Range(0, gridSizeX));
        int posY = Mathf.RoundToInt(Random.Range(0, gridSizeY));
        Vector2 gridPos = new Vector2(posX, posY);
        return gridPos;
    }


    List<Vector3> GetTilePositions(Tilemap _tileMap)
    {

        List<Vector3> TemporaryStorage = new List<Vector3>();
      
        for (int n = _tileMap.cellBounds.xMin; n < _tileMap.cellBounds.xMax; n++)
        {
            for (int p = _tileMap.cellBounds.yMin; p < _tileMap.cellBounds.yMax; p++)
            {
                Vector2Int localPlace = (new Vector2Int(n, p));
                Vector3 place = _tileMap.CellToWorld((Vector3Int)localPlace);


                if (_tileMap.HasTile((Vector3Int)localPlace))
                {
                    if(place.x <= 1 || place.x >= gridSizeX-1 || place.y <= 1 || place.y >= gridSizeY-1)
                    {
                        TemporaryStorage.Clear();
                        return null;
                    }
                    else
                    {
                        if (grid[Mathf.RoundToInt(place.x), Mathf.RoundToInt(place.y)] == gridSpace.empty)
                        {  //Tile at "place"

                            TemporaryStorage.Add(place);
                        }
                        else
                        {
                            TemporaryStorage.Clear();
                            return null;
                        }
                    }
                    
                }
                else
                {

                }
            }                  

        }
        foreach(Vector3 place in TemporaryStorage)
        {
            roomPoses.Insert(0, place);
            grid[Mathf.RoundToInt(place.x), Mathf.RoundToInt(place.y)] = gridSpace.room;
            if (grid[Mathf.RoundToInt(place.x+1), Mathf.RoundToInt(place.y)] == gridSpace.empty) grid[Mathf.RoundToInt(place.x+1), Mathf.RoundToInt(place.y)] = gridSpace.room;
            if (grid[Mathf.RoundToInt(place.x-1), Mathf.RoundToInt(place.y)] == gridSpace.empty) grid[Mathf.RoundToInt(place.x-1), Mathf.RoundToInt(place.y)] = gridSpace.room;
            if (grid[Mathf.RoundToInt(place.x), Mathf.RoundToInt(place.y+1)] == gridSpace.empty) grid[Mathf.RoundToInt(place.x), Mathf.RoundToInt(place.y+1)] = gridSpace.room;
            if (grid[Mathf.RoundToInt(place.x), Mathf.RoundToInt(place.y-1)] == gridSpace.empty) grid[Mathf.RoundToInt(place.x), Mathf.RoundToInt(place.y-1)] = gridSpace.room;
            if (grid[Mathf.RoundToInt(place.x-1), Mathf.RoundToInt(place.y-1)] == gridSpace.empty) grid[Mathf.RoundToInt(place.x-1), Mathf.RoundToInt(place.y-1)] = gridSpace.room;
            if (grid[Mathf.RoundToInt(place.x+1), Mathf.RoundToInt(place.y+1)] == gridSpace.empty) grid[Mathf.RoundToInt(place.x+1), Mathf.RoundToInt(place.y+1)] = gridSpace.room;
        }
        TemporaryStorage.Clear();
        return roomPoses;
    }

    void RandomSetRoomPosition()
    {

        Vector2 roomPos = new Vector2(GenerateRandomCoord().x, GenerateRandomCoord().y);
        GameObject actualRoom = Instantiate(roomObj, roomPos, Quaternion.identity);
        if ( GetTilePositions(actualRoom.transform.GetComponentInChildren<Tilemap>()) == null)
        {
            grid[Mathf.RoundToInt(roomPos.x), Mathf.RoundToInt(roomPos.y)] = gridSpace.empty;
            Destroy(actualRoom);
            return;
        }
        else
        {
            sucess++;
        }
    }
    List<Vector2> startingPoints = new List<Vector2>();
    List<Vector2> LookForStartingPoints()
    {

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                if(grid[x,y] == gridSpace.empty)
                {
                    Vector2 newStartPoint = new Vector2(x, y);
                    startingPoints.Insert(0, newStartPoint);
                }
                
            }
        }
        return startingPoints;
    }

    List<Vector2> TracePos;
   public Tile tile;
    void GeneratePathWay()
    {
        
        TracePos = new List<Vector2>();
        int index = Random.Range(0, LookForStartingPoints().Count);
        Vector2 StartPathPos = startingPoints[index];
        Vector2 dir = RandomPathDirection();
        int iterations = 0;
        int i = 0;
        do
        {
            
            Vector2 pos = StartPathPos;
            
            StartPathPos = pos + dir;
            print(StartPathPos);
            if (StartPathPos.x <= 1 || StartPathPos.x >= gridSizeX - 1 || StartPathPos.y <= 1 || StartPathPos.y >= gridSizeY - 1)
            {
                if (i - 1 > 0)
                {
                    StartPathPos = TracePos[i - 1];
                    RandomPathDirection();
                }
                else
                {

                    print("totot");
                    break;

                }
            }
            else
            {
                print(StartPathPos);
                if (grid[Mathf.RoundToInt(StartPathPos.x), Mathf.RoundToInt(StartPathPos.y)] == gridSpace.empty)
                {
                    grid[Mathf.RoundToInt(StartPathPos.x), Mathf.RoundToInt(StartPathPos.y)] = gridSpace.corridor;
                    Vector3Int fro = GetComponentInChildren<Tilemap>().WorldToCell(StartPathPos);
                    GetComponentInChildren<Tilemap>().SetTile(fro, tile);
                    TracePos.Insert(0, StartPathPos);
                    i++;
                }
                else
                {
                    if (i - 1 > 0)
                    {
                        StartPathPos = TracePos[i - 1];
                        dir = RandomPathDirection();
                    }
                    else
                    {
                        break;
                    }

                }
            }

               
      


            iterations++;
        }while(iterations < 1000);
    }

    private Vector2 RandomPathDirection()
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
}

