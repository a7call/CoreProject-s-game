using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{


    [SerializeField] protected int currentHealth;
    protected  int maxHealth;
    protected Material whiteMat;
    protected Material defaultMat;
    [SerializeField] protected SpriteRenderer spriteRenderer;

    private void Start()
    {
        SetMaxHealth();
    }

    private void Update()
    {
      
    }

    // Set health to maximum
    private void SetMaxHealth()
    {
        currentHealth = maxHealth;
    }

    // prends les dammages
    protected void TakeDamage(int _damage)
    {
        currentHealth -= _damage;
        StartCoroutine(WhiteFlash());
    }

    // Couroutine white flash on hit
    protected IEnumerator WhiteFlash()
    {

        spriteRenderer.material = whiteMat;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.material = defaultMat;

    }
     

}
