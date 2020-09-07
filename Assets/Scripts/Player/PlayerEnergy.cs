using System.Collections;
using UnityEngine;

public class PlayerEnergy : MonoBehaviour
{
    public int maxEnergy;
    public int currentEnergy;
    public int energyReloadNumber;
    public bool energyIsReloading = false;


    public static PlayerEnergy instance;

    // Singleton
    private void Awake()
    {
       
        if (instance != null)
        {
            Debug.LogWarning("+ d'une instance de PlayerEnergy dans la scene");
            return;
        }
        else
        {
            instance = this;
        }
        SetMaxEnergy();
    }
    private void Update()
    {
      
       StartCoroutine(EnergyReload());
    }
    
    // Déduit l'energie dépensée
    public void SpendEnergy(int energySpent)
    {
        currentEnergy -= energySpent;
    }


    // Set Max Energy à l'Initialisation
    void SetMaxEnergy()
    {
        currentEnergy = maxEnergy;
    }

    // Energie recupérée au cours du temps
    private IEnumerator EnergyReload()
    {   
        if(currentEnergy < maxEnergy && !energyIsReloading)
        {
            energyIsReloading = true;
            currentEnergy += energyReloadNumber;
            yield return new WaitForSeconds(0.1f);
            energyIsReloading = false;
        }
        else
        {
            yield return 0;
        }
        
    }
}
