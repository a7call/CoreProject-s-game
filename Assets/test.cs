using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class test : MonoBehaviour
{
    private Renderer rendere;
    private Collider2D col;
    void Start()
    {
        if (GetComponent<TilemapRenderer>())
        {
            rendere = GetComponent<TilemapRenderer>();
            Debug.LogWarning(rendere.bounds);
        }
        else
        {
            col = GetComponent<Collider2D>();
            print(col.bounds.size);
        }
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
