using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffreObject2 : Coffre
{
    [SerializeField] protected List<GameObject> ListeObjects2;
    public static List<GameObject> SListeObjects2;

    protected void Start()
    {
        SListeObjects2 = ListeObjects2;
    }


    protected override void PopRandomObject()
    {
        int Choice = Random.Range(0, SListeObjects2.Count);
        GameObject.Instantiate(SListeObjects2[Choice], transform.position, Quaternion.identity);
        SListeObjects2.Remove(SListeObjects2[Choice]);
    }
}
