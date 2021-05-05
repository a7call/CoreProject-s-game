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
    public Color _Color;



    public StinkTile(TileBase tile, Tilemap stinkTilemap, Vector3Int gridPos)
    {
        _tile = tile;
        _stinkTilemap = stinkTilemap;
        _tileGridPos = gridPos;
        _stinkTilemap.SetTile(gridPos, _tile);
        _Color = _stinkTilemap.color;
    }

}
public class StinkManager : MonoBehaviour
{
    public Tilemap stinkMap;
    public TileBase[] tiles;
    public int radius;

    public List<StinkTile> stinkTileList = new List<StinkTile>();

    private void Update()
    {
        
        DestroyStinkTiles();
        SetSideTiles();
    }

    public void SetStinkTile(Vector3 worldPosition, int radius)
    {

        Vector3Int gridPosition = stinkMap.WorldToCell(worldPosition);
        for (int x = -radius; x <= radius; x++)
        {
            for (int y = -radius; y <= radius; y++)
            {
                float distanceFromCenter = Mathf.Abs(x) + Mathf.Abs(y);
                if (distanceFromCenter <= radius)
                {
                    TileBase tile = tiles[Random.Range(0, tiles.Length)];
                    checkTileMap(gridPosition.x + x, gridPosition.y + y);
                    Vector3Int gridPos = new Vector3Int(gridPosition.x + x, gridPosition.y + y, 0);
                    var stinkTile = new StinkTile(tile, stinkMap, gridPos);
                    stinkTileList.Add(stinkTile);

                }
            }
        }
    }
    void SetSideTiles()
    {
        foreach(var stink in stinkTileList.ToArray())
        {
            int neighboorCounter = 0;
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if ((i == 0 && j == 0 ) || (i == 1 && j == 1) || (i == -1 && j == -1) || (i == -1 && j == 1) || (i == 1 && j == -1))
                        continue;
                    var neigboorsPos = new Vector3Int(stink._tileGridPos.x + i, stink._tileGridPos.y + j, 0);
                    if (stinkMap.GetTile(neigboorsPos) != null)
                        neighboorCounter++;
                }
            }
            if(stinkMap.GetTile(stink._tileGridPos) == tiles[0] && neighboorCounter >= 4)
            {
                stinkMap.SetTile(stink._tileGridPos, tiles[2]);
            }
            if (neighboorCounter < 4)
                stinkMap.SetTile(stink._tileGridPos, tiles[0]);

        }
    }

    void checkTileMap(int gridPosX, int gridPosY)
    {
        foreach (var stink in stinkTileList.ToArray())
        {
            if (stink._tileGridPos.x == gridPosX && stink._tileGridPos.y == gridPosY)
            {
                Vector3Int gridPos = new Vector3Int(gridPosX, gridPosY, 0);
                stinkMap.SetTile(gridPos, null);
                stinkTileList.Remove(stink);
            }
        }
    }


    void DestroyStinkTiles()
    {
        foreach (var stinkTile in stinkTileList.ToArray())
        {
            stinkTile._maxTimeAlive -= Time.deltaTime / 2;

            //var alpha = stinkTile._Color.a -= Time.deltaTime / 4;
            //var color = new Color(stinkTile._Color.r, stinkTile._Color.g, stinkTile._Color.b, alpha);
            //stinkMap.SetTileFlags(stinkTile._tileGridPos, TileFlags.None);
            //stinkMap.SetColor(stinkTile._tileGridPos, color);
            //stinkMap.SetTileFlags(stinkTile._tileGridPos, TileFlags.LockColor);
            if (stinkTile._maxTimeAlive <= 0)
            {
                stinkMap.SetTile(stinkTile._tileGridPos, null);
                stinkTileList.Remove(stinkTile);
            }
        }
    }
}

