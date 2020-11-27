using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveObjects : MonoBehaviour
{

    protected bool UseModule = false;
    protected bool ModuleAlreadyUse = false;
    protected bool readyToUse = true;
    [SerializeField] protected float cd;
    // Start is called before the first frame update
    void Start()
    {
        
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
}
