using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public RoomConnectionType roomConnectionType;

    [HideInInspector]
    public Transform pointToSpawn;

    public bool isAssigned { get; set; }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.position = pointToSpawn.position;
        }

    }
}
