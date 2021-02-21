using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleOverTime : MonoBehaviour
{
    public float timer = 0f; // Timer
    private float maxSize = 20f; // Taille max du TP (à définir)
    public float growTime = 2f; // Temps pendant lequel il grandit (à définir)

    // Vector contenant les tailles de TP
    private Vector2 startScale; // Taille initiale
    private Vector2 maxScale; // Taille max

    private void Start()
    {
        // Définition des vectors
        startScale = transform.localScale;
        maxScale = new Vector2(maxSize, maxSize);
    }

    // Coroutine qui fait que le TP grandi
    public IEnumerator Grow()
    {
        do
        {
            transform.localScale = Vector2.Lerp(startScale, maxScale, timer / growTime);
            timer += Time.unscaledDeltaTime;

            yield return null;
        }
        while (timer <= growTime);

        timer = 0f;
    }

    // Coroutine qui fait que le TP diminue
    public IEnumerator Decrease()
    {
        do
        {
            transform.localScale = Vector2.Lerp(maxScale, startScale, timer / growTime) ;
            timer += Time.unscaledDeltaTime;

            yield return null;
        }
        while (timer <= growTime);

        timer = 0f;
    }
}
