using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rewards : MonoBehaviour
{
    protected Inventory inventory;
    void Start()
    { 
        inventory = FindObjectOfType<Inventory>();

    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }

}
