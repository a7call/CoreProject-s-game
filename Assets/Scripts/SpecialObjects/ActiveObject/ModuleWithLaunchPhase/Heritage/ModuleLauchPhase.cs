using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleLauchPhase : MonoBehaviour
{
    protected Vector3 positionMouse;
    protected Vector3 playePos;
    protected Vector3 direction;
    protected Vector3 Force;
    protected ActiveObjects module;
    protected Rigidbody2D rbBomb;
    protected bool isNotMoving;
    protected bool isSetToGround;
    protected bool isAlreadyActive = false;
    public Transform transBomb;
    public Transform transShadow;
    protected bool isFalling = false;
    protected float coef;


    protected virtual void Start()
    {
        GetDatas();
        SetVerticalForce();
        StartCoroutine(CheckIfFalling());
    }
    private void SetVerticalForce()
    {
        float distanceToPlayer = Vector3.Distance(positionMouse, playePos);
        if (distanceToPlayer > module.range) distanceToPlayer = module.range;
        coef = distanceToPlayer / module.range;
        if (coef < 0.42) coef = 0.42f;
        rbBomb.AddForce(new Vector2(0, coef * 300), ForceMode2D.Force);

    }

    protected void Launch(Vector3 mousePos, Vector3 playerPos, float range, Vector3 dir)
    {
        if ((Vector3.Distance(mousePos, playerPos) < 1))
        {
            isFalling = true;
            rbBomb.gravityScale = 0;
            transBomb.position = transShadow.position;
            return;
        }
        if (Vector3.Distance(mousePos, playerPos) < range)
        {
            if (Vector3.Distance(mousePos, transform.position) > 0.1)
            {
                transform.Translate(dir * 3f * Time.deltaTime);
            }
            else
            {
                transform.position = mousePos;
               
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, playePos) < range)
            {
                transform.Translate(dir * 3f * Time.deltaTime);
            }
        }
       
    }
    protected virtual void Update()
    {
        if (!isNotMoving)
        {
            StartCoroutine(CheckIfMoving());
            CheckIfTouchingGround();
        }
        if (!isSetToGround)
        {
            Launch(positionMouse, playePos, module.range, direction);
        }
        else
        {
            transBomb.position = transform.position;
        }
        


    }

    private void GetDatas()
    {
        rbBomb = transBomb.GetComponent<Rigidbody2D>();
        module = FindObjectOfType<ActiveObjects>();
        positionMouse = module.GetMousePosition();
        playePos = module.transform.position;
        direction = module.GetDirection().normalized;
    }


    private IEnumerator CheckIfMoving()
    {
        Vector3 firstPos = transform.position;
        yield return new WaitForSeconds(0.2f);
        Vector3 secondPos = transform.position;
        if(firstPos == secondPos)
        {
            isNotMoving = true;
            isSetToGround = true;
        }
    }

    private IEnumerator CheckIfFalling()
    {
        yield return new WaitForSeconds(0.5f);
        isFalling = true;
    }

    private void CheckIfTouchingGround()
    {
        if (Vector2.Distance(transBomb.position, transform.position) < 0.15 && isFalling)
        {
            
            rbBomb.gravityScale = 0;
            transBomb.position = transform.position;
        }
    }
}
