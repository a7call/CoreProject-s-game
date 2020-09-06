using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;
    private bool isInvincible;
    public float InvincibilityFlashDelay;
    public float InvincibleDelay;

    public SpriteRenderer graphics;

    public static PlayerHealth instance;

    // Singleton
    private void Awake()
    {
        SetMaxHealth();
        if (instance != null)
        {
            Debug.LogWarning("+ d'une instance de PlayerHealth dans la scene");
            return;
        }
        else
        {
            instance = this;
        }
    }

    public void SetMaxHealth()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        StartCoroutine(InvincibilityDelay());
        StartCoroutine(InvincibilityFlash());
    }

    private IEnumerator InvincibilityFlash()
    {
        while(isInvincible){
            graphics.color = new Color(1f, 1f, 1f, 0f);
            yield return new WaitForSeconds(InvincibilityFlashDelay);
            graphics.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(InvincibilityFlashDelay);
        }
    }

    private IEnumerator InvincibilityDelay()
    {
        isInvincible = true;
        yield return new WaitForSeconds(InvincibleDelay);
        isInvincible = false;
    }
}
