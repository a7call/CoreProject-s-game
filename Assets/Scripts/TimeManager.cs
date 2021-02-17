using UnityEngine;

/// <summary>
/// Permet de gérer le temps du jeu, et appliquer des slowMotion par la fonction DoSlowMotion
/// </summary>
/// 
public class TimeManager : MonoBehaviour
{
    public float slowDownFactor = 0.5f; // Le jeu va deux fois moins vite
    public float slowDownLenght = 2f; // Durée du slowMo

    // Permet de remettre le jeu normalement
    private void Update()
    {
        Time.timeScale += Time.unscaledDeltaTime * (1f / slowDownLenght);
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
    }
    
    // Applique la slowMotion
    public void DoSlowMotion()
    {
        Time.timeScale = slowDownFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }
}
