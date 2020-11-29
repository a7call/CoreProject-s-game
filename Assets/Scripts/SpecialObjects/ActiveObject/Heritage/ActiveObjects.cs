using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveObjects : MonoBehaviour
{

    protected bool UseModule = false;
    protected bool ModuleAlreadyUse = false;
    protected bool readyToUse = true;
    [SerializeField] protected float cd;
    public float range;
    [SerializeField] protected float speed;

    protected Vector3 MousePos;
    protected Camera Cam;

    // Start is called before the first frame update
    protected void Start()
    {
        Cam = Camera.main;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.U) && readyToUse)
        {
            StartCoroutine(CdToReUse());
            readyToUse = false;
            UseModule = true;
        }
    }


    private IEnumerator CdToReUse()
    {
        yield return new WaitForSeconds(cd);
        readyToUse = true;
    }


    public virtual Vector3 GetMousePosition()
    {
        MousePos = Cam.ScreenToWorldPoint(Input.mousePosition);
        MousePos.z = 0;
        return MousePos;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, range);
        Gizmos.color = Color.red;
    }

    public Vector3 GetDirection()
    {
        Vector3 direction = (GetMousePosition() - transform.position).normalized;
        return direction;
    }
    
}
