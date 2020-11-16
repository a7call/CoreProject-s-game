using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotProjectile : MonoBehaviour
{
    private assitantRobot robot;
    [SerializeField] float speed = 0f;
    private Vector2 dir;
    private void Awake()
    {
        robot = FindObjectOfType<assitantRobot>();
        dir = robot.dir;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Launch();
    }
    private void Launch()
    {
        transform.Translate(dir * speed * Time.deltaTime);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            enemy.TakeDamage(robot.damage);
            Destroy(gameObject);
        }
    }
}
