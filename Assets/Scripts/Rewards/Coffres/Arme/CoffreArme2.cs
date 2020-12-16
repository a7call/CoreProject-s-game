using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffreArme2 : Coffre
{
    [SerializeField]protected List<GameObject> ListeArmes2;
    public static List<GameObject> SListeArmes2;

    protected void Start()
    {
        SListeArmes2 = ListeArmes2;
    }


    public override void PopRandomObject()
    {
        int Choice = Random.Range(0, SListeArmes2.Count);
        GameObject.Instantiate(SListeArmes2[Choice], transform.position, Quaternion.identity);
        SListeArmes2.Remove(SListeArmes2[Choice]);
    }
}
