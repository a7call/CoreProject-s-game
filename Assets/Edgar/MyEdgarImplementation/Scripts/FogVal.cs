using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Wanderer.Utils;

public class FogVal : MonoBehaviour
{
    private Tilemap fogOfWarTilemap;
    private List<Vector3> tileWorldLocations = new List<Vector3>();
    private List<Vector3> levelTilePos = new List<Vector3>();
    // Use this for initialization
    void Start()
    {
        fogOfWarTilemap = GetComponent<Tilemap>();
        tileWorldLocations = Utils.GetAllTilesPosition(fogOfWarTilemap);
    }


    public void GetAllLevlesTiles(List<Tilemap> tilemaps)
    {
        foreach(var tilemap in tilemaps)
        {
            levelTilePos.AddRange(Utils.GetAllTilesPosition(tilemap));
        }
        foreach (var pos in levelTilePos)
        {
            if (tileWorldLocations.Contains(pos))
            {
                var posTile = fogOfWarTilemap.WorldToCell(pos);
                fogOfWarTilemap.SetTile(posTile, null);
                tileWorldLocations.Remove(pos);
            }
        }
    }

  
}
