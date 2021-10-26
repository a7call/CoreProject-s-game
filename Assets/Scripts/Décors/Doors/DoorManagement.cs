using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DoorManagement : MonoBehaviour
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
            ManageLayers(door, true);
            door.AnimateDoors("isClosing");
            door.ManageCollider(false);
        }
    }

    public void OpenDoors()
    {
        foreach (var door in doors)
        {
            ManageLayers(door, false);
            door.AnimateDoors("isOpening");
            door.ManageCollider(true);
        }
    }

    void ManageLayers(Door doorsScript, bool isClosing)
    {
        //if (doorsScript.isForeGroundDoor && isClosing)
        //{
        //    doorsScript.sr.sortingLayerName = "ForeGround";
        //    doorsScript.sr.sortingOrder = 0;
        //}
        //else
        //{
        //    doorsScript.sr.sortingLayerName = "BackGround";
        //    doorsScript.sr.sortingOrder = 100;
        //}
    }

    
}

