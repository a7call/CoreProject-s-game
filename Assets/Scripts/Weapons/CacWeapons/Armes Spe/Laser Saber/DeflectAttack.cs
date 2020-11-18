using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeflectAttack : MonoBehaviour
{
    private Collider2D coll;
    [SerializeField] private float deflectTime = 0f;

    private void Start()
    {
        coll = gameObject.GetComponent<Collider2D>();
    }

    public IEnumerator DeflectProjectils()
    {
        coll.enabled = true;
        yield return new WaitForSeconds(deflectTime);
        coll.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyProjectil"))
        {
            print(collision.gameObject);
            Projectile projectils = collision.GetComponent<Projectile>();
            projectils.dir = -projectils.dir;
        }

    }

}
