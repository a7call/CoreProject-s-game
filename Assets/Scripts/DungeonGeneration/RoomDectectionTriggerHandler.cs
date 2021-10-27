using System.Collections;
using UnityEngine;


public class RoomDectectionTriggerHandler : MonoBehaviour
{

    private RoomManager roomManager;
    void Start()
    {
        roomManager = GetComponentInParent<RoomManager>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            roomManager?.OnRoomEnter(collision.gameObject);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            roomManager?.OnRoomExit(collision.gameObject);
        }
    }
}
