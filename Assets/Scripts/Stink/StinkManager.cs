using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class StinkTile
{
    public Tilemap _stinkTilemap;
    public TileBase _tile;
    public Vector3Int _tileGridPos;
    public float _maxTimeAlive = 3f;
    public float _timeLiving = 0f;


    public StinkTile(TileBase tile, Tilemap stinkTilemap, Vector3Int gridPos)
    {
        _tile = tile;
        _stinkTilemap = stinkTilemap;
        _tileGridPos = gridPos;
        destroyTile();
    }
    void destroyTile()
    {
        while (_timeLiving <= _maxTimeAlive)
        {
            _timeLiving += Time.deltaTime;
        }
        _stinkTilemap.SetTile(_tileGridPos, null);
    }

    public void UpdateTile()
    {
        _timeLiving = 0f;
    }
}
public class StinkManager : MonoBehaviour
{
    public Tilemap stinkMap;
    public TileBase tile;

    List<StinkTile> stinkTileList = new List<StinkTile>();

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int gridPosition = stinkMap.WorldToCell(mousePos);

            SetStinkTile(gridPosition);
        }

    }

    void SetStinkTile(Vector3Int gridPos)
    {
        foreach(var stink in stinkTileList)
        {
            if (stink._tileGridPos != gridPos)
            {
                var stinkTile = new StinkTile(tile, stinkMap, gridPos);
                stinkTileList.Add(stinkTile);
            }
            else
            {
                stink.UpdateTile();
            } 
        }
        
    }
}
