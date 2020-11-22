using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffreObject1 : Coffre
{
    [SerializeField] protected List<GameObject> ListeObjects1;
    public static List<GameObject> SListeObjects1;

    protected void Start()
    {
        SListeObjects1 = ListeObjects1;
    }


    protected override void PopRandomObject()
    {
        int Choice = Random.Range(0, SListeObjects1.Count);
        GameObject.Instantiate(SListeObjects1[Choice], transform.position, Quaternion.identity);
        SListeObjects1.Remove(SListeObjects1[Choice]);
    }
}
