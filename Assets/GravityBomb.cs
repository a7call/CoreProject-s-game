using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityBomb : MonoBehaviour
{
    [SerializeField] private float timeBeforActivation;
    [SerializeField] private float timeBeforDesactivation;
    [SerializeField] private float radius;
    [SerializeField] private float explosionDamage;
    [SerializeField] private LayerMask hit;

    private List<GameObject> enemiesSlowed;

    void Start()
    {
        enemiesSlowed = new List<GameObject>();
        Invoke("Activation", timeBeforActivation);
        StartCoroutine(Desactivation());

    }
    private void Activation()
    {
        GetComponent<Collider2D>().enabled = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().moveSpeed /= 2f;
            enemiesSlowed.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().moveSpeed *= 2f;
            enemiesSlowed.Remove(collision.gameObject);
        }
    }

    private IEnumerator Desactivation()
    {
        yield return new WaitForSeconds(timeBeforDesactivation);
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius, hit);
        foreach(Collider2D hit in hits) {
            if (hit.gameObject.GetComponent<Enemy>())
            {
                hit.GetComponent<Enemy>().TakeDamage(explosionDamage);
            }
        }
        Destroy(gameObject);
    }
}
