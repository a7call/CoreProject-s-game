using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Teleportation : MonoBehaviour
{
    private Player player;
    public GameObject portal;

    private float reloadDelay = 3f;
    [SerializeField] private static bool canTp = true;


    private void Start()
    {
        player = FindObjectOfType<Player>();
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && canTp)
        {
            StartCoroutine(Teleporte());
        }
    }

    private IEnumerator Teleporte()
    {
        Teleportation.canTp = false;
        player.transform.position = portal.transform.position;
        yield return new WaitForSeconds(reloadDelay);
        Teleportation.canTp = true;
    }
}
