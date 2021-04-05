using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityAcceleratorBomb : MonoBehaviour
{
    [SerializeField] private float timeBeforActivation;
    [SerializeField] private float timeBeforDesactivation;
    private List<GameObject> enemiesSlowed;

    void Start()
    {
        enemiesSlowed = new List<GameObject>();
        Invoke("Activation", timeBeforActivation);
        CoroutineManager.Instance.StartCoroutine(Desactivation());
        
    }
    private void Activation()
    {

        GetComponent<CircleCollider2D>().enabled = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().aIPath.maxSpeed /= 2f;
            enemiesSlowed.Add(collision.gameObject);
        }
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().mooveSpeed /= 2f;
           
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().aIPath.maxSpeed *= 2f;
            enemiesSlowed.Remove(collision.gameObject);
        }
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().mooveSpeed *= 2f;

        }
    }

    private IEnumerator Desactivation()
    {
        yield return new WaitForSeconds(timeBeforDesactivation);
        Destroy(gameObject);
    }


}
