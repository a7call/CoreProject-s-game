using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmeEnemy : MonoBehaviour
{

    protected SpriteRenderer spriteRenderer;
    protected Vector3 rotationVector;
    protected Vector3 PositionArme;
    protected Vector3 posAttackPoint;
    Transform attackPoint;
    protected Enemy enemy;
    Vector3 dirWep;


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent <SpriteRenderer>();
        enemy = GetComponentInParent<Enemy>();

        PositionArme = transform.localPosition;
        dirWep = (gameObject.transform.position - enemy.transform.position).normalized;

    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 dir = (enemy.target.position - enemy.transform.position).normalized;



        
        
         if (dir.x <= 0 && !spriteRenderer.flipX)
         {
             spriteRenderer.flipX = true;
             attackPoint = transform.Find("attackPoint");
             posAttackPoint = attackPoint.localPosition ;
             attackPoint.localPosition = new Vector3(-posAttackPoint.x, posAttackPoint.y, posAttackPoint.z);
             transform.localPosition = new Vector3(-PositionArme.x, PositionArme.y, PositionArme.z);

         }
         else if (dir.x > 0 && spriteRenderer.flipX)
         {
             spriteRenderer.flipX = false;
             transform.localPosition = PositionArme;
             attackPoint.localPosition = posAttackPoint;
         }
         rotationVector.z = Mathf.Atan(dir.y / dir.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(rotationVector);




    }
}
