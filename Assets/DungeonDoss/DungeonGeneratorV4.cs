using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGeneratorV4 : MonoBehaviour
{
    [SerializeField] private int numberOfRooms;
    [SerializeField] Vector2 worldSize = new Vector2(50, 50);
    float worldUnitsInOneGridCell = 1f;
    int gridSizeX, gridSizeY;
   
    [SerializeField] private GameObject roomObj;
    enum gridSpace
    {
        empty, room, connections, belongToRoom
    }

    gridSpace [,] grid;

    private void Start()
    {
        SetUp();
        CreateLvl();
    }
    private void SetUp()
    {
        gridSizeX = Mathf.RoundToInt(worldSize.x / worldUnitsInOneGridCell);
        gridSizeY = Mathf.RoundToInt(worldSize.y / worldUnitsInOneGridCell);
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


    void CreateLvl()
    {
        int iterations = 0;//loop will not run forever
        do
        {

            //RandomSetRoomPosition();
            iterations++;
            if (NumberOfRoom() >= numberOfRooms)
            {
                break;
            }
            } while (iterations < 10000) ;
        
    }

    [SerializeField] GameObject testObj;
   
              
          
    
        
        
    

    Vector2 GenerateRandomCoord()
    {
        int posX  = Mathf.RoundToInt(Random.Range(0, gridSizeX));
        int posY = Mathf.RoundToInt(Random.Range(0, gridSizeY));
        Vector2 gridPos = new Vector2(posX, posY);
        return gridPos;
    }

    int NumberOfRoom()
    {
        int count = 0;
        foreach (gridSpace space in grid)
        {
            if (space == gridSpace.room)
            {
                count++;
            }
        }
        return count;
    }






   /* void RandomSetRoomPosition()
    {
        List<Vector2> roomEspace = new List<Vector2>();
        Vector2 roomPos = new Vector2(GenerateRandomCoord().x, GenerateRandomCoord().y);
        if (grid[Mathf.RoundToInt(roomPos.x), Mathf.RoundToInt(roomPos.y)] == gridSpace.empty)
        {

            grid[Mathf.RoundToInt(roomPos.x), Mathf.RoundToInt(roomPos.y)] = gridSpace.room;
        }
        GameObject actualRoom = Instantiate(roomObj, roomPos, Quaternion.identity);
        if (roomPos.x - Mathf.RoundToInt(actualRoom.GetComponentInChildren<Collider2D>().bounds.size.x / 2) < 0 || roomPos.x + Mathf.RoundToInt(actualRoom.GetComponentInChildren<Collider2D>().bounds.size.x / 2) > gridSizeX || roomPos.y + Mathf.RoundToInt(actualRoom.GetComponentInChildren<Collider2D>().bounds.size.y / 2) > gridSizeY || roomPos.y - Mathf.RoundToInt(actualRoom.GetComponentInChildren<Collider2D>().bounds.size.y / 2) < 0)
        {
            grid[Mathf.RoundToInt(roomPos.x), Mathf.RoundToInt(roomPos.y)] = gridSpace.empty;
            Destroy(actualRoom);
            return;
        }

        for (int x = (int)roomPos.x - Mathf.RoundToInt(actualRoom.GetComponentInChildren<Collider2D>().bounds.size.x / 2); x < (int)roomPos.x + Mathf.RoundToInt(actualRoom.GetComponentInChildren<Collider2D>().bounds.size.x / 2); x++)
        {
            for (int y = (int)roomPos.y - Mathf.RoundToInt(actualRoom.GetComponentInChildren<Collider2D>().bounds.size.y / 2); y < (int)roomPos.y + Mathf.RoundToInt(actualRoom.GetComponentInChildren<Collider2D>().bounds.size.y / 2); y++)
            {
                if (grid[x, y] != gridSpace.empty && x != roomPos.x && y != roomPos.y)
                {

                    grid[(int)roomPos.x, (int)roomPos.y] = gridSpace.empty;
                    foreach (Vector2 espace in roomEspace)
                    {
                        grid[(int)espace.x, (int)espace.y] = gridSpace.empty;
                    }
                    Destroy(actualRoom);
                    break;

                }
                else if (x != roomPos.x && y != roomPos.y)
                {
                    Vector2 goodCoord = new Vector2(x, y);
                    roomEspace.Insert(0, goodCoord);
                    grid[x, y] = gridSpace.belongToRoom;

                }


            }


        }
    }
   */
}
