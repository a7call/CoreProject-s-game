using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
/// <summary>
/// Classe héritière de Distance.cs
/// Elle contient les fonctions de la classe mère
/// </summary>
public class SnotBubble : Distance
{

    private bool alreadyActive;
    StinkManager stinkManager;
    protected override void Start()
    {
        
        SetData();
        stinkManager = FindObjectOfType<StinkManager>();
        base.Start();

    }
    protected override void Update()
    {
        base.Update();
        stinkManager.SetStinkTile(transform.position, 3);
    }
    //protected IEnumerator ZoneCo()
    //{
    //    if (!alreadyActive)
    //    {
    //        alreadyActive = true;
    //        yield return new WaitForSeconds(0.8f);
    //        Instantiate(projetile, transform.position, Quaternion.identity);
    //        alreadyActive = false;
    //    }
    //}

}


