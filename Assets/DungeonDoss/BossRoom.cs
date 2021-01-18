using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : testdung
{
    protected override void Start()
    {
        if (up)
        {
            down = false;
            left = false;
            right = false;
        }else if (down)
        {
            left = false;
            right = false;
            up = false;
        } else if (right)
        {
            up = false;
            left = false;
            down = false;

        }
        else
        {
            up = false;
            right = false;
            down = false;
        }
        base.Start();
    }
}
