using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    private Collider2D[] hits;
    [SerializeField] private float range;
    [SerializeField] private float speed;
    [SerializeField] private float radiusSpeed;
    [SerializeField] private float timeToExplod;
    [SerializeField] private LayerMask hitLayer;
    List<GameObject> hitsList = new List<GameObject>();

    private void Start()
    {
        StartCoroutine(DestroyAll());
    }
    private void Update()
    {
        GetAllEnemies();
        if (hitsList.Count > 1) AttractAllEnemies();
    }

    private void GetAllEnemies()
    {
        hits = Physics2D.OverlapCircleAll(transform.position,range, hitLayer);


        foreach(Collider2D hit in hits)
        {
            if(hit.CompareTag("Enemy"))
            {
                hitsList.Add(hit.gameObject);
            }
            
           
        }
    }

    void AttractAllEnemies()
    {
        foreach(GameObject hit in hitsList)
        {
            if (hit == null) continue;
            if (hit.CompareTag("Enemy"))
            {
                Transform hitTrans = hit.GetComponent<Transform>();
                Vector2 dir = (hitTrans.position - transform.position).normalized;
                hitTrans.Translate(-dir * radiusSpeed * Time.deltaTime, Space.World); ;
                hit.transform.RotateAround(transform.position, Vector3.forward, speed * Time.deltaTime);
                hit.GetComponent<Enemy>().currentState = Enemy.State.Paralysed;
                hit.GetComponent<Enemy>().aIPath.canMove = false;
                hit.GetComponent<BoxCollider2D>().enabled = false;
                
            }
        }
    }

    private IEnumerator DestroyAll()
    {
        yield return new WaitForSeconds(timeToExplod);
        foreach (GameObject hit in hitsList)
        {
            Destroy(hit);
            
        }
        hitsList.Clear();
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}

