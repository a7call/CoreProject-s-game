using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomDoorManager : MonoBehaviour
{
    private List<Door> doors = new List<Door>();
    void Awake()
    {
        GetActualDoors();
    }

    void GetActualDoors()
    {
        foreach (Transform trans in gameObject.transform)
        {
            doors.Add(trans.GetComponent<Door>());
        }
    }

    public void CloseDoors()
    {
        foreach (var door in doors)
        {            
            door.AnimateDoors("isClosing");
            door.ManageCollider(false);
            door.ManageLayers(true);
        }
    }

    public void OpenDoors()
    {
        foreach (var door in doors)
        {           
            door.AnimateDoors("isOpening");
            door.ManageCollider(true);
            door.ManageLayers(false);
        }
    }

   

    
}

