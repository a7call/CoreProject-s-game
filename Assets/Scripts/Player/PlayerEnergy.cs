using System.Collections;
using UnityEngine;
/// <summary>
/// Classe gérant l'énergi du joueur
/// </summary>
public class PlayerEnergy : Player
{

    public bool energyIsReloading = false;
    public int currentStack;

    protected override void Awake()
    {
        base.Awake();
        SetMaxEnergy();
    }
    private void Update()
    {
        StartCoroutine(EnergyReloading());
        if(currentStack  > maxStacks)
        {
            currentStack = maxStacks;
            energyIsReloading = false;
            StopAllCoroutines();
        }
    }
    
    // Déduit l'energie dépensée
    // Si module redbull présent, il a son énergie au max!
    public void SpendEnergy(int stackSpend)
    {
       currentStack -= stackSpend;

    }


    // Set Max Energy à l'Initialisation
    void SetMaxEnergy()
    {
        currentStack = maxStacks;
    }

    public IEnumerator EnergyReloading()
    {
        if(!energyIsReloading && currentStack != maxStacks)
        {
            energyIsReloading = true;
            yield return new WaitForSeconds(stacksReloadTime);
            currentStack++;
            energyIsReloading = false;
        }

    }
}
