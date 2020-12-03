using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoConverter : ActiveObjects
{

    //private GameObject player;
    [SerializeField] private float activeTime = 0;
    [SerializeField] private float zoneRadius = 0;
    protected bool isActive = false;

    protected override void Start()
    {
        base.Start();
        CircleCollider2D CirlceCollider = gameObject.AddComponent<CircleCollider2D>();
        CirlceCollider.radius = zoneRadius;
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
    }

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
        gameObject.GetComponent<CircleCollider2D>().enabled = true;

        yield return new WaitForSeconds(activeTime);

        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        readyToUse = true;
        isActive = false;
        
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyProjectil"))
        {
            
            Player player = FindObjectOfType<Player>();
            
            WeaponsManagerSelected weaponManager =  player.GetComponentInChildren<WeaponsManagerSelected>();
            if(weaponManager.GetComponentInChildren<DistanceWeapon>())
            {
                DistanceWeapon distanceWeapon = weaponManager.GetComponentInChildren<DistanceWeapon>();
                distanceWeapon.AmmoStock++;

            }
            else
            {
                return;
            }




        }
    }
    
    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, zoneRadius);
    }

   
}