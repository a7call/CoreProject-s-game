using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProteinBar : StacksObjects
{
    protected override void Start()
    {
        base.Start();
        player = FindObjectOfType<Player>();
    }

    protected override void Update()
    {
        base.Update();
        if(UseModule)
        {
            TakeProteinBar();
            UseModule = false;
        }
    }

    private void TakeProteinBar()
    {
        int gainHp = 2;
        player.CurrentHealth += gainHp;
    }

}
