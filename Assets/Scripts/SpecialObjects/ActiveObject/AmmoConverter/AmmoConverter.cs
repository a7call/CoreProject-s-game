using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoConverter : ActiveObjects
{

    private GameObject player;
    [SerializeField] private float activeTime = 0;
    [SerializeField] private float zoneRadius = 0;
    protected bool isActive = false;



    protected override void Update()
    {
        base.Update();

        if (UseModule)
        {
            StartCoroutine(ActiveTime());
            UseModule = false;
            isActive = true;

        }



    }

    protected virtual IEnumerator ActiveTime()
    {
        
        
        Physics2D.OverlapCircleAll(transform.position, zoneRadius);

        gameObject.GetComponent<Collider2D>().enabled = true;

        yield return new WaitForSeconds(activeTime);
        gameObject.GetComponent<Collider2D>().enabled = false;
        readyToUse = true;
        isActive = false;
        //Desactivation();
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyProjectil"))
        {
           
            GameObject Arme = GameObject.FindGameObjectWithTag("DistanceWeapon");

            DistanceWeapon ArmeScript = Arme.GetComponent<DistanceWeapon>();

            ArmeScript.AmmoStock++;
            
        }
    }
    
    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, zoneRadius);
    }

   
}