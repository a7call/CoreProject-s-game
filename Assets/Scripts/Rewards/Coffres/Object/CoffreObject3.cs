using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffreObject3 : Coffre
{
    [SerializeField] protected List<GameObject> ListeObjects3;
    public static List<GameObject> SListeObjects3;

    protected void Start()
    {
        SListeObjects3 = ListeObjects3;
    }


    protected override void PopRandomObject()
    {
        int Choice = Random.Range(0, SListeObjects3.Count);
        GameObject.Instantiate(SListeObjects3[Choice], transform.position, Quaternion.identity);
        SListeObjects3.Remove(SListeObjects3[Choice]);
    }
}
