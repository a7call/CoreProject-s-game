using System.Collections;
using UnityEngine;
/// <summary>
/// Classe gérant l'énergi du joueur
/// </summary>
public class PlayerEnergy : Player
{

    public float currentEnergy;
    public EnergyBar energyBar;
    public GameObject energyBarGFX;
    public bool energyIsReloading = false;
    private bool isActive;
    public int maxStackNumber=3;
    public int currentStack;

    protected override void Awake()
    {
        base.Awake();
        //energyBarGFX = GameObject.FindGameObjectWithTag("EnergyBar");
        //energyBarGFX.SetActive(false);
        SetMaxEnergy();
    }
    private void Update()
    {
        StartCoroutine(EnergyReloading());
        // energyBar.SetEnergy(currentEnergy);
        CalculNumberStack();
        ResetEnergy();
    }
    
    // Déduit l'energie dépensée
    public void SpendEnergy(float energySpend)
    {
        currentEnergy -= energySpend;
    }


    // Set Max Energy à l'Initialisation
    void SetMaxEnergy()
    {
        currentEnergy = playerData.maxEnergy;
       // energyBar.SetMaxEnergy(playerData.maxEnergy);
    }

    private void ResetEnergy()
    {
        if (currentEnergy < 0)
        {
            currentEnergy = 0f;
        }
        if (currentEnergy > maxEnergy)
        {
            currentEnergy = maxEnergy;
        }
    }
    private void CalculNumberStack()
    {
        if (currentEnergy <= maxEnergy)
        {
            currentStack = maxStackNumber;

                if (currentEnergy < 75)
                {
                    currentStack = maxStackNumber - 1;

                    if (currentEnergy < 50)
                    {
                        currentStack = maxStackNumber - 2;

                        if (currentEnergy < 25)
                        {
                            currentStack = maxStackNumber - 3;
                        }
                    }
                }
        }
    }

    // Energie recupérée au cours du temps
    //private IEnumerator EnergyReload()
    //{

    //    if (currentEnergy < playerData.maxEnergy)
    //    {

    //        if (!isActive)
    //        {
    //           // energyBarGFX.SetActive(true);
    //            isActive = true;
    //        }
    //        energyIsReloading = true;
    //        currentEnergy += playerData.energyReloadNumber;
    //        yield return new WaitForSeconds(0.1f);
    //        energyIsReloading = false;
    //    }
    //    else
    //    {
    //        if (isActive)
    //        {
    //            //energyBarGFX.SetActive(false);
    //            isActive = false;
    //        }
    //        yield break;
    //    }
        
    //}
    
    public IEnumerator EnergyReloading()
    {
        if (energyIsReloading && currentEnergy < playerData.maxEnergy)
        {
            currentEnergy += playerData.energyReloadNumber;
            yield return new WaitForSeconds(0.1f);
        }
        else
        {
            yield break;
        }
    }
}
