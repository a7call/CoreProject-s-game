using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoConverter : CdObjects
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
            

        } 
        
    }

    protected virtual IEnumerator ActiveTime()
    {
        gameObject.GetComponent<CircleCollider2D>().enabled = true;
        isActive = true;

        yield return new WaitForSeconds(activeTime);

        isActive = false;
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("EnemyProjectil") && isActive)
        {
            
            Player player = FindObjectOfType<Player>();
            
            WeaponsManagerSelected weaponManager =  player.GetComponentInChildren<WeaponsManagerSelected>();
            if(weaponManager.GetComponentInChildren<BaseShootableWeapon>())
            {
                BaseShootableWeapon distanceWeapon = weaponManager.GetComponentInChildren<BaseShootableWeapon>();
                distanceWeapon.ammoStock++;
                

            }
            else
            {
                return;
            }
            Destroy(collision.gameObject);




        }
    }
    
    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, zoneRadius);
    }

   
}