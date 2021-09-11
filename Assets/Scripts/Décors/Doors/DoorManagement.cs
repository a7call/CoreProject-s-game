using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DoorManagement : MonoBehaviour
{
    private List<DoorObj> doors = new List<DoorObj>();
    void Start()
    {
        GetActualDoors();
    }

    void GetActualDoors()
    {
        foreach (Transform trans in gameObject.transform)
        {
            doors.Add(trans.GetComponent<DoorObj>());  
        }
    }

    public void CloseDoors()
    {
        foreach (var door in doors)
        {
            ManageLayers(door, true);
            door.AnimateDoors("isClosing");
            door.ManageCollider(true);
        }
    }

    public void OpenDoors()
    {
        foreach (var door in doors)
        {
            ManageLayers(door, false);
            door.AnimateDoors("isOpening");
            door.ManageCollider(false);
        }
    }

    void ManageLayers(DoorObj doorsScript, bool isClosing)
    {
        if (doorsScript.isForeGroundDoor && isClosing)
        {
            doorsScript.sr.sortingLayerName = "ForeGround";
            doorsScript.sr.sortingOrder = 0;
        }
        else
        {
            doorsScript.sr.sortingLayerName = "BackGround";
            doorsScript.sr.sortingOrder = 100;
        }
    }

    
}

