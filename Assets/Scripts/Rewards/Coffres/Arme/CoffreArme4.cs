using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffreArme4 : Coffre
{
    [SerializeField] protected List<GameObject> ListeArmes4;
    public static List<GameObject> SListeArmes4;

    protected void Start()
    {
        SListeArmes4 = ListeArmes4;
    }


    public override void PopRandomObject()
    {
        int Choice = Random.Range(0, SListeArmes4.Count);
        GameObject.Instantiate(SListeArmes4[Choice], transform.position, Quaternion.identity);
        SListeArmes4.Remove(SListeArmes4[Choice]);
    }
}
