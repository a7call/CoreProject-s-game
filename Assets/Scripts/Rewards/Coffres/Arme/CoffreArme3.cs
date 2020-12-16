using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffreArme3 : Coffre
{
    [SerializeField] protected List<GameObject> ListeArmes3;
    public static List<GameObject> SListeArmes3;

    protected void Start()
    {
        SListeArmes3 = ListeArmes3;
    }


    public override void PopRandomObject()
    {
        int Choice = Random.Range(0, SListeArmes3.Count);
        GameObject.Instantiate(SListeArmes3[Choice], transform.position, Quaternion.identity);
        SListeArmes3.Remove(SListeArmes3[Choice]);
    }
}
