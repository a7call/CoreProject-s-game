using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetizedBeld : CdObjects
{
    protected Vector3 dir;
    protected RaycastHit2D hit;
    protected RaycastHit2D hitMemory;
    private bool isGoingToWall;
    private GameObject player;
    private PlayerMouvement playerMouv;
    [SerializeField]  protected LayerMask hitLayer;
    [SerializeField] protected float speed;
    protected override void Start()
    {
        base.Start();
        GetDatas();
    }
    private void GetDatas()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerMouv = player.GetComponent<PlayerMouvement>();
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
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            playerMouv.currentEtat = Player.EtatJoueur.Grapping;
            playerMouv.rb.AddForce(GetDirection() * speed * Time.deltaTime);
            if (Vector3.Distance(hitMemory.collider.transform.position, player.transform.position) < 0.5f)
            {
                playerMouv.rb.velocity = Vector2.zero;
                isGoingToWall = false;
                playerMouv.currentEtat = Player.EtatJoueur.normal;
            }
          
        }
    }
}
