using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotProtecteur : MonoBehaviour
{
    [SerializeField] private float RotateSpeed = 0f;
    [SerializeField] private float Radius = 0f;
    [SerializeField] private int damage = 0;
    private Vector2 _centre;
    private float _angle;
    private GameObject player;

    private void Start()
    {
        
        player = GameObject.FindGameObjectWithTag("Player");
        Physics2D.IgnoreCollision(player.GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>());
        foreach (Transform child in player.transform)
        {
            if (child.GetComponent<BoxCollider2D>() != null)
            {
                Physics2D.IgnoreCollision(child.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            }
        }

    }

    private void Update()
    {
        _centre = player.transform.position;
        _angle += RotateSpeed * Time.deltaTime;

        var offset = new Vector2(Mathf.Sin(_angle), Mathf.Cos(_angle)) * Radius;
        transform.position = _centre + offset;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().TakeDamage(damage);
            Destroy(gameObject);
        }
        else if(collision.CompareTag("EnemyProjectil"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        
    }

}
