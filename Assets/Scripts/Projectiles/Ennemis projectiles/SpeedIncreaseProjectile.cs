using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedIncreaseProjectile : Projectile
{
    private float speedIncreasing;
    private float time = 1f;

    // Start is called before the first frame update
    void Start()
    {
        GetDirection();
    }

    // Update is called once per frame
   protected override void Update()
    {
        CalculDistance();
        IncreasingSpeed();
        base.Update();
    }

    // Fonction qui augmente la vitesse du tir lorsque le projectile se rapproche du joueur toutes les times secondes
    private void IncreasingSpeed()
    {
        speedIncreasing = speed/distance;
        StartCoroutine(WaitingTime());
    }

    // Coroutine pour espacer la durée d'accroissement de vitesse du tir
    private IEnumerator WaitingTime()
    {
        yield return new WaitForSeconds(time);
    }

    protected override void Lauch()
    {
        transform.Translate(dir * speedIncreasing * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
        // Damage
    }

    protected override void CalculDistance()
    {
        base.CalculDistance();
    }

    protected override void GetDirection()
    {
        base.GetDirection();
    }

}
