using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoomConnectionType
{
    EveryConnections,
    Left, 
    Right,
    Front,       
}
public class RoomObject : MonoBehaviour
{
    public  RoomBase Room { get; set; }
   
    public Transform spawnPoint;

    public List<Door> doors = new List<Door>();

    public RoomConnectionType roomConnectionType;

    public List<SpawnPoint> spawnPoints = new List<SpawnPoint>();

    private void Awake()
    {
       // spawnPoint = transform.Find("SpawnPoint");
    }

}
