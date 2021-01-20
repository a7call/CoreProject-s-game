using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffreObject4 : Coffre
{
    [SerializeField] protected List<GameObject> ListeObjects4;
    public static List<GameObject> SListeObjects4;

    protected void Start()
    {
        SListeObjects4 = ListeObjects4;
    }


    public override void PopRandomObject()
    {
        int Choice = Random.Range(0, SListeObjects4.Count);
        GameObject.Instantiate(SListeObjects4[Choice], transform.position, Quaternion.identity);
        SListeObjects4.Remove(SListeObjects4[Choice]);
    }
}
