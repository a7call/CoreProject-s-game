using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coffre : MonoBehaviour
{
    protected bool isOpen = false;
    
    [SerializeField] protected GameObject[] ListArmes;

    //PassePartoutModule
    [HideInInspector]
    public static bool isKeyPassePartoutModule =false;
    


    protected virtual void OnTriggerStay2D(Collider2D collision)
    {
        Inventory inventory = FindObjectOfType<Inventory>();
        

        if (collision.CompareTag("Player") && !isOpen && Input.GetKeyDown(KeyCode.E))
        {
            if ( inventory.numberOfKeys >= 1 && !isKeyPassePartoutModule)
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

    protected void PopRandomObject()
    {
        int ArmeChoice = Random.Range(0, ListArmes.Length);
        GameObject.Instantiate(ListArmes[ArmeChoice], transform.position, Quaternion.identity);
        
    }
}
