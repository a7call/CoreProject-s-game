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

    public List<TimeLineManager> ExitTimeLines = new List<TimeLineManager>();

    public RoomConnectionType roomConnectionType;

}
