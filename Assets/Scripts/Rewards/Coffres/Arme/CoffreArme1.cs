using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffreArme1 : Coffre
{
    [SerializeField] protected List<GameObject> ListeArmes1;
    public static List<GameObject> SListeArmes1;

    protected void Start()
    {
        SListeArmes1 = ListeArmes1;
    }


    public override void PopRandomObject()
    {
        int Choice = Random.Range(0, SListeArmes1.Count);
        GameObject.Instantiate(SListeArmes1[Choice], transform.position, Quaternion.identity);
        SListeArmes1.Remove(SListeArmes1[Choice]);
    }
}
