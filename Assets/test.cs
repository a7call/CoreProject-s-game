using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class test : MonoBehaviour
{
    private Renderer rendere;
    private Collider2D col;
    public GameObject dooor;
    void Start()
    {
       GameObject toto=  Instantiate(dooor, transform.position, Quaternion.identity);
        toto.GetComponent<SpriteRenderer>().sortingOrder = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
