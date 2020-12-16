using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coffre : MonoBehaviour
{
    public bool isOpen = false;
    public bool OkToOpen = false;
    
    public static List<GameObject> SListeObjects;

    //PassePartoutModule
    [HideInInspector]
    public static bool isKeyPassePartoutModule = false;



    protected virtual void OnTriggerStay2D(Collider2D collision)
    {
        Inventory inventory = FindObjectOfType<Inventory>();


        if (collision.CompareTag("Player") && !isOpen && OkToOpen)
        {
            if (inventory.numberOfKeys >= 1 && !isKeyPassePartoutModule)
            {
                isOpen = true;

                inventory.numberOfKeys--;
                PopRandomObject();
            }

            else if (isKeyPassePartoutModule)
            {
                isOpen = true;

                PopRandomObject();
            }
            else
            {
                return;
            }

        }

    }

    public virtual void PopRandomObject()
    {
        
        int Choice = Random.Range(0, SListeObjects.Count);
        GameObject.Instantiate(SListeObjects[Choice], transform.position, Quaternion.identity);
        SListeObjects.Remove(SListeObjects[Choice]);
    }

}