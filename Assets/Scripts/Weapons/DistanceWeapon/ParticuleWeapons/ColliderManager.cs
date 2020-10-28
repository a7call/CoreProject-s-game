using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderManager : MonoBehaviour
{
    [SerializeField] protected float damageTimer;
    protected bool readyToHit = true;
    private List<GameObject> Targets = new List<GameObject>();
    ParticleSystem particules;

    private void Start()
    {
        particules = gameObject.GetComponentInChildren<ParticleSystem>();
    }
    protected  void Update()
    {
        if (Input.GetMouseButton(0))
        {
            ActivateShoot();
            if (readyToHit)
            {
                StartCoroutine(DamageCO());
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            DesactivateShoot();
        }
        

    }

    protected void ActivateShoot()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        var emision = particules.emission;
        emision.enabled = true;
        // Active le particuleSysteme;
    }
    protected void DesactivateShoot()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;

        var emision = particules.emission;
        emision.enabled = false;

        // Desactive le particuleSysteme;
    }

    void OnTriggerEnter2D(Collider2D Col)
    {
        if (Col.tag == "Enemy")
        {
            Targets.Add(Col.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D Col)
    {
        if (Col.tag == "Enemy")
        {
            Targets.Remove(Col.gameObject);
        }
    }
    private IEnumerator DamageCO()
    {
        readyToHit = false;
        
        for (int Cont = 0; Cont < Targets.Count; Cont++)
        {
            Enemy enemy = Targets[Cont].GetComponent<Enemy>();
            enemy.TakeDamage(1);
        }
        yield return new WaitForSeconds(damageTimer);
        readyToHit = true;
    }

    

}
