using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOgScripy : MonoBehaviour
{
    private Animator animator;
    
    void Start()
    {
        animator = GetComponent<Animator>(); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.SetTrigger("isTriggered");
        }
    }
}
