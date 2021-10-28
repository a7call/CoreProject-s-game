using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class DungeonGenerationPostProcess : MonoBehaviour
{
    GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void PostProcessPipeline(List<BspTree> activeNodes)
    {
        foreach(var node in activeNodes)
        {
            AddRoomManager(node);
            LookForSpawnRoom(node);
            SetUpTilemapComponents(node);           
        }
       
    }
    private void LookForSpawnRoom(BspTree node)
    {
        if (node.room.Type == RoomType.Spawn)
        {
            player.transform.position = node.room.roomObject.spawnPoint.position;
        }
    }

    void AddRoomManager(BspTree node)
    {
        var roomManager = node.room.roomObject.gameObject.AddComponent(typeof(RoomManager)) as RoomManager;
        roomManager.Room = node.room;
        node.room.roomObject.gameObject.AddComponent(typeof(RoomDectectionTriggerHandler));
    }


    void SetUpTilemapComponents(BspTree node)
    {
        var tilemaps = node.room.roomObject.transform.Find("Tilemaps");

        // Ground
        AddCompositeCollider(tilemaps.Find("Ground").gameObject, true);
        SetUpLayer(tilemaps.Find("Ground").gameObject, 14);

        //Walls
        AddCompositeCollider(tilemaps.Find("Walls").gameObject);
        SetUpLayer(tilemaps.Find("Walls").gameObject, 10);
    }

    void SetUpLayer(GameObject tilemap, int layer)
    {
        tilemap.layer = layer;
    }

    void AddCompositeCollider(GameObject tilemap, bool isTrigger = false)
    {
        var tilemapCollider = tilemap.AddComponent(typeof(TilemapCollider2D)) as TilemapCollider2D;
        tilemapCollider.usedByComposite = true;
        var compositeCollider2d = tilemap.AddComponent(typeof(CompositeCollider2D)) as CompositeCollider2D;
        compositeCollider2d.geometryType = CompositeCollider2D.GeometryType.Polygons;
        compositeCollider2d.isTrigger = isTrigger;
        tilemap.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
       
    }

}
