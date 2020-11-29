using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleLauchPhase : MonoBehaviour
{
    protected Vector3 positionMouse;
    protected Vector3 playePos;
    protected Vector3 direction;
    protected ActiveObjects module;
    protected bool isNotMoving;
    protected bool isAlreadyActive = false;


    protected virtual void Start()
    {
        GetDatas();
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
        Launch(positionMouse, playePos, module.range, direction);
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
