using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveObjectManager : MonoBehaviour
{

    private void Start()
    {
    }

    private void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PassiveObject"))
        {
            collision.transform.parent = gameObject.transform;
            collision.GetComponent<PassiveObjects>().enabled = true;
            collision.transform.position = gameObject.transform.position;
            collision.GetComponent<Collider2D>().enabled = false;
        }
    }

}
