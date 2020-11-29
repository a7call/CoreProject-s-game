using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistortionGrenade : ModuleLauchPhase
{
    [SerializeField] private float timeBeforDesactivation;
    [SerializeField] private float radius;
    [SerializeField] private float explosionDamage;
    [SerializeField] private LayerMask hit;
    [SerializeField] protected float knockBackForce;
    [SerializeField] protected float knockBackTime;

    protected override void Start()
    {
        base.Start();
        StartCoroutine(Explosion());
    }
    private IEnumerator Explosion()
    {
        yield return new WaitForSeconds(timeBeforDesactivation);
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius, hit);
        foreach (Collider2D hit in hits)
        {
            if (hit.gameObject.GetComponent<Enemy>())
            {
                Enemy enemy = hit.gameObject.GetComponent<Enemy>();

                CoroutineManager.Instance.StartCoroutine(enemy.KnockCo(knockBackForce, RandomDir(), knockBackTime, enemy));
                enemy.TakeDamage(explosionDamage);
            }
        }
        Destroy(gameObject);
    }

    protected override void Update()
    {
        base.Update();
        if (isNotMoving && !isAlreadyActive)
        {
            isAlreadyActive = true;
            StartCoroutine(Explosion());
        }
    }
    private Vector3 RandomDir()
    {
        int choice = Mathf.FloorToInt(Random.value * 3.99f);
        //use that int to chose a direction
        switch (choice)
        {
            case 0:
                return Vector2.down;
            case 1:
                return Vector2.left;
            case 2:
                return Vector2.up;
            default:
                return Vector2.right;
        }
    }
}
