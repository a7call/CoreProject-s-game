using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedIncreaseProjectile : Projectile
{
    public float speedIncreasing;

    // Start is called before the first frame update
    void Start()
    {
        GetDirection();
    }

    // Update is called once per frame
    void Update()
    {
        CalculDistance();
        IncreasingSpeed();
        Lauch();
        print(distance + " et la vitesse " + speedIncreasing);
    }

    // Calculer intelligement cette fonction
    private void IncreasingSpeed()
    {
        if (distance < 2)
        {
            speedIncreasing = 25;
        }
        else
        {
            speedIncreasing = speed;
        }
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
