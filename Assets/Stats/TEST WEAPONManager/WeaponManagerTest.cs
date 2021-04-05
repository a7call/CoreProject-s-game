using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManagerTest : MonoBehaviour
{
    private PlayerMouvement player;
    private void Start()
    {
        player = transform.parent.GetComponent<PlayerMouvement>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CacWeapon") || collision.CompareTag("DistanceWeapon"))
        {
        }
    }
}
