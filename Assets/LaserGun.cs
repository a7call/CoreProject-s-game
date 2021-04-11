
using UnityEngine;
using Wanderer.Utils;

public class LaserGun : Weapons
{
    public LineRenderer lineRenderer;
    void Start()
    {
        DisableLaser();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (Input.GetMouseButtonDown(0))
        {
            EnableLaser();
        }

        if (Input.GetMouseButton(0))
        {
            UpdateLaser();
        }

        if (Input.GetMouseButtonUp(0))
        {
            DisableLaser();
        }

        
    }


    void EnableLaser()
    {
        lineRenderer.enabled = true;
    }

    void UpdateLaser()
    {
        Vector2 mousePos = Utils.GetMouseWorldPosition();
        lineRenderer.SetPosition(0, attackPoint.position);
        lineRenderer.SetPosition(1, mousePos);

        Vector2 direction = mousePos - (Vector2)transform.position;
        RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position, direction.normalized, direction.magnitude);
        if (hit)
            lineRenderer.SetPosition(1, hit.point);

    }

    void DisableLaser()
    {
        lineRenderer.enabled = false;
    }
}
