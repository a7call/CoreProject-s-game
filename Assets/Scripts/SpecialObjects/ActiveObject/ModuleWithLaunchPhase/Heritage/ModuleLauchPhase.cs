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
    protected bool isAlreadyActive = false;
    public Transform transBomb;
    public Transform transShadow;



    protected virtual void Start()
    {
        GetDatas();
        rbBomb = transBomb.GetComponent<Rigidbody2D>();
        rbBomb.AddForce(new Vector2(0, 300), ForceMode2D.Force);
    }

    protected void Launch2(Vector3 mousePos, Vector3 playerPos, float range, Vector3 dir)
    {
        if (Vector3.Distance(mousePos, playerPos) < range)
        {
            if (Vector3.Distance(mousePos, transform.position) > 0.3)
            {
                transShadow.Translate(dir * 3f * Time.deltaTime);
                transBomb.Translate(dir * 3f * Time.deltaTime);
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
                transShadow.Translate(dir * 3f * Time.deltaTime);
                transBomb.Translate(dir * 3f * Time.deltaTime);
            }
        }
    }
    protected void Launch(Vector3 mousePos, Vector3 playerPos, float range, Vector3 dir)
    {
        if (Vector3.Distance(mousePos, playerPos) < range)
        {
            if (Vector3.Distance(mousePos, transform.position) > 0.3)
            {
                transform.Translate(dir * 1.5f * Time.deltaTime);
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
                transform.Translate(dir * 5 * Time.deltaTime);
            }
        }
    }
    protected virtual void Update()
    {
        StartCoroutine(CheckIfMoving());
        Launch2(positionMouse, playePos, module.range, direction);
    }

    private void GetDatas()
    {
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
        }
    }
}
