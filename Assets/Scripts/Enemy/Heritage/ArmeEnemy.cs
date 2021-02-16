using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmeEnemy : MonoBehaviour
{

    protected SpriteRenderer spriteRenderer;
    protected Vector3 rotationVector;
    protected Vector3 PositionArme;
    protected Enemy enemy;


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent <SpriteRenderer>();
        enemy = GetComponentInParent<Enemy>();

        PositionArme = transform.position - enemy.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 dir = (enemy.target.position - enemy.transform.position).normalized;

        if (dir.x <= 0)
        {
            spriteRenderer.flipX = true;
            transform.position = new Vector3(-PositionArme.x, PositionArme.y, PositionArme.z) + enemy.transform.position;
        }
        else if (dir.x > 0)
        {
            spriteRenderer.flipX = false;
            transform.position = PositionArme + enemy.transform.position;
        }

        rotationVector.z = Mathf.Atan(dir.y / dir.x)*Mathf.Rad2Deg;
        
        transform.rotation = Quaternion.Euler(rotationVector);
        
    }
}
