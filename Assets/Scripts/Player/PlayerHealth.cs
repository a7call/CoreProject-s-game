using System.Collections;
using UnityEngine;
/// <summary>
/// Classe player health gérant la vie du joueur 
/// </summary>

public class PlayerHealth : MonoBehaviour
{
    public PlayerScriptableObjectScript playerData;

    public int currentHealth;
    private bool isInvincible;
    public float InvincibilityFlashDelay;
    public float InvincibleDelay;

    public SpriteRenderer graphics;
    public void Awake()
    {
        SetMaxHealth();
    }

    public void SetMaxHealth()
    {
        currentHealth =  playerData.maxHealth;
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
