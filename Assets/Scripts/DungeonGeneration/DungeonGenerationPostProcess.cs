using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DungeonGenerationPostProcess : MonoBehaviour
{
    GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void PostProcessPipeline(List<BspTree> activeNodes)
    {
        LookForSpawnRoom(activeNodes);
    }
    private void LookForSpawnRoom(List<BspTree> activeNodes)
    {
        foreach(var node in activeNodes)
        {
            if (node.room.Type == RoomType.Spawn)
            {
                player.transform.position = node.room.roomObject.spawnPoint.position;
                break;
            }                          
        }
    }

}
