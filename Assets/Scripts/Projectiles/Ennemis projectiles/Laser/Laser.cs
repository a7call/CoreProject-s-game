using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Classe héritière de Projectile.cs
/// Contient les fonctions de la classe mères
/// </summary>
public class Laser : Projectile
{
    protected bool damageDone = false;
    
    // Start is called before the first frame update
    void Start()
    {
        GetDirection();
    }

    // Update is called once per frame
    void Update()
    {
        //Lauch();
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir);

        Debug.DrawRay(transform.position, dir * 10, Color.red);

        if (hit.collider.CompareTag("Player") && !damageDone)
        {
            //take Damage
            Debug.Log("Damage");
            damageDone = true;

        }
        StartCoroutine(destroy());

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
        // Damage
    }

    protected override void GetDirection()
    {
        base.GetDirection();
    }

    protected override void Lauch()
    {
        base.Lauch();
    }

    protected IEnumerator destroy()
    {
        yield return new WaitForSeconds(ActiveTime);
        Destroy(gameObject);
    }
}






