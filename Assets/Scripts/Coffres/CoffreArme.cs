using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffreArme : MonoBehaviour
{
    protected bool isOpen = false;
    
    [SerializeField] protected GameObject[] ListArmes;
   

  

    protected virtual void OnTriggerStay2D(Collider2D collision)
    {
        Inventory inventory = FindObjectOfType<Inventory>();
        Debug.Log(inventory.numberOfKeys);

        if (collision.CompareTag("Player") && !isOpen && inventory.numberOfKeys>=1 && Input.GetKeyDown(KeyCode.E))
        {
            isOpen = true;
            inventory.numberOfKeys--;
            PopRandomWeapon();
        }
            
    }

    protected void PopRandomWeapon()
    {
        int ArmeChoice = Random.Range(1, ListArmes.Length);
        GameObject.Instantiate(ListArmes[ArmeChoice], transform.position, Quaternion.identity);
        Debug.Log(ArmeChoice);
    }
}
