using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{

    public GameObject brokenParts;
    public float force = 5f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
       GameObject brokenObj = Instantiate(brokenParts, transform.position, Quaternion.identity);
        foreach(Transform parts in brokenObj.transform)
        {
            Rigidbody2D rb = parts.GetComponent<Rigidbody2D>();
          
            Vector2 dir = -parts.position + transform.position ;
            rb.AddForce(dir.normalized * force);
            Collider2D col = parts.GetComponent<Collider2D>();
            col.enabled = false;
            CoroutineManager.Instance.StartCoroutine(ShouldStopMoving(rb));
        }
        
        Destroy(gameObject);
    }

    private IEnumerator ShouldStopMoving(Rigidbody2D rb)
    {
            yield return new WaitForSeconds(0.4f);
            rb.velocity = Vector2.zero;
    }


}
