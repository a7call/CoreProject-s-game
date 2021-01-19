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
        empty, room, connections
    }

    gridSpace[,] grid;
    void Start()
    {
        SetUp();
        CreateLvl();
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
        } while (iterations < 100);

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
                    print(place);
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
                else
                {

                }
            }                  

        }
        foreach(Vector3 place in TemporaryStorage)
        {
            roomPoses.Insert(0, place);
            grid[Mathf.RoundToInt(place.x), Mathf.RoundToInt(place.y)] = gridSpace.room ;
        }
        TemporaryStorage.Clear();
        return roomPoses;
    }

    void RandomSetRoomPosition()
    {

        Vector2 roomPos = new Vector2(GenerateRandomCoord().x, GenerateRandomCoord().y);

        
        GameObject actualRoom = Instantiate(roomObj, roomPos, Quaternion.identity);
        if (roomPos.x - Mathf.RoundToInt(actualRoom.GetComponentInChildren<Collider2D>().bounds.size.x ) < 0 || roomPos.x + Mathf.RoundToInt(actualRoom.GetComponentInChildren<Collider2D>().bounds.size.x) > gridSizeX || roomPos.y + Mathf.RoundToInt(actualRoom.GetComponentInChildren<Collider2D>().bounds.size.y ) > gridSizeY || roomPos.y - Mathf.RoundToInt(actualRoom.GetComponentInChildren<Collider2D>().bounds.size.y ) < 0)
        {
            grid[Mathf.RoundToInt(roomPos.x), Mathf.RoundToInt(roomPos.y)] = gridSpace.empty;
            Destroy(actualRoom);
            return;
        }

        if ( GetTilePositions(actualRoom.transform.GetComponentInChildren<Tilemap>()) == null)
        {
            Destroy(actualRoom);
            return;
        }
        else
        {
            sucess++;
        }
    }
}

