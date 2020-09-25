using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : Enemy
{
    public List<GameObject> ennemies;
    protected int amountToheal;
    protected bool startHealing = false;
    protected bool hasFinished = true;
    [SerializeField] protected float healCd;

    private void Start()
    {
        List<GameObject> ennemies = new List<GameObject>();

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
        int lowerEnnemiHealth = 4;
        float count = 0;
        GameObject[] ennemiArray = ennemies.ToArray();
            for (int i = 0; i < ennemiArray.Length; i++)
            {
                GameObject nouveauCandidat = ennemiArray[i];
                hasFinished = false;

                if(nouveauCandidat == null)
                {
                    ennemies.Remove(ennemiArray[i]);
                    hasFinished = true;
                    return;
                }

                if (nouveauCandidat.GetComponent<Enemy>().currentHealth < nouveauCandidat.GetComponent<Enemy>().maxHealth)
                {
                    startHealing = true;
                }
                else
                {
                    count++;
                    if (count == ennemiArray.Length)
                    {
                        startHealing = false;
                        hasFinished = true;
                    }
                }

                
                if (nouveauCandidat.GetComponent<Enemy>().currentHealth < lowerEnnemiHealth && startHealing)
                {
                    
                    lowerEnnemiHealth = nouveauCandidat.GetComponent<Enemy>().currentHealth;
                     StartCoroutine(HealEnnemiCo(nouveauCandidat, 1));
                    
                }
            }
        }
       
    }

    // A revoir ! si monstre detruit ? 
    private IEnumerator HealEnnemiCo(GameObject _ennemiToHeal, int _amountToHeal)
    {
        while (_ennemiToHeal.GetComponent<Enemy>().currentHealth < _ennemiToHeal.GetComponent<Enemy>().maxHealth)
        {
            _ennemiToHeal.GetComponent<Enemy>().currentHealth += _amountToHeal;
            yield return new WaitForSeconds(healCd);
            if (_ennemiToHeal == null)
            {
                hasFinished = true;
                yield break;
            }
        }

        hasFinished = true;
        yield return null;
    }

}
