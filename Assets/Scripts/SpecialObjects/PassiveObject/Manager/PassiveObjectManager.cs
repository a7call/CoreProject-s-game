﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveObjectManager : MonoBehaviour
{

    public static List<IAbility> currentAbilities = new List<IAbility>();
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PassiveObject"))
        {
            collision.transform.parent = gameObject.transform;
            collision.GetComponent<PassiveObjects>().enabled = true;
            collision.transform.position = gameObject.transform.position;
            collision.GetComponent<Collider2D>().enabled = false;
            collision.GetComponent<PassiveObjects>().passiveObjectsDatas.Equip(transform.parent.GetComponent<Player>());
        }
    }

}
