using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rewards : MonoBehaviour
{
    protected Inventory inventory;

    protected virtual void Start()
    {
        SetLayer();
        Transform player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        inventory = FindObjectOfType<Inventory>();
        foreach (Transform child in player)
        {
            if (child.GetComponent<Collider2D>())
            {
                Physics2D.IgnoreCollision(child.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            }

        }
       
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);  
    }

    void SetLayer()
    {
        gameObject.layer = 7;
    }
}
