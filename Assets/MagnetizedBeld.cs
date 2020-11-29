using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetizedBeld : ActiveObjects
{
    protected Vector3 dir;
    protected RaycastHit2D hit;
    private bool isGoingToWall;

    [SerializeField]  protected LayerMask hitLayer;
    
    private void GetDatas()
    {
    }
    protected override void Update()
    {
        base.Update();
        if (UseModule)
        {
            
            UseModule = false;
            DetectWall();
        }
        StartCoroutine(GrapWall());
    }

    private RaycastHit2D DetectWall()
    {
        hit = Physics2D.Raycast(transform.position, GetDirection(), range, hitLayer);
        if (hit.collider != null)
        {
            isGoingToWall = true;
            return hit;
        }
        else
        {
            return hit;
        }

    }

    private IEnumerator GrapWall()
    {

        if (isGoingToWall)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<Transform>().Translate(GetDirection() * speed * Time.deltaTime);
            yield return new WaitForSeconds(2f);
            isGoingToWall = false;
        }
    }
}
