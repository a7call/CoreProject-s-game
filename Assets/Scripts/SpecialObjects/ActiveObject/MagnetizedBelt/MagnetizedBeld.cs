using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetizedBeld : CdObjects
{
    protected Vector3 dir;
    protected RaycastHit2D hit;
    protected RaycastHit2D hitMemory;
    private bool isGoingToWall;
    [SerializeField]  protected LayerMask hitLayer;
    [SerializeField] protected float projectileSpeed;
    protected override void Start()
    {
        base.Start();
        GetDatas();
    }
    private void GetDatas()
    {
        player = player.GetComponent<Player>();
    }
    protected override void Update()
    {
        base.Update();
        if (UseModule)
        {
            
            UseModule = false;
            hitMemory = DetectWall() ;
        }
        GrapWall();
    }

    private RaycastHit2D DetectWall()
    {
        hit = Physics2D.Raycast(transform.position, GetDirection(), range, hitLayer);
        if (hit.collider != null)
        {
            
            player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            isGoingToWall = true;
            return hit;
        }
        else
        {
            return hit;
        }

    }

    private void GrapWall()
    {

        if (isGoingToWall)
        {

            player.currentEtat = Player.EtatJoueur.Grapping;
            player.rb.AddForce(GetDirection() * projectileSpeed * Time.deltaTime);
            if (Vector3.Distance(hitMemory.collider.transform.position, player.transform.position) < 0.5f)
            {
                player.rb.velocity = Vector2.zero;
                isGoingToWall = false;
                player.currentEtat = Player.EtatJoueur.normal;
            }
          
        }
    }
}
