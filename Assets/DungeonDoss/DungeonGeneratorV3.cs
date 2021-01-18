using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGeneratorV3 : MonoBehaviour
{
    [SerializeField] private int numberOfRooms;
    [SerializeField] Vector2 worldSize = new Vector2(50, 50);
    int gridSizeX, gridSizeY;
   [SerializeField] List<GameObject> roomGOList = new List<GameObject>();
    List<Room> roomsList = new List<Room>();
    public GameObject[] sampleRooms;

    private void SetUP()
    {
        gridSizeX = Mathf.RoundToInt(worldSize.x);
        gridSizeY = Mathf.RoundToInt(worldSize.y);
    }

    private void Start()
    {
        CreateLvl();
    }

    void CreateLvl()
    {
        SetUP();
        int iterations = 0;//loop will not run forever
        do
        {
            SpawnRooms();
           
            iterations++;
            if(roomGOList.Count >= numberOfRooms)
            {
                break;
            }
        } while (iterations < 10000);
    }



    
    void SpawnRooms()   {
        int index = 0;
        Vector2 spawnPos = new Vector2(Mathf.RoundToInt(Random.Range(0, gridSizeX)), Mathf.RoundToInt(Random.Range(0, gridSizeY)));
        GameObject room = Instantiate(sampleRooms[SelectRandomRoom()],spawnPos, Quaternion.identity);
        roomGOList.Insert(0, room);



        Room roomClass = new Room(spawnPos,1);
        
        roomsList.Insert(0, roomClass);
        


        Collider2D roomCollider = room.GetComponentInChildren<Collider2D>();

        if(spawnPos.x- roomCollider.bounds.size.x/2 < 0 || spawnPos.x + roomCollider.bounds.size.x/ 2 > gridSizeX || spawnPos.y - roomCollider.bounds.size.y/2 < 0 || spawnPos.y + roomCollider.bounds.size.y/2 > gridSizeY)
        {
            Destroy(room);
            roomGOList.Remove(room);
            roomsList.Remove(roomClass);
            return;
        }



        foreach (Room roomInList in roomsList)
        {


            if (roomInList == roomClass)
            {
                continue;
            }
           // print("tutu" + roomsList.Count);
            Collider2D roomInListCol = roomGOList[index].GetComponentInChildren<Collider2D>();
           // print( Vector2.Distance(roomClass.gridPos, roomInList.gridPos));
           
            if (Mathf.Abs(roomClass.gridPos.x - roomInList.gridPos.x) < roomCollider.bounds.size.x/2 + roomInListCol.bounds.size.x / 2 &&  Mathf.Abs(roomClass.gridPos.y - roomInList.gridPos.y) < roomCollider.bounds.size.y / 2 + roomInListCol.bounds.size.y / 2)
            {
                Destroy(room);
                roomGOList.Remove(room);
                roomsList.Remove(roomClass);
                return;
            }

            index++;
        }
    }


   int SelectRandomRoom()
    {

         int number = Mathf.RoundToInt(Random.Range(0, sampleRooms.Length ));
         return number;
    }
}
