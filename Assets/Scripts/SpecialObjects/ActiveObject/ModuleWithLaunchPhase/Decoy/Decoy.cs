using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decoy : ModuleLauchPhase
{

    [SerializeField] private float timeBeforDesactivation;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask hit;

    protected override void Start()
    {
        base.Start();
        StartCoroutine(DecoyFunction());
    }
    protected override void Update()
    {
        base.Update();
        if (isNotMoving && !isAlreadyActive)
        {
            isAlreadyActive = true;
            StartCoroutine(DecoyFunction());
        }
    }

    private IEnumerator DecoyFunction()
    {
        yield return new WaitForSeconds(timeBeforDesactivation);
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius, hit);
        foreach (Collider2D hit in hits)
        {
            if (hit.gameObject.GetComponent<Enemy>())
            {
                Enemy enemy = hit.gameObject.GetComponent<Enemy>();
                //Vector3 Direction = (enemy.transform.position - gameObject.transform.position).normalized;
                //enemy.rb.velocity = Direction * enemy.moveSpeed * Time.deltaTime;
            }
        }
        Destroy(gameObject);
    }

}
