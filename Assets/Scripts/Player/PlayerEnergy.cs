using System.Collections;
using UnityEngine;

public class PlayerEnergy : MonoBehaviour
{

    public PlayerScriptableObjectScript playerData;
    public int currentEnergy;
    public bool energyIsReloading = false;

    private void Awake()
    {
       
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
        currentEnergy = playerData.maxEnergy;
    }

    // Energie recupérée au cours du temps
    private IEnumerator EnergyReload()
    {   
        if(currentEnergy < playerData.maxEnergy && !energyIsReloading)
        {
            energyIsReloading = true;
            currentEnergy += playerData.energyReloadNumber;
            yield return new WaitForSeconds(0.1f);
            energyIsReloading = false;
        }
        else
        {
            yield return 0;
        }
        
    }
}
