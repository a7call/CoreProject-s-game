using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleOverTime : MonoBehaviour
{
    public float timer = 0f;
    private float maxSize = 20f; // Taille max du TP (à définir)
    public float growTime = 2f; // Temps pendant lequel il grandit (à définir)
    private bool isMaxSize = false;

    private void Start()
    {
        if (!isMaxSize) StartCoroutine(Grow());
    }

    // Coroutine qui fait que le TP grandi
    public IEnumerator Grow()
    {
        Vector2 startScale = transform.localScale;
        Vector2 maxScale = new Vector2(maxSize, maxSize);

        do
        {
            transform.localScale = Vector2.Lerp(startScale, maxScale, timer / growTime);
            timer += Time.unscaledDeltaTime; // A revoir
            yield return null;
        }
        while (timer < growTime);  
        
        isMaxSize = true;
    }
}
