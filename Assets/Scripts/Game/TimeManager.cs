using System.Collections;
using System;
using UnityEngine;

/// <summary>
/// Permet de gérer le temps du jeu, et appliquer des slowMotion par la fonction DoSlowMotion
/// </summary>
/// 
public class TimeManager 
{
    private float SlowDownFactor { get; set; }
    private float SlowDownLenght { get; set; }
    public TimeManager(float slowDownFactor, float slowDownLenght)
    {
        SlowDownFactor = slowDownFactor;
        SlowDownLenght = slowDownLenght;
    }
    private IEnumerator SlowMotionCo()
    {
        while(Time.timeScale != 1)
        {
            Time.timeScale += Time.unscaledDeltaTime * (1f / SlowDownLenght);
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
            yield return null;
        }
            Time.fixedDeltaTime = 0.02f;
    }

    // Applique la slowMotion
    public void DoSlowMotion()
    {
        Time.timeScale = SlowDownFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
        CoroutineManager.GetInstance().StartCoroutine(SlowMotionCo());

    }
}

