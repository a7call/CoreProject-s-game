using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveObjects : MonoBehaviour
{

    protected bool UseModule = false;
    protected bool ModuleAlreadyUse = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            UseModule = true;
            print("1");
        }
    }
}
