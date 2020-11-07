using System.Collections;
using UnityEngine;
/// <summary>
/// Classe gérant l'énergi du joueur
/// </summary>
public class PlayerEnergy : Player
{

    public int currentEnergy;
    public EnergyBar energyBar;
    public GameObject energyBarGFX;
    public bool energyIsReloading = false;
    private bool isActive;

    protected override void Awake()
    {
        base.Awake();
        //energyBarGFX = GameObject.FindGameObjectWithTag("EnergyBar");
        //energyBarGFX.SetActive(false);
        SetMaxEnergy();
    }
    private void Update()
    {
       if(!energyIsReloading) StartCoroutine(EnergyReload());
      // energyBar.SetEnergy(currentEnergy);
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
       // energyBar.SetMaxEnergy(playerData.maxEnergy);
    }

    // Energie recupérée au cours du temps
    private IEnumerator EnergyReload()
    {

        if (currentEnergy < playerData.maxEnergy)
        {

            if (!isActive)
            {
               // energyBarGFX.SetActive(true);
                isActive = true;
            }
            energyIsReloading = true;
            currentEnergy += playerData.energyReloadNumber;
            yield return new WaitForSeconds(0.1f);
            energyIsReloading = false;
        }
        else
        {
            if (isActive)
            {
                //energyBarGFX.SetActive(false);
                isActive = false;
            }
            yield break;
        }
        
    }
    
}
