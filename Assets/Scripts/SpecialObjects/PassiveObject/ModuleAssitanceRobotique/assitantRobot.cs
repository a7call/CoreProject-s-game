using System.Collections;
using UnityEngine;

public class assitantRobot : MonoBehaviour
{
    [SerializeField] private float RotateSpeed = 0f;
    [SerializeField] private float Radius = 0f;
    [SerializeField] private float shootRadius = 0f;
    [SerializeField] private float shootTimer = 0f;
    [SerializeField] protected GameObject proj = null;
    [HideInInspector]
    public Vector2 dir;
    public int damage;
    private bool canShoot = true;
    [SerializeField] private LayerMask enemyLayer = 0;


    private Vector2 _centre;
    private float _angle;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
       
    }

    private void Update()
    {
        _centre = player.transform.position;
        _angle += RotateSpeed * Time.deltaTime;

        var offset = new Vector2(Mathf.Sin(_angle), Mathf.Cos(_angle)) * Radius;
        transform.position = _centre + offset;
        ShootATarget();
    }


   private void ShootATarget()
    {
        Collider2D hit = Physics2D.OverlapCircle(player.transform.position, shootRadius, enemyLayer);
        if(hit != null)
        {
            dir = (hit.transform.position - transform.position).normalized;
            if (canShoot)
            {
                StartCoroutine(shootCo());
            }
            
        }
    }

    private IEnumerator shootCo()
    {
        canShoot = false;
        Instantiate(proj, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(shootTimer);
        canShoot = true;
    }

}
