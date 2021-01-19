using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class tileposScrip : MonoBehaviour
{
    private Tilemap tileMap = null;

    public List<Vector3> availablePlaces;

    void Start()
    {
        
        tileMap = transform.GetComponent<Tilemap>();
        availablePlaces = new List<Vector3>();

        for (int n = tileMap.cellBounds.xMin; n < tileMap.cellBounds.xMax; n++)
        {
            for (int p = tileMap.cellBounds.yMin; p < tileMap.cellBounds.yMax; p++)
            {
                Vector2Int localPlace = (new Vector2Int(n, p));
                Vector2 place = tileMap.CellToWorld((Vector3Int)localPlace);
                
                if (tileMap.HasTile((Vector3Int)localPlace))
                {
                    //Tile at "place"
                    print(localPlace);
                    availablePlaces.Add(place);
                }
                else
                {
                    //No tile at "place"
                }
            }
        }
    }
}
