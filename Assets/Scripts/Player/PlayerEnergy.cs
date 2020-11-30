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
    //[HideInInspector]
    public bool energyIsReloading = false;
    private bool isActive;
    public int maxStackNumber=3;
    public int currentStack;
    private float timeToNotAbuse = 10f;
    private float minAmountEnergy = 10f;

    private Redbull redbull;

    protected override void Awake()
    {
        base.Awake();
        //energyBarGFX = GameObject.FindGameObjectWithTag("EnergyBar");
        //energyBarGFX.SetActive(false);
        SetMaxEnergy();

        redbull = FindObjectOfType<Redbull>();
    }
    private void Update()
    {
        StartCoroutine(EnergyReloading());
        // energyBar.SetEnergy(currentEnergy);
        CalculNumberStack();
        ResetEnergy();
    }
    
    // Déduit l'energie dépensée
    // Si module redbull présent, il a son énergie au max!
    public void SpendEnergy(float energySpend)
    {
        if (redbull.UseModule)
        {
            currentEnergy = maxEnergy;
        }
        else
        {
            currentEnergy -= energySpend;
        }
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
        if (energyIsReloading && currentEnergy < playerData.maxEnergy && !EnergyDrink.interrupt)
        {
            if (currentEnergy <= minAmountEnergy)
            {
                energyIsReloading = false;
                yield return new WaitForSeconds(timeToNotAbuse);
                energyIsReloading = true;
                currentEnergy = 25f;
            }
            else
            {
                energyIsReloading = false;
                currentEnergy += playerData.energyReloadNumber;
                yield return new WaitForSeconds(0.2f);
                energyIsReloading = true;
            }
        }
        else
        {
            yield break;
        }
    }
}
