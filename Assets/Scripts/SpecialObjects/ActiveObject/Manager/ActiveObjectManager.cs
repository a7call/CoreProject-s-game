using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveObjectManager : MonoBehaviour
{

    private void Start()
    {
    }

    private void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ActiveObject"))
        {
            collision.transform.parent = gameObject.transform;
            collision.GetComponent<ActiveObjects>().enabled = true;
            collision.transform.position = gameObject.transform.position;
            collision.GetComponent<Collider2D>().enabled = false;
        }
    }

}
