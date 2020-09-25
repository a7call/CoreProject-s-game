using UnityEngine;

public class Healer : Enemy
{
    [SerializeField] protected GameObject[] ennemies;
    protected int amountToheal;
    protected bool startHealing = false;
    protected bool hasFinished = true;
    

    private void Start()
    {
       
    }

    private void Update()
    {
        GetEnnemiToHeal();
    }

    protected override void UpdatePath()
    {
        
    }

    private void GetEnnemiToHeal()
    {
        if (hasFinished)
        {
        int lowerEnnemiHealth = 10;
        float count = 0;
            for (int i = 0; i < ennemies.Length; i++)
            {
                hasFinished = false;

                if (ennemies[i].GetComponent<Enemy>().currentHealth < ennemies[i].GetComponent<Enemy>().maxHealth)
                {
                    startHealing = true;
                }
                else
                {
                    count++;
                    if (count == ennemies.Length)
                    {
                        startHealing = false;
                    }
                }

                GameObject nouveauCandidat = ennemies[i];
                if (nouveauCandidat.GetComponent<Enemy>().currentHealth < lowerEnnemiHealth && startHealing)
                {
                    lowerEnnemiHealth = ennemies[i].GetComponent<Enemy>().currentHealth;
                    print(i);
                }
                hasFinished = true;
            }
        }
       
    }
}
