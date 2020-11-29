using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityBomb : ModuleLauchPhase
{
    [SerializeField] private float timeBeforActivation = 0f;
    [SerializeField] private float timeBeforDesactivation = 0f;
    [SerializeField] private float radius = 0f;
    [SerializeField] private float explosionDamage = 0f;
    [SerializeField] private LayerMask hit = 0;
    
   
    

    private List<GameObject> enemiesSlowed;

    protected override void Start()
    {
        base.Start();
        enemiesSlowed = new List<GameObject>();
      

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

    protected override void Update()
    {
        base.Update();
        if(isNotMoving && !isAlreadyActive)
        {
            isAlreadyActive = true;
            Invoke("Activation", timeBeforActivation);
            StartCoroutine(Desactivation());
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
