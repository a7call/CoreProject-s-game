using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityBomb : MonoBehaviour
{
    [SerializeField] private float timeBeforActivation = 0f;
    [SerializeField] private float timeBeforDesactivation = 0f;
    [SerializeField] private float radius = 0f;
    [SerializeField] private float explosionDamage = 0f;
    [SerializeField] private LayerMask hit = 0;
    private Vector3 positionMouse;
    private Vector3 playePos;
    private Vector3 direction;
    private ActiveObjects module;

    private List<GameObject> enemiesSlowed;

    void Start()
    {
        enemiesSlowed = new List<GameObject>();
        module = FindObjectOfType<ActiveObjects>();
        positionMouse = module.GetMousePosition();
        playePos = module.transform.position;
        direction = module.GetDirection().normalized;
        Invoke("Activation", timeBeforActivation);
        StartCoroutine(Desactivation());

    }
    private void Update()
    {
        Launch(positionMouse, playePos, module.range, direction);
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


    protected void Launch(Vector3 mousePos, Vector3 playerPos, float range, Vector3 dir)
    {
        if (Vector3.Distance(mousePos, playerPos) < range)
        {
            if (Vector3.Distance(mousePos, transform.position) > 0.3)
            {
                transform.Translate(dir * 1.5f * Time.deltaTime);
            }
            else
            {
                transform.position = mousePos;
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, playePos) < range)
            {
                transform.Translate(dir * 5 * Time.deltaTime);
            }
        }
    }
}
