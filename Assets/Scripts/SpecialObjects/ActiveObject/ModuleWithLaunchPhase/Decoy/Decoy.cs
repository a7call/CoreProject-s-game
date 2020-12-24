using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decoy : ModuleLauchPhase
{
    private Player player;

    [SerializeField] private float timeBeforDesactivation;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask hit;

    protected override void Start()
    {
        base.Start();
        player = FindObjectOfType<Player>();
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
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius, hit);
        foreach (Collider2D hit in hits)
        {
            if (hit.gameObject.GetComponent<Enemy>())
            {
                Enemy enemy = hit.gameObject.GetComponent<Enemy>();
                //enemy.targetPoint = gameObject.transform;
                enemy.target = gameObject.transform;
                enemy.currentState = Enemy.State.Chasing;
            }
        }
        yield return new WaitForSeconds(timeBeforDesactivation);

        foreach (Collider2D hit in hits)
        {
            if (hit.gameObject.GetComponent<Enemy>())
            {
                Enemy enemy = hit.gameObject.GetComponent<Enemy>();
               // enemy.targetPoint = player.transform ;
                enemy.target = player.transform;
                enemy.currentState = Enemy.State.Chasing;
            }
        }
        Destroy(gameObject);
    }

}
