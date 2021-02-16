using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistorsionTemporelle : CdObjects
{
    private float currentSlowMotion = 0f;
    [SerializeField] private float slowTimeAllowed = 3f;
    private float newTime = 0.5f; // En mettant 0.5, le jeu va deux fois moins vite

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        if (UseModule)
        {
            SlowMotionFonction();
            UseModule = false;
        }
        VerifySlowMo();
      
    }

    private void SlowMotionFonction()
    {
        if (Time.timeScale == 1f)
        {
            Time.timeScale = newTime;
        }
        else
        {
            Time.timeScale = 1f;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }

    }

    private void VerifySlowMo()
    {
        if (Time.timeScale == newTime)
        {
            currentSlowMotion += Time.deltaTime;
        }

        if (currentSlowMotion > slowTimeAllowed)
        {
            currentSlowMotion = 0;
            Time.timeScale = 1f;
        }
    }
}
