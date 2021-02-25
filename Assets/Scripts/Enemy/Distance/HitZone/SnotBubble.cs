using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Classe héritière de Distance.cs
/// Elle contient les fonctions de la classe mère
/// </summary>
public class SnotBubble : Distance
{

    private bool alreadyActive;

    void Start()
    {
        // Set data
        SetData();
        SetMaxHealth();
    }
    protected override void Update()
    {
        base.Update();
        switch (currentState)
        {
            case State.Chasing:
                StartCoroutine(ZoneCo());
                // suit le path créé et s'arrête pour tirer
                break;

        }

    }
    protected IEnumerator ZoneCo()
    {
        if (!alreadyActive)
        {
            alreadyActive = true;
            yield return new WaitForSeconds(0.8f);
            Instantiate(projetile, transform.position, Quaternion.identity);
            alreadyActive = false;
        }
    }

}


