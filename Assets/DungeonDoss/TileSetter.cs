using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileSetter : MonoBehaviour
{
    private Tilemap tilemap;
    public Tile Room;
    public GameObject test;
    void Start()
    {
      tilemap = GetComponentInChildren<Tilemap>();
        Room.gameObject = test;
        
      tilemap.SetTile(Vector3Int.zero, Room);
    }

}
