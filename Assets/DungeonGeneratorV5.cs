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
        empty, room, connections, corridor, wall,
    }

    gridSpace[,] grid;
    void Start()
    {
        SetUp();
        CreateLvl();
        StartCoroutine(createMaze());



    }


   IEnumerator createMaze()
    {  
         for (int x = 1; x<gridSizeX; x += 15)
        {
            for (int y = 1; y<gridSizeY; y += 15)
            {
                
                var pos = new Vector2(x, y);

                if (grid[Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y)] != gridSpace.empty) continue;

                List<Vector2> cells = new List<Vector2>();
                Vector2 lastDir = Vector2.zero;
                AddTile(pos);
                cells.Add(pos);


                while (cells.Count > 0)
                {
                    yield return new WaitForSeconds(0.01f);
                    Vector2 cell = cells[cells.Count - 1];
                    List<Vector2> Direction = new List<Vector2>();
                    Direction.Add(Vector2.up);
                    Direction.Add(Vector2.down);
                    Direction.Add(Vector2.left);
                    Direction.Add(Vector2.right);
                    List<Vector2> unMadeCells = new List<Vector2>();

                    foreach (Vector2 dir in Direction.ToArray())
                    {
                        if (_canAddTile(cell, dir))
                        {
                            unMadeCells.Add(dir);
                        }
                    }
                

                    if (unMadeCells.Count > 0)
                    {
                        Vector2 dir;

                        if (unMadeCells.Contains(lastDir) && Random.Range(0, 100) > 45)
                        {
                            dir = lastDir;
                        }
                        else
                        {
                            dir = unMadeCells[Random.Range(0, unMadeCells.Count)];
                        }

                        AddTile(cell + dir);
                        AddTile(cell + 2 * dir);
                        AddTile(cell + 3 * dir);
                        AddTile(cell + 4 * dir);
                        AddTile(cell + 5 * dir);
                        AddTile(cell + 6 * dir);
                        AddTile(cell + 7 * dir);
                        AddTile(cell + 8 * dir);
                        AddTile(cell + 9 * dir);
                        AddTile(cell + 10 * dir);
                        AddTile(cell + 11* dir);
                        AddTile(cell + 12 * dir);
                        AddTile(cell + 13* dir);
                        AddTile(cell + 14 * dir);
                        AddTile(cell + 15* dir);

                     
                        cells.Add(cell + 15 * dir);
                        lastDir = dir;

                    }
                    else
                    {
                        cells.RemoveAt(cells.Count - 1);
                        lastDir = Vector2.zero;

                    }

                }
                yield return true;



            }
        }
    }

     bool _canAddTile(Vector2 pos, Vector2 dir)
    {
        if (pos.x + dir.x * 15 >= gridSizeX || pos.x + dir.x * 15 <= 0 || pos.y + dir.y * 15 >= gridSizeY || pos.y + dir.y * 15 <= 0) return false;
        
        return grid[Mathf.RoundToInt(pos.x + dir.x * 15), Mathf.RoundToInt(pos.y + dir.y * 15)] == gridSpace.empty;
    }

    void AddTile(Vector2 pos)
    {
        if (grid[Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y)] == gridSpace.empty)
        {
            Vector3Int tilePos = GetComponentInChildren<Tilemap>().WorldToCell(pos);
            Vector3Int tilePos2 = GetComponentInChildren<Tilemap>().WorldToCell(pos+ Vector2.up);
            Vector3Int tilePos3= GetComponentInChildren<Tilemap>().WorldToCell(pos+ Vector2.down);
            Vector3Int tilePos4 = GetComponentInChildren<Tilemap>().WorldToCell(pos+ Vector2.left);
            Vector3Int tilePos5 = GetComponentInChildren<Tilemap>().WorldToCell(pos +Vector2.right + Vector2.up);
            Vector3Int tilePos6 = GetComponentInChildren<Tilemap>().WorldToCell(pos +Vector2.right + Vector2.down);
            Vector3Int tilePos7 = GetComponentInChildren<Tilemap>().WorldToCell(pos +Vector2.left + Vector2.up);
            Vector3Int tilePos8 = GetComponentInChildren<Tilemap>().WorldToCell(pos +Vector2.left +  Vector2.down);

            grid[Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y)] = gridSpace.connections;
           GetComponentInChildren<Tilemap>().SetTile(tilePos, tile);
            /* GetComponentInChildren<Tilemap>().SetTile(tilePos2, tile);
            GetComponentInChildren<Tilemap>().SetTile(tilePos3, tile);
            GetComponentInChildren<Tilemap>().SetTile(tilePos4, tile);
            GetComponentInChildren<Tilemap>().SetTile(tilePos5, tile);
            GetComponentInChildren<Tilemap>().SetTile(tilePos6, tile);
            GetComponentInChildren<Tilemap>().SetTile(tilePos7, tile);
            GetComponentInChildren<Tilemap>().SetTile(tilePos8, tile);
          */

        }

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
        } while (iterations < 10000);

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
  Vector2 dir;
   
   
  
   
    public List<Vector2> previousDirections = new List<Vector2>();
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















































    IEnumerator GeneratePathWay()
    {

        TracePos = new List<Vector2>();
        int index = Random.Range(0, LookForStartingPoints().Count);
        Vector2 StartPathPos = startingPoints[index];
        int dirIndex = 0;
        int iterations = 0;
        int i = 0;
        int ip = 1;
        dir = RandomPathDirection();
        do
        {
            // print(iterations);


            Vector2 pos = StartPathPos;

            Vector2 TempPos = pos + dir;

            if (TempPos.x <= 2 || TempPos.x >= gridSizeX - 2 || TempPos.y <= 2 || TempPos.y >= gridSizeY - 2)
            {
                if (i - ip > 0)
                {

                    StartPathPos = TracePos[i - ip];
                    dir = RandomPathDirection();
                    ip++;
                }
                else
                {
                    break;
                }
            }
            else
            {
                StartPathPos = TempPos;
                if (i - ip >= TracePos.Count - 1 && grid[Mathf.RoundToInt(StartPathPos.x), Mathf.RoundToInt(StartPathPos.y)] == gridSpace.corridor) break;
                if (grid[Mathf.RoundToInt(StartPathPos.x), Mathf.RoundToInt(StartPathPos.y)] == gridSpace.empty && grid[Mathf.RoundToInt(StartPathPos.x + 2 * dir.x), Mathf.RoundToInt(StartPathPos.y + 2 * dir.y)] == gridSpace.empty)
                {

                    yield return new WaitForSeconds(0.01f);
                    grid[Mathf.RoundToInt(StartPathPos.x), Mathf.RoundToInt(StartPathPos.y)] = gridSpace.corridor;

                    Vector3Int fro = GetComponentInChildren<Tilemap>().WorldToCell(StartPathPos);

                    GetComponentInChildren<Tilemap>().SetTile(fro, tile);
                    print(i);



                    TracePos.Add(StartPathPos);
                    previousDirections.Add(dir);
                    dirIndex++;
                    i++;
                    ip = 1;
                }
                else
                {

                    StartPathPos = TracePos[i - ip];
                    dir = RandomPathDirection();
                    if (i - ip > 0) ip++;

                }

            }

            iterations++;
        } while (iterations < 10000);
    }
}

