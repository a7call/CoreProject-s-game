using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGeneratorV4 : MonoBehaviour
{
    [SerializeField] private int numberOfRooms;
    [SerializeField] Vector2 worldSize = new Vector2(50, 50);
    float worldUnitsInOneGridCell = 1;
    int gridSizeX, gridSizeY;
    [SerializeField] private GameObject roomObj;
    enum gridSpace
    {
        empty, room, connections
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

            RandomSetRoomPosition();
            iterations++;

            if (NumberOfRoom() >= numberOfRooms)
            {
                break;
            }
            } while (iterations < 10000) ;
        
    }
    void  RandomSetRoomPosition()
    {
        Vector2 roomPos = new Vector2(GenerateRandomCoord().x, GenerateRandomCoord().y);

        if (grid[Mathf.RoundToInt(roomPos.x), Mathf.RoundToInt(roomPos.y)] == gridSpace.empty)  {
           
                grid[Mathf.RoundToInt(roomPos.x), Mathf.RoundToInt(roomPos.y)] = gridSpace.room;
            Instantiate(roomObj, roomPos, Quaternion.identity);
            }
        
        
    }

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


}
